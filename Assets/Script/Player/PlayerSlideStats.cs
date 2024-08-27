using UnityEngine;

public class PlayerSlideStats : StateActor
{
    public PlayerSlideStats(Player _player, StateMachine _stateMachine, string _animString) : base(_player, _stateMachine, _animString)
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

        player.SetVelocity(player.inputX * player.speed, player.rd.velocityY);
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            stateMachine.ChangeState(player.jumpWall);
            return;
        }

        if (!player.IsGround() && !player.IsWall())
        {
            stateMachine.ChangeState(player.air);

        }

        if (player.IsGround())
        {
            stateMachine.ChangeState(player.idel);
        }
        
        if (player.inputY == 0)
        {
            player.SetVelocity(0, 0.7f * player.rd.velocityY);
        }
        else if(player.inputY != 0)
        {
            player.SetVelocity(0,player.rd.velocityY);
        }

        
        
    }
}
