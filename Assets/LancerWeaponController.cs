using UnityEngine;

public class LancerWeaponController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int result;

    void Start()
    {
        result = 1; // default to 1 which is same hit invincible player
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Ground"))
        {
            result = 2;
            return;
        }
        if (collision.CompareTag("Ground"))
        {
            result = 1;
            return;

        }
        if (collision.CompareTag("Ground"))
        {
            result = 0;
            return;
        }
        
    }
    
    public int GetResult()
    {
        return result;
    }

    public void OnEnable()
    {
        result = 1;
    }
    


}
