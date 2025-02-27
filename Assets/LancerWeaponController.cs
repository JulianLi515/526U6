using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LancerWeaponController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int result;
    public LayerMask targetLayer;

    void Start()
    {
        result = 0; // default to 1 which is same hit invincible player
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetResult()
    {
        List<Collider2D> results = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;
        filter.useLayerMask = true;
        filter.SetLayerMask(targetLayer);
        bool deflected = false;
        bool hit = false;
        //Debug.Log("Checking");
        int b = GetComponent<Collider2D>().Overlap(filter, results);
        //int a = Physics2D.OverlapCollider(GetComponent<Collider2D>(), filter, results);
        Debug.Log(b);
        foreach (Collider2D cldr in results)
        {
            if (cldr == null)
            {
                continue;
            }
            if (cldr.CompareTag("PlayerDeflectBox"))
            {
                
                deflected = true;
            }

            else if (cldr.CompareTag("PlayerBodyBox"))
            {
                hit = true;
            }
        }

        if (deflected)
        {
            return 2;

        }
        else
        {
            if (hit)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{

    //    if (collision.CompareTag("PlayerDeflectBox"))
    //    {
    //        result = 2;
    //        Debug.Log("DDDDD");
    //        return;
    //    }

    //    if (collision.CompareTag("PlayerInvincibleBox"))
    //    {
    //        result = 0;
    //        Debug.Log("IIII");
    //        return;
    //    }
    //    if (collision.CompareTag("PlayerBodyBox"))
    //    {
    //        result = 1;
    //        Debug.Log("BBBB");
    //        return;

    //    }
        

    //}
    
    //public int GetResult()
    //{
    //    return result;
    //}

    public void OnEnable()
    {
        result = 0;
    }
    


}
