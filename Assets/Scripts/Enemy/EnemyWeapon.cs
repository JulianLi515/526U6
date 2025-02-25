using System;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyWeapon : MonoBehaviour,Deflectable
{
    public Rigidbody2D rb;
    public SpriteRenderer circle;
    public float frequency;
    public int isActive; // isActive is equvilant to is Attacking
    public Transform dfTransform { get; set; }
    public bool grabbable { get; set; }
    public int id { get; set; }
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
    }

    private void Blink()
    {
        frequency = Mathf.Max(frequency - Time.deltaTime, 0);
        if (frequency == 0)
        {
            frequency = 2f;
            if (isActive == 0)
            {
                isActive = 1;
            }
            isActive *= -1;
        }

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
    private void OnDeflect(Deflectable attackingEnemy)
    {
        if (ReferenceEquals(attackingEnemy, this))
        // Check if this enemy is the one attacking and deflected
        // Stay stuned and not attacking for 5s;
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
        // Check if this enemy is the one attacking and deflected
        // Stay stuned and not attacking for 5s;
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
        // Check if this enemy is the one attacking and deflected
        // Stay stuned and not attacking for 5s;
        {
            if (isActive == 1)
            {
                UnityEngine.Debug.Log($"{name}'s attack Evaded by Player, Pitty!");
            }
        }
    }

    public void onGrab(Deflectable df)
    {
        if (ReferenceEquals(df, this))
        // Check if this enemy is the one attacking and deflected
        // Stay stuned and not attacking for 5s;
        {
            if (grabbable)
            {
                UnityEngine.Debug.Log($"{name}'s attack is Grabbed by player, Destroy");
            }
        }
    }

    public bool canGrab()
    {
        return grabbable;
    }
    public int getID() { return id; }
}
