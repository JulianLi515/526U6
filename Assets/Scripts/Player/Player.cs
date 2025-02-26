using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;

//using Cinemachine;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public PlayerInput input;
    public enum IState
    {
        Grab,
        Deflect,
        Invincible,
        Fragile,
    }

    [Header("Stats")]
    public SpriteRenderer playerPrototypeSprite;
    public int frameRate = 60;
    public float gravityScale;
    public IState iState;
    public bool ladderCheck;

    [Header("LevelCollision")]
    public Transform groundCheckLeft;
    public Transform groundCheckRight;
    public float groundCheckDistance;
    public Transform wallCheckTop;
    public Transform wallCheckBottom;
    public float wallCheckDistance;
    public LayerMask level;

    [Header("GroundMovement")]
    [SerializeField] private Vector2 rawSpeed;
    public float HorizontalSpeedFalling;
    public float HorizontalSpeedGround;
    public int facingDir;
    public float JumpInitialSpeed;
    public int JumpCounter;
    public bool jumpable => JumpCounter > 0;

    [Header("LadderMovement")]
    public float ladderHorizontalSpeed;
    public float ladderVerticalSpeed;

    [Header("WallJump")]
    public float wallSlideSpeed;
    public float wallJumpSpeedX;
    public float wallJumpSpeedY;
    public float wallJumpFreeze;
    public Timer wallJumpFreezeTimer;

    [Header("Deflect")]
    public GameObject deflectBoxPrefab;
    public float deflectDuration;
    public Timer deflectTimer;
    public float deflectHitboxOffsetX;
    public float deflectHitboxOffsetY;
    public float deflectJumpSpeed;

    [Header("Grab")]
    public GameObject grabBoxPrefab;
    public float grabDuration;
    public Timer grabTimer;
    public float grabHitboxOffsetX;
    public float grabHitboxOffsetY;
    [Header("Roll")]
    public float rollSpeed;
    public float rollCoolDown;
    public Timer rollCoolDownTimer;
    public float rollDuration;
    public Timer rollDurationTimer;
    public float rollInvincibleDuration;


    [Header("Invincible")]
    public Timer Invincibletimer;

    [Header("DamagedPenalty")]
    public float knockbackThreshold = 5f;
    public float knockbackForceMultiplier = 10f;
    public float controlLossDuration = 0.5f;
    public float KnockBackDuration = 0.2f;
    public Timer KnockBackTimer;

    [Header("Weapon")]
    public PlayerWeapon weapon1;
    public PlayerWeapon weapon2;
    public PlayerWeapon currentWeapon;
    public WeaponsDiction weaponDictionary;

    [Header("Animation")]
    [SerializeField] private string animState;

    #region Components
    [Header("Components")]
    // public Animator anim;
    public Rigidbody2D rb { get; private set; }
    #endregion

    #region Controllers
    public TimerCountDownController TimerCountDownCtrl { get; private set; }
    public InvincibleController InvincibleCtrl { get; private set; }
    public GroundMoveController GroundMoveCtrl { get; private set; }
    public FlipController FlipCtrl { get; private set; }
    public InputBufferController InputBufferCtrl { get; private set; }
    public LevelCollisionController LevelCollisionCtrl { get; private set; }
    public AirMoveController AirMoveCtrl { get; private set; }
    public JumpController JumpCtrl { get; private set; }
    public RollController RollCtrl { get; private set; }
    public WallMovementController WallMovementCtrl { get; private set; }
    public DeflectController DeflectCtrl { get; private set; }
    public GrabController GrabCtrl { get; private set; }
    public KnockPlayerBackController KnockBackCtrl { get; private set; }
    public OnFlyableController OnFlyableCtrl { get; private set; }
    public WeaponController WeaponCtrl { get; private set; }
    #endregion

    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerState idleState { get; private set; }
    public PlayerState moveState { get; private set; }
    public PlayerState fallState { get; private set; }
    public PlayerState jumpState { get; private set; }
    public PlayerState rollState { get; private set; }
    public PlayerState wallSlideState { get; private set; }
    public PlayerState wallJumpState { get; private set; }
    public PlayerState dashState { get; private set; }
    public PlayerState onDamageState { get; private set; }
    public PlayerState deflectState { get; private set; }
    public PlayerState grabState { get; private set; }
    public PlayerState deflectRewardState { get; private set; }
    public PlayerState grabRewardState { get; private set; }
    public PlayerState damagePenaltyState { get; private set; }
    public PlayerState ladderMoveState { get; private set; }
    public PlayerState attackState { get; private set; }
    #endregion

    private void Awake()
    {

        input = GetComponent<PlayerInput>();

        TimerCountDownCtrl = new TimerCountDownController(this);
        InvincibleCtrl = new InvincibleController(this);
        GroundMoveCtrl = new GroundMoveController(this);
        FlipCtrl = new FlipController(this);
        InputBufferCtrl = new InputBufferController(this);
        LevelCollisionCtrl = new LevelCollisionController(this);
        AirMoveCtrl = new AirMoveController(this);
        JumpCtrl = new JumpController(this);
        RollCtrl = new RollController(this);
        WallMovementCtrl = new WallMovementController(this);
        DeflectCtrl = new DeflectController(this);
        GrabCtrl = new GrabController(this);
        KnockBackCtrl = new KnockPlayerBackController(this);
        OnFlyableCtrl = new OnFlyableController(this);
        WeaponCtrl = new WeaponController(this);


        stateMachine = new PlayerStateMachine(this);
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        fallState = new PlayerFallState(this, stateMachine, "Fall");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        rollState = new PlayerRollState(this, stateMachine, "Roll");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "wallJump");
        deflectState = new PlayerDeflectState(this, stateMachine, "Deflect");
        grabState = new PlayerGrabState(this, stateMachine, "Grab");
        deflectRewardState = new PlayerDeflectRewardState(this, stateMachine, "DeflectReward");
        grabRewardState = new PlayerGrabRewardState(this, stateMachine, "GrabReward");
        damagePenaltyState = new PlayerDamagedPenalyState(this, stateMachine, "DamagePenalty");
        ladderMoveState = new PlayerLadderMoveState(this, stateMachine, "LadderMove");
        attackState = new PlayerAttackState(this, stateMachine, "Skill");

    }

    private void Start()
    {
        
        Application.targetFrameRate = frameRate;
        
        //assign component
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;
        weaponDictionary = GetComponentInChildren<WeaponsDiction>();

        input.EnableGamePlayInputs();
        stateMachine.Initialize(fallState);
        iState = IState.Fragile;

        //Subscribe to enemyAttacking
        EventManager.StartListening<Deflectable>("EnemyAttacking", OnDamage);
        EventManager.StartListening("InvincibleStop", OnInvincibleStop);

    }

    private void Update()
    {
        // set buffer before state update
        InputBufferCtrl.SetBufferOnInput();

        //Timer count down
        TimerCountDownCtrl.Update();


        stateMachine.currentState.Update();

        //Debug
        rawSpeed = rb.linearVelocity;
        PlayerColorStateIndicator();


    }

    private void LateUpdate()
    {
        stateMachine.currentState.LateUpdate();
        

    }

    private void OnDestroy()
    {
        EventManager.StopListening<Deflectable>("EnemyAttacking", OnDamage);
        EventManager.StopListening("InvincibleStop", OnInvincibleStop);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(200, 200, 200, 200), "playerState: " + stateMachine.currentState.animBoolName);
        GUI.Label(new Rect(200, 220, 200, 200), "LadderDected: " + ladderCheck);
    }

    private void PlayerColorStateIndicator()
    {
        switch (iState)
        {
            case IState.Fragile:
                playerPrototypeSprite.color = Color.gray;
                break;
            case IState.Deflect:
                playerPrototypeSprite.color = Color.blue;
                break;
            case IState.Invincible:
                playerPrototypeSprite.color = Color.yellow;
                break;
        }
    }
    private void OnDrawGizmos()
    {
        //LevelCollisionCtrl.draw();
    }

    private void OnDamage(Deflectable df)
    {

        switch (iState)
        {
            case IState.Grab:
                //Grab Sucessful, grabreward
                stateMachine.ChangeState(grabRewardState, df);
                EventManager.TriggerEvent("PlayerGrabbing", df);
                break;
            case IState.Deflect:
                // Deflect Sucessful, deflectreward
                stateMachine.ChangeState(deflectRewardState,df);
                EventManager.TriggerEvent("PlayerDeflecting", df);
                break;
            case IState.Invincible:
                //EventManager.TriggerEvent("PlayerEvading", df);
                break;
            case IState.Fragile:
                // Get Hit, go To damagedPenalty
                stateMachine.ChangeState(damagePenaltyState,df);
                EventManager.TriggerEvent("PlayerGettingHit", df);
                break;
        }
    }


    private void OnInvincibleStop()
    {
        iState = IState.Fragile;
    }

    public void StartLadderInteractionCheck()
    {
        ladderCheck = true;
    }

    public void StopLadderInteractionCheck()
    {
        ladderCheck = false;
    }

    public bool LadderInteractionCheckOnCurrentState()
    {
        if (ladderCheck)
        {
            if (stateMachine.currentState != ladderMoveState)
            {
                bool case1 = input.Yinput > 0 && LevelCollisionCtrl.IsGroundDetected();
                bool case2 = input.Yinput != 0 && !LevelCollisionCtrl.IsGroundDetected();
                if (case1 || case2)
                {
                    stateMachine.ChangeState(ladderMoveState);
                    return true;
                }
            }
        }
        else
        {
            if (stateMachine.currentState == ladderMoveState)
            {
                if (LevelCollisionCtrl.IsGroundDetected())
                {
                    stateMachine.ChangeState(idleState);
                    return true;
                }
                else
                {
                    stateMachine.ChangeState(fallState);
                    return true;
                }
            }
        }
        return false;
    }


}
