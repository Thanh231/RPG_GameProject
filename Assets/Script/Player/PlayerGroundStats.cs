using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundStats : StateActor
{
    public PlayerGroundStats(Player _player, StateMachine _stateMachine, string _animString) : base(_player, _stateMachine, _animString)
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

        if(Input.GetKeyDown(KeyCode.Space) && player.IsGround())
        {
            stateMachine.ChangeState(player.jump);
        }
        if(Input.GetKeyDown(KeyCode.F) && player.IsGround())
        {
            stateMachine.ChangeState(player.attack);
        }
        if(Input.GetMouseButtonDown(0))
        {
            stateMachine.ChangeState(player.counterAttack);
        }
    }
}
