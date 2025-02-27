using System;
using System.Collections.Generic;
using UnityEngine;

public class DeflectHitBoxCalculator : MonoBehaviour
{
    public Rigidbody2D rb;
    public Player player;
    public Vector3 offSet;
    public Collider2D triggerCollider;
    public LayerMask contactLayer;
    private ContactFilter2D contactFilter;
    private List<Collider2D> detectedColliders = new List<Collider2D>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    private void Awake()
    {
        player = GetComponentInParent<Player>();
        rb = GetComponent<Rigidbody2D>();
        triggerCollider = GetComponent<Collider2D>();
        contactFilter.useTriggers = true;
        contactFilter.SetLayerMask(contactLayer);
    }

    // Update is called once per frame
    void Update()
    {
        checkAllColliders();
    }

    public void checkAllColliders()
    {
        detectedColliders.Clear();
        int a = Physics2D.OverlapCollider(triggerCollider, contactFilter, detectedColliders);

        foreach (var collider in detectedColliders)
        {
            Debug.Log(collider.gameObject.name);
            if (collider.gameObject.CompareTag("EnemyAttackBox"))
            {
                Debug.Log("DeflectPPPPPPPPPPPPP");
                player.trigger = collider.gameObject;
                player.battleInfo = Player.BattleInfo.Deflect;
                break;
            }
        }
    }
    private void OnEnable()
    {
        if (player.facingDir == 1)
        {
            transform.position = player.transform.position + offSet;
        }
        else
        {
            transform.position = player.transform.position -offSet;
        }
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
        contactFilter.useTriggers = true;
        contactFilter.SetLayerMask(contactLayer);

    }
}
