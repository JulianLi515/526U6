using System.Collections;
using UnityEngine;

public class DraupnirSpear : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public enum SpearState
    {
        InAir,
       
        OnWall
       
    }
    
    public SpearState state;
    public float moveSpeed;
    public float liveTime;
    private Rigidbody2D rb;
    private PlatformEffector2D pe;
    void Start()
    {
        state = SpearState.InAir;
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        pe = GetComponent<PlatformEffector2D>();
        StartCoroutine(nameof(DestroySpearCoroutine));
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case SpearState.InAir:
                rb.linearVelocity = transform.right*moveSpeed;

                break;
            case SpearState.OnWall:
                break;
        }
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (state == SpearState.OnWall)
        {
            return;
        }
        
        if (collision.collider.CompareTag("Wall"))
        {
            state = SpearState.OnWall;
            rb.linearVelocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            //rb.bodyType = RigidbodyType2D.Dynamic;
            //pe.enabled = true;
            transform.parent = collision.transform;
            //gameObject.layer = LayerMask.NameToLayer("Ground");
        }
        if (collision.collider.CompareTag("Player"))
        {
            //Debug.Log("Playerddddddddddddddd");
            collision.gameObject.transform.parent = transform;
        }

    }

    private void OnCollisionExit2D(UnityEngine.Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }

    IEnumerator DestroySpearCoroutine()
    {
        yield return new WaitForSeconds(liveTime);
        Destroy(gameObject);
    }
}
