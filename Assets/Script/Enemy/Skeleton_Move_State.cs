public class Skeleton_Move_State : Skeleton_Ground_State
{
    public Skeleton_Move_State(Enemy _enemy, EnemyStateMachine _stateMachine, string _animString, Enemy_Skeleton _skeleton) : base(_enemy, _stateMachine, _animString, _skeleton)
    {
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
        skeleton.SetVelocity(skeleton.facingDir * skeleton.speed,skeleton.rd.velocityY);
        if (!skeleton.IsGround() || skeleton.IsWall())
        {
            skeleton.SetVelocity(-0.01f * skeleton.facingDir, 0);
            stateMachine.ChangeState(skeleton.enemy_idel);
        }

    }
}
