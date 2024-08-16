public class Skeleton_Check_KnockBack_State : EnemyState
{
 
    public Enemy_Skeleton skeleton;
    public Skeleton_Check_KnockBack_State(Enemy _enemy, EnemyStateMachine _stateMachine, string _animString, Enemy_Skeleton _skeleton) : base(_enemy, _stateMachine, _animString)
    {
        skeleton = _skeleton;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

    }
}
