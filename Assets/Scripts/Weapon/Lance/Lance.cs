using Unity.Cinemachine;
using UnityEngine;

public class Lance : PlayerWeapon
{
    [Header("Component")]
    public Rigidbody2D rb;
    public Collider2D col;
    public GameObject skin;
    public GameObject throwableLancePrefab;
    

    [Header("Initialization")]
    public Vector3 spwanPoint;

    [Header("Timer")]
    public float timer;

    [Header("Stats")]
    public bool isDisappear;

    #region States
    public LanceStateMachine stateMachine;
    public LanceIdleState idleState;
    public LanceAttackState attackState;
    #endregion

    private void Awake()
    {
        WeaponID = 0;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        isDisappear = false;

        stateMachine = new LanceStateMachine(this);
        idleState = new LanceIdleState(stateMachine,this);
        attackState = new LanceAttackState(stateMachine, this);

        stateMachine.Initialize(idleState);

    }

    private void OnEnable()
    {
        transform.position = player.transform.position + spwanPoint;
    }
    private void OnDisable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState.Update();
        timerUpdate();
    }

    private void timerUpdate()
    {
        timer = Mathf.Max(0,timer-Time.deltaTime);
    }

    public override void attack()
    {
        base.attack();
        stateMachine.ChangeState(attackState);
    }

    public override void grabSkill()
    {
        base.grabSkill();
    }
    public override void skill()
    {
        base.skill();
    }

    public override void ActivateWeapon()
    {
        base.ActivateWeapon();
    }

    public override void DeactivateWeapon()
    {
        base.DeactivateWeapon();
    }
    public void Disappear()
    {
        col.enabled = false;
        skin.SetActive(false);
        isDisappear = true;
    }

    public void Appear()
    {
        col.enabled = true;
        skin.SetActive(true);
        isDisappear = false ;
    }
}
