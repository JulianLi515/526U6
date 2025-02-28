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
    //TODO:Discard when implemnt UI
    public enum BattleInfo
    {
        Peace,
        Grab,
        Deflect,
        Doge,
        Attack,
        Hit
        
    }

    [Header("Stats")]
    public SpriteRenderer playerPrototypeSprite;
    public int frameRate = 60;
    public float gravityScale;
    public bool ladderCheck;
    public SpriteRenderer Bleeding;

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
    public GameObject deflectBox;
    public float deflectDuration;
    public Timer deflectTimer;
    public float deflectHitboxOffsetX;
    public float deflectHitboxOffsetY;
    public float deflectJumpSpeed;

    [Header("Grab")]
    public GameObject grabBox;
    public float grabDuration;
    public Timer grabTimer;
    public float grabHitboxOffsetX;
    public float grabHitboxOffsetY;

    [Header("BattleInfo")]
    public BattleInfo battleInfo;
    public GameObject trigger;

    [Header("Roll")]
    public float rollSpeed;
    public float rollCoolDown;
    public Timer rollCoolDownTimer;
    public float rollDuration;
    public Timer rollDurationTimer;
    public float rollInvincibleDuration;


    [Header("HitBox")]
    public GameObject InvincibleBox;
    public GameObject HitBox;

    [Header("DamagedPenalty")]
    public float knockbackThreshold;
    public float knockbackForceMultiplier;
    public float controlLossDuration;
    public float KnockBackDuration;
    public Timer KnockBackTimer;

    [Header("Weapon")]
    public PlayerWeapon weapon1;
    public int weapon1Ammo;
    public PlayerWeapon weapon2;
    public int weapon2Ammo;
    public PlayerWeapon currentWeapon;
    public int currentWeaponAmmo;
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
        deflectState = new PlayerDeflectState(this, stateMachine, "Deflect");
        grabState = new PlayerGrabState(this, stateMachine, "Grab");
        deflectRewardState = new PlayerDeflectRewardState(this, stateMachine, "DeflectReward");
        grabRewardState = new PlayerGrabRewardState(this, stateMachine, "GrabReward");
        damagePenaltyState = new PlayerDamagedPenalyState(this, stateMachine, "DamagePenalty");
        ladderMoveState = new PlayerLadderMoveState(this, stateMachine, "LadderMove");
        attackState = new PlayerAttackState(this, stateMachine, "Skill");

        deflectBox.SetActive(false);
        grabBox.SetActive(false);
        InvincibleBox.SetActive(false);

        battleInfo = BattleInfo.Peace;

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



    }

    private void Update()
    {
        // set buffer before state update
        InputBufferCtrl.SetBufferOnInput();

        //Timer count down
        TimerCountDownCtrl.Update();

        //battbleTriggered StateChange
        if(battleInfo!= BattleInfo.Peace)
        {
            OnBattle();
        }

        // stateMachine update second; aleast 0 frame on Playerstate.update() is called()
        stateMachine.currentState.Update();

        //Debug
        rawSpeed = rb.linearVelocity;


    }

    private void LateUpdate()
    {
        stateMachine.currentState.LateUpdate();
    }

    private void OnDestroy()
    {
        
    }

    void OnGUI()
    {
        // Set the font size
        //TODO:Discard when implemnt UI
        GUIStyle bigFontStyle = new GUIStyle(GUI.skin.label);
        bigFontStyle.fontSize = 16;
        GUI.Label(new Rect(200, 100, 200, 200), "playerState: " + stateMachine.currentState.animBoolName, bigFontStyle);
        GUI.Label(new Rect(200, 120, 200, 200), "CurrentWeapon Ammo " + currentWeaponAmmo, bigFontStyle);
    }

    private void OnDrawGizmos()
    {
        //LevelCollisionCtrl.draw();
    }

    private void OnBattle()
    {
        switch (battleInfo)
        {
            case BattleInfo.Grab:
                stateMachine.ChangeState(grabRewardState);
                break;
            case BattleInfo.Deflect:
                stateMachine.ChangeState(deflectRewardState);
                break;
            case BattleInfo.Doge:
                //Invinsible do nothing
                break;
            case BattleInfo.Hit:
                stateMachine.ChangeState(damagePenaltyState);
                break;
            case BattleInfo.Attack:
                //TODO:Implement Attack reward state
                break;
        }
    }

    public void GoInvincible(float Duration)
    {
        StopCoroutine(InvincibleCoroutine(Duration));
        StartCoroutine(InvincibleCoroutine(Duration));
    }

    IEnumerator InvincibleCoroutine(float Duration)
    {
        InvincibleBox.SetActive(true);
        HitBox.SetActive(false);
        yield return new WaitForSeconds(Duration);
        InvincibleBox.SetActive(false);
        HitBox.SetActive(true);
        battleInfo = BattleInfo.Peace;
    }

}
