using UnityEngine;

public class PlayerIdelState : PlayerGroundStats
{
    public PlayerIdelState(Player _player, StateMachine _stateMachine, string _animString) : base(_player, _stateMachine, _animString)
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

        player.SetVelocity(player.inputX * player.speed, player.rd.velocity.y);

        if(player.inputX != 0 && !player.IsWall() && !player.isBusy && player.IsGround())
        {
            stateMachine.ChangeState(player.move);
        }
    }
    
}
