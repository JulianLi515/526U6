using UnityEngine;

public class  HitBoxCalculator: MonoBehaviour
{
    public Rigidbody2D rb;
    public Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnEnable()
    {
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        player.battleInfo = Player.BattleInfo.Hit;
        player.trigger = other.gameObject;
    }
}
