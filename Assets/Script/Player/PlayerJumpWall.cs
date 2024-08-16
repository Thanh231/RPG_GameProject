using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpWall : StateActor
{
    public PlayerJumpWall(Player _player, StateMachine _stateMachine, string _animString) : base(_player, _stateMachine, _animString)
    {

    }

    public override void Enter()
    {
        base.Enter();
        timeValue = 0.4f;
        player.SetVelocity(5 * -player.facingDir ,player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (timeValue < 0)
        {
            stateMachine.ChangeState(player.air);
        }
    }
}
