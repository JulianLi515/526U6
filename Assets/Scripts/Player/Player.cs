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
        Defelct,
        Invincible,
        Fragile
    }

    [Header("Stats")]
    public SpriteRenderer playerPrototypeSprite;
    public int frameRate = 60;
    public IState iState;

    [Header("Movement")]
    [SerializeField] private Vector2 rawSpeed;
    public float HorizontalSpeedFalling;
    public float HorizontalSpeedGround;
    public int facingDir = 1;
    public float JumpInitialSpeed;
    public int JumpCounter;
    public bool jumpable => JumpCounter > 0;


    [Header("LevelCollision")]
    public Transform groundCheckLeft;
    public Transform groundCheckRight;
    public float groundCheckDistance;
    public Transform wallCheckTop;
    public Transform wallCheckBottom;
    public float wallCheckDistance;
    public LayerMask level;

    [Header("Deflect")]
    public GameObject deflectBox;
    public float deflectDuration;
    public Timer deflectTimer;

    [Header("Roll")]
    public float rollSpeed;
    public float rollCoolDown;
    public Timer rollCoolDownTimer;
    public float rollDuration;
    public Timer rollDurationTimer;
    public float rollInvincibleDuration;

    [Header("WallJump")]
    public float wallSlideSpeed;
    public float wallJumpSpeedX;
    public float wallJumpSpeedY;
    public float wallJumpFreeze;
    public Timer wallJumpFreezeTimer;

    [Header("Invincible")]
    public Timer Invincibletimer;

    [Header("Animation")]
    [SerializeField] private string animState;

    #region Components
    [Header("Components")]
    // public Animator anim;
    public Rigidbody2D rb { get; private set; }
    public GameObject deflectBoxPrefab;
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


        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        fallState = new PlayerFallState(this, stateMachine, "Fall");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        rollState = new PlayerRollState(this, stateMachine, "Roll");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "wallJump");
        deflectState = new PlayerDeflectState(this, stateMachine, "Deflect");
        //onDamageState = new PlayerOnDamageState(this, stateMachine, "OnDamage");

    }

    private void Start()
    {
        
        Application.targetFrameRate = frameRate;
        
        //assign component
        rb = GetComponent<Rigidbody2D>();
        
        input.EnableGamePlayInputs();
        stateMachine.Initialize(fallState);
        iState = IState.Fragile;

        //Subscribe to enemyAttacking
        EventManager.StartListening<Deflectable>("EnemyAttacking", OnDamage);

    }

    private void Update()
    {
        // set buffer before state update
        InputBufferCtrl.SetBufferOnInput();

        // state update
        stateMachine.currentState.Update();

        //Timer count down
        TimerCountDownCtrl.Update();

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
    }

    void OnGUI()
    {
        GUI.Label(new Rect(200, 200, 200, 200), "playerState: " + stateMachine.currentState.animBoolName);
        GUI.Label(new Rect(200, 220, 200, 200), "deflect timer: " + DeflectCtrl.timer.timer);
    }

    private void PlayerColorStateIndicator()
    {
        switch (iState)
        {
            case IState.Fragile:
                playerPrototypeSprite.color = Color.gray;
                break;
            case IState.Defelct:
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
            case IState.Fragile:
                EventManager.TriggerEvent("PlayerGettingHit", df);
                Debug.Log("PlayerGettingHit");
                break;
            case IState.Defelct:
                EventManager.TriggerEvent("PlayerDeflecting", df);
                Debug.Log("PlayerDeflecting");
                break;
            case IState.Invincible:
                EventManager.TriggerEvent("PlayerEvading", df);
                Debug.Log("PlayerEvading");
                break;
        }
    }
}
