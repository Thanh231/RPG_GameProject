using System.Collections;
using UnityEngine;

public class Player : Entity
{
    #region Variable
    public bool isdoubleJump;
    public float inputX;

    [Header("Counter")]
    public Transform attackCounterCheck;
    public float attackCheckRadius;
    public float inputY { get; private set; }
    public float jumpForce;
    public float dashCooldown;
    public bool isCombo;
    public bool isBusy;
    public Vector2[] attackForce;

    public float gravity = 3.5f;

    [Header("Speed")]
   
    private float timevalue;
    public float dashSpeed;

    #endregion

    #region States
    public StateMachine stateMachine {  get; private set; }

    public PlayerIdelState idel { get; private set; }
    public PlayerMoveState move { get; private set; }
    public PlayerJumpState jump { get; private set; }
    public PlayerAirStates air { get; private set; }
    public PlayerDashState dash { get; private set; }
    public PlayerSlideStats slide { get; private set; }
    public PlayerAttackState attack { get; private set; }
    public PlayerJumpWall jumpWall { get; private set; }
    public Player_Counter_Attack_State counterAttack { get; private set; }
    #endregion
    
    protected override void Awake()
    {
        base.Awake();

        stateMachine = new StateMachine();
        idel = new PlayerIdelState(this, stateMachine, "Idel");
        move = new PlayerMoveState(this, stateMachine, "Move");
        jump = new PlayerJumpState(this, stateMachine, "Jump");
        air = new PlayerAirStates(this, stateMachine, "Jump");
        dash = new PlayerDashState(this, stateMachine, "Dash");
        slide = new PlayerSlideStats(this, stateMachine, "Slide");
        attack = new PlayerAttackState(this, stateMachine, "Attack");
        jumpWall = new PlayerJumpWall(this, stateMachine, "Jump");
        counterAttack = new Player_Counter_Attack_State(this, stateMachine, "CounterActtack");
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Iniatial(idel);
    }

    protected override void Update()
    {
        base.Update();

        timevalue -= Time.deltaTime;
        stateMachine.state.Update();

        CheckMove();
        inputY = Input.GetAxisRaw("Vertical");

        Dash();
    }
    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Z) && timevalue < 0 && !IsWall())
        {
            stateMachine.ChangeState(dash);
            timevalue = dashCooldown;
            StartCoroutine(DashWaiting());
        }
    }
    IEnumerator DashWaiting()
    {
        rd.gravityScale = 0;
        yield return new WaitForSeconds(dashCooldown);
        rd.gravityScale = gravity;
    }

    IEnumerator AvoidMove(float second)
    {
        // this function avoid character move betweent attack 
        isBusy = true;
        yield return new WaitForSeconds(second);
        isBusy = false;
    }
    public void StartAvoidMove()
    {
        StartCoroutine(AvoidMove(0.8f));
    }
    public void CheckMove()
    {
        if (!isBusy)
        {
            inputX = Input.GetAxisRaw("Horizontal");
        }
        else
        {
            inputX = 0;
        }
    }
}
