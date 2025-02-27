using System.Collections;
using UnityEngine;

public class DrpSpearVertical : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public enum SpearState
    {
        InAir,
        OnGround
    }

    public SpearState state;
    public float moveSpeed;
    public float liveTime;
    private Rigidbody2D rb;
    void Start()
    {
        state = SpearState.InAir;
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        StartCoroutine(nameof(DestroySpearVCoroutine));
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case SpearState.InAir:
                rb.linearVelocity = -transform.up * moveSpeed;

                break;
            case SpearState.OnGround:
                break;
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        //other.GetComponent<Player>().OnDamage();
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(collision.gameObject.name);
        if (state == SpearState.OnGround)
        {
            return;
        }

        if (other.CompareTag("Ground"))
        {
            state = SpearState.OnGround;
            rb.linearVelocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            //rb.bodyType = RigidbodyType2D.Dynamic;
            //pe.enabled = true;
            transform.parent = other.transform;
            //gameObject.layer = LayerMask.NameToLayer("Ground");
        }
    }

    IEnumerator DestroySpearVCoroutine()
    {
        yield return new WaitForSeconds(liveTime);
        Destroy(gameObject);
    }
}
