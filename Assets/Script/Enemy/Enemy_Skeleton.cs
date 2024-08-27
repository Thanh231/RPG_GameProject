using UnityEngine;
public class Enemy_Skeleton : Enemy
{

    #region Skeneton State
    private EnemyStateMachine enemyStateMachine;
    public Skeleton_Idle_State enemy_idel {  get; private set; }
    public Skeleton_Move_State enemy_move { get; private set; }
    public Skeleton_Battle_State enemy_battle { get; private set; }
    public Skeleton_Attack_State enemy_attack { get; private set; }
    public Skeleton_Stun_State enemy_stun { get; private set; }

    public SkeletonDeadStats dead {  get; private set; }

    #endregion

    
    protected override void Awake()
    {
        base.Awake();

        enemyStateMachine = new EnemyStateMachine();

        enemy_idel = new Skeleton_Idle_State(this, enemyStateMachine, "Idel", this);
        enemy_move = new Skeleton_Move_State(this, enemyStateMachine, "Move", this);
        enemy_battle = new Skeleton_Battle_State(this, enemyStateMachine, "Move", this);
        enemy_attack = new Skeleton_Attack_State(this, enemyStateMachine, "Attack", this);
        enemy_stun = new Skeleton_Stun_State(this, enemyStateMachine, "Stun", this);
        dead = new SkeletonDeadStats(this, enemyStateMachine, "Idel", this);
    }

    protected override void Start()
    {
        base.Start();
        enemyStateMachine.Initial(enemy_idel);
    }

    protected override void Update()
    {
        base.Update();
        enemyStateMachine.currentState.Update();
        
    }
    public override bool CanBeStun()
    {
        if(canbeStun)
        {
            enemyStateMachine.ChangeState(enemy_stun);
            CloseCounterAttackWindow();
            return true;
        }
        return false;
    }

    public override void Die()
    {
        base.Die();
        enemyStateMachine.ChangeState(dead);
        
    }
}
