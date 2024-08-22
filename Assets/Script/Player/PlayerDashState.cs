using UnityEngine;
public class PlayerDashState : StateActor
{
    public PlayerDashState(Player _player, StateMachine _stateMachine, string _animString) : base(_player, _stateMachine, _animString)
    {

    }

    public override void Enter()
    {
        base.Enter();
        timeValue = player.dashDuration;
        if(player.inputX == 0)
        {
            player.rd.velocity = new Vector2(player.dashSpeed * player.facingDir, 0);
        }
        else
        {
            player.rd.velocity = new Vector2(player.dashSpeed * player.inputX, 0);
        }
            
        player.rd.gravityScale = 0;
        player.skill.clone.CreateCloneAtStart(player.transform, Vector2.zero);
    }

    public override void Exit()
    {
        base.Exit();
        player.rd.gravityScale = player.gravity;
        player.rd.velocity = new Vector2(0, player.rd.velocity.y);
        player.skill.clone.CreateCloneAtEnd(player.transform, Vector2.zero);
    }

    public override void Update()
    {
        base.Update();
        if (timeValue < 0 && player.IsGround())
        {
            stateMachine.ChangeState(player.idel);
        }
        else if (timeValue < 0 && !player.IsGround())
        {
            stateMachine.ChangeState(player.air);
        }
    }
}
