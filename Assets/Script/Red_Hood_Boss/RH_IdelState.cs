public class RH_IdelState : EnemyState
{
    private RedHood rh_enemy;
    public RH_IdelState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animString, RedHood _rh_enemy) : base(_enemy, _stateMachine, _animString)
    {
        rh_enemy = _rh_enemy;
    }

    public override void Enter()
    {
        base.Enter();
        timeValue = 2f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(timeValue < 0)
        {
            rh_enemy.rhStateMachine.ChangeState(rh_enemy.moveState);
        }
        
    }
}
