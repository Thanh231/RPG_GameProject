using System.Collections;
using UnityEngine;

public class Player : Entity
{
    
    #region Variable

    public bool isdoubleJump;
    public float inputX { get; private set; }
    public float inputY { get; private set; }
    private SpriteRenderer sr;

    #endregion
    #region Counter  Attack
    [Header("Counter")]
    public Transform attackCounterCheck;
    public float attackCheckRadius;
    
    public float jumpForce;
    public bool isCombo;
    public bool isBusy;
    public Vector2[] attackForce;
    public Vector2 impactSwordReturn;

    public float gravity = 3.5f;

    #endregion
    #region Speed
    [Header("Speed")]
    public float dashDuration;
    public float dashSpeed;
    public SkillManager skill { get; private set; }
    public GameObject sword { get; private set; }

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
    public PlayerAimState aimState { get; private set; }
    public PlayerCatchState catchState { get; private set; }
    public PlayerBlackHoleState blackHoleState { get; private set; }
    public PlayerDeadStats dead {  get; private set; }

    public bool isDead;

    #endregion

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new StateMachine();

        sr = GetComponentInChildren<SpriteRenderer>();

        idel = new PlayerIdelState(this, stateMachine, "Idel");
        move = new PlayerMoveState(this, stateMachine, "Move");
        jump = new PlayerJumpState(this, stateMachine, "Jump");
        air = new PlayerAirStates(this, stateMachine, "Jump");
        dash = new PlayerDashState(this, stateMachine, "Dash");
        slide = new PlayerSlideStats(this, stateMachine, "Slide");
        attack = new PlayerAttackState(this, stateMachine, "Attack");
        jumpWall = new PlayerJumpWall(this, stateMachine, "Jump");
        counterAttack = new Player_Counter_Attack_State(this, stateMachine, "CounterActtack");
        aimState = new PlayerAimState(this, stateMachine, "AimSword");
        catchState = new PlayerCatchState(this, stateMachine, "CatchSword");
        blackHoleState = new PlayerBlackHoleState(this, stateMachine, "Jump");
        dead = new PlayerDeadStats(this, stateMachine, "Dead");
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Iniatial(idel);
        skill = SkillManager.instance;

        stats.damage.AddModify(4);
    }

    protected override void Update()
    {
        base.Update();

        
        stateMachine.state.Update();

        CheckMove();
        inputY = Input.GetAxisRaw("Vertical");

        Dash();
        if(Input.GetKeyDown(KeyCode.E) && !isDead)
        {
            skill.crystalSkill.UseSkill();
        }
        if(skill.attackArroundSkill.CanUseSkill())
        {
            skill.attackArroundSkill.UseSkill();
        }
    }
    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.W) && skill.dash.CanUseSkill() && !IsWall() && !isDead)
        {
            stateMachine.ChangeState(dash);
        }
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
    public void AssignSword(GameObject newSword)
    {
        sword = newSword;
    }
    public void CatchSword()
    {
        stateMachine.ChangeState(catchState);
        Destroy(sword);
    }
    
    public void HideCharacter(bool isTransparent)
    {
        if(isTransparent)
        {
            sr.color = Color.clear;
        }
        else
        { 
            sr.color = Color.white; 
        }
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(dead);
    }
}
