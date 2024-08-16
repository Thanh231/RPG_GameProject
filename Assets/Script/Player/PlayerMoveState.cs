public class PlayerMoveState : PlayerGroundStats
{
    public PlayerMoveState(Player _player, StateMachine _stateMachine, string _animString) : base(_player, _stateMachine, _animString)
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

        if (player.inputX == 0 || player.IsWall())
        {
            stateMachine.ChangeState(player.idel);
        }
    }
}
