using System;
using System.Collections.Specialized;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyWeaponStill : MonoBehaviour,Deflectable
{
    public Rigidbody2D rb;
    public SpriteRenderer circle;
    public float frequency;
    public int isActive; // isActive is equvilant to is Attacking
    //TODO: Mechanism to prevent Dereferencing Null dfTransform pointer
    public Transform dfTransform { get; set; }
    public int id { get; set; }
    public bool grabbable { get; set; }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dfTransform = transform;
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
        isActive = 1;
        frequency = 2f;
        EventManager.StartListening<Deflectable>("PlayerDeflecting", OnDeflect);
        EventManager.StartListening<Deflectable>("PlayerGettingHit", OnSuccess);
        EventManager.StartListening<Deflectable>("PlayerEvading", OnFailure);
        EventManager.StartListening<Deflectable>("PlayerGrabbing", OnGrab);
    }

    // Update is called once per frame
    void Update()
    {
        Blink();
    }

    private void OnDestroy()
    {
        EventManager.StopListening<Deflectable>("PlayerDeflecting", OnDeflect);
        EventManager.StopListening<Deflectable>("PlayerGettingHit", OnSuccess);
        EventManager.StopListening<Deflectable>("PlayerEvading", OnFailure);
        EventManager.StopListening<Deflectable>("PlayerGrabbing", OnGrab);
    }

    private void Blink()
    {
        frequency = Mathf.Max(frequency - Time.deltaTime, 0);
        if (frequency == 0)
        {
            isActive = 1;
            gameObject.SetActive(true);
        }

        //Active when attack
        if (isActive == 1)
        {
            circle.color = Color.red;
        }
        else if (isActive == -1)
        {
            circle.color = Color.white;
        }
        else
        {
            circle.color = Color.green;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isActive == 1)
        {
            UnityEngine.Debug.Log($"{gameObject.name} is trying to damage {other.name}");

            EventManager.TriggerEvent("EnemyAttacking", this);

        }
    }
    private void OnDeflect(Deflectable df)
    {
        if (ReferenceEquals(df, this))
        // Check if this enemy is the sender

        {
            if (isActive == 1)
            {
                UnityEngine.Debug.Log($"{name}'s attack was deflected ! Enter stun for 5s");
                frequency = 5f;
                isActive *= -1;
            }
        }
    }
    private void OnSuccess(Deflectable df)
    {
        if (ReferenceEquals(df, this))
        // Check if this enemy is the sender
 
        {
            if (isActive == 1)
            {
                UnityEngine.Debug.Log($"{name}'s attack hit player! Good Job");
                //disable current attack
                isActive = 0;
                frequency = 1f;
                
            }
        }
    }
    private void OnFailure(Deflectable df)
    {
        if (ReferenceEquals(df, this))
        // Check if this enemy is the sender

        {
            if (isActive == 1)
            {
                UnityEngine.Debug.Log($"{name}'s attack Evaded by Player, Pitty!");
            }
        }
    }

    private void OnGrab(Deflectable df)
    {
        if(ReferenceEquals(df, this))
        // Check if this enemy is the sender

        {
            if (canGrab())
            {
                UnityEngine.Debug.Log($"{name}'s attack is Grabbed by player, Destroy");
                gameObject.SetActive(false);
                frequency = 5f;
            }
        }
    }
    public Vector3 getPosition() { return transform.position; }
    public bool canGrab() { return true;  }
    public int getID () { return 0; }
    public bool isDrop() { return true;}
}
