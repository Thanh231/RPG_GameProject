
using UnityEngine;

public class PlayerCatchState : StateActor
{
    private Transform sword;
    public PlayerCatchState(Player _player, StateMachine _stateMachine, string _animString) : base(_player, _stateMachine, _animString)
    {
    }

    public override void Enter()
    {
        base.Enter();
        sword = player.sword.transform;
        if (sword.position.x > player.transform.position.x && player.facingDir == -1)
            player.SetVelocity(0.001f, player.rd.velocityY);
        else if (sword.position.x < player.transform.position.x && player.facingDir == 1)
            player.SetVelocity(-0.001f, player.rd.velocityY);

        player.rd.velocity = new Vector2(player.impactSwordReturn.x * -player.facingDir,player.impactSwordReturn.y);
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
