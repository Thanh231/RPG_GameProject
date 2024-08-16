using UnityEngine;
public class PlayerDashState : StateActor
{
    public PlayerDashState(Player _player, StateMachine _stateMachine, string _animString) : base(_player, _stateMachine, _animString)
    {

    }

    public override void Enter()
    {
        base.Enter();
        timeValue = player.dashCooldown;
        player.rd.velocity = new Vector2(player.dashSpeed,player.rd.velocity.y) * (Vector2)player.transform.right;
    }

    public override void Exit()
    {
        base.Exit();
        player.rd.velocity = new Vector2(0, player.rd.velocity.y);
    }

    public override void Update()
    {
        base.Update();
        if(timeValue < 0 && player.IsGround())
        {
            stateMachine.ChangeState(player.idel);
        }
        else if (timeValue < 0 && !player.IsGround())
        {
            stateMachine.ChangeState(player.air);
        }
    }
}
