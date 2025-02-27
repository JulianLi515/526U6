using System.Collections.Generic;
using UnityEngine;

public class EnemyGrabController : MonoBehaviour
{
    public LayerMask targetLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public bool GetResult()
    {
        List<Collider2D> results = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true; // this is important
        filter.useLayerMask = true;
        filter.SetLayerMask(targetLayer);
        int b = GetComponent<Collider2D>().Overlap(filter, results);
        Debug.Log(b);
        foreach (Collider2D cldr in results)
        {

            if (cldr.CompareTag("PlayerGrabBox"))
            {

                return true;
            }

            
        }
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
