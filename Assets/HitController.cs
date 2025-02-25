using UnityEngine;

public class HitController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool isEffective;
    private int hitResult;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (isEffective)
    //    {
    //        if (other.CompareTag("Player"))
    //        {
    //            int result = other.GetComponent<Player>().OnDamage();
    //            switch (result)
    //            {
    //                case 0:
    //                    // dodged the attack, keep effective
    //                    hitResult = 0;
    //                    break;
    //                case 1:
    //                    // parried the attack, not effective anymore

    //                    isEffective = false;
    //                    hitResult = 1;
    //                    break;
    //                case 2:
    //                    isEffective = false;
    //                    hitResult = 2;
    //                    break;
    //            }
    //        }
    //    }
    //}

    public int GetHitResult()
    {
        return hitResult;
    }

    public void StartAttackCheck()
    {
        isEffective = true;
    }

    public void StopAttackCheck()
    {
        isEffective = false;
    }   
}
