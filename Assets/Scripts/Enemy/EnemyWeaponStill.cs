using System;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyWeaponStill : MonoBehaviour,Deflectable
{
    public Rigidbody2D rb;
    public SpriteRenderer circle;
    public float frequency;
    public int isActive; // isActive is equvilant to is Attacking
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            isActive = 1;
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
}
