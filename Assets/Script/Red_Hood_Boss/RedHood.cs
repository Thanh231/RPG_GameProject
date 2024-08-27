using UnityEngine;

public class RedHood : Enemy
{
    public EnemyStateMachine rhStateMachine;
    public GameObject endGame;
    public RH_IdelState idelState {  get; private set;}
    public RH_MoveState moveState { get; private set;}
    public RH_AttackState attackState { get; private set;}
    private float timeValue;
    public Vector2 attackImpact;
    private bool isDead;
    protected override void Awake()
    {
        base.Awake();
        rhStateMachine = new EnemyStateMachine();
        idelState = new RH_IdelState(this, rhStateMachine, "Idel", this);
        moveState = new RH_MoveState(this, rhStateMachine, "Run", this);
        attackState = new RH_AttackState(this, rhStateMachine, "Attack", this);
    }
    protected override void Start()
    {
        rhStateMachine.Initial(idelState);
    }
    protected override void Update()
    {
        rhStateMachine.currentState.Update();
        timeValue -= Time.deltaTime;
        if (timeValue < 0 && isDead)
        {
            fx.StartCoroutine(fx.ApplyAlimentForEntity(fx.wasDamage, 2f));
        }
    }
    public bool IsPlayerCheckArround() => Physics2D.OverlapCircle(transform.position, 5f,playerMask);
    public bool IsWallCheck() => Physics2D.Raycast(wallCheck.position, new Vector2(facingDir, 0), wallDistance, groundLayer);
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 5f);
        Gizmos.DrawWireSphere(attackCheck.position, attackRadius);
        Gizmos.DrawLine(wallCheck.position, new Vector2((wallCheck.position.x + facingDir * wallDistance), wallCheck.position.y));

    }
    public void FlipBoss()
    {
        facingDir *= -1;
        transform.localScale = new Vector3(transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
    }
    public override void Die()
    {
        base.Die();
        endGame.SetActive(true);
        isDead = true;
        timeValue = 2f;
        Destroy(gameObject);
        AudioController.Ins.PlaySound(AudioController.Ins.aiDeath);
    }
    
}
