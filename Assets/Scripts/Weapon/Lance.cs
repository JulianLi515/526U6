using UnityEngine;

public class Lance : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public enum LanceState
    {
        TrackingTarget,
        TrackingBackOriginator,
        DisFunctioned
    }

    public GameObject originator;
    public GameObject target;
    public SpriteRenderer tip;
    public SpriteRenderer rod;

    private Rigidbody2D rb;

    public float moveSpeed;
    public LanceState lanceState;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
