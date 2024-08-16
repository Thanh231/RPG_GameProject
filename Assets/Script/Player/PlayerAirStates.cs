using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirStates : StateActor
{
    public PlayerAirStates(Player _player, StateMachine _stateMachine, string _animString) : base(_player, _stateMachine, _animString)
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
        if(player.rd.velocityY == 0 || player.IsGround())
        {
            stateMachine.ChangeState(player.idel);
        }
    }
}
