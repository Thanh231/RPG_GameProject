using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : StateActor
{
    
    public PlayerJumpState(Player _player, StateMachine _stateMachine, string _animString) : base(_player, _stateMachine, _animString)
    {
    }

    public override void Enter()
    {
        player.isdoubleJump = true;
        base.Enter();
        player.rd.velocity = new Vector2(player.rd.velocityX, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
        
    }

    public override void Update()
    {
        base.Update();
        if(player.rd.velocityY < 0 && !player.IsWall())
        {
            stateMachine.ChangeState(player.air);
        }
        if(player.isdoubleJump && Input.GetKeyDown(KeyCode.Space))
        {
            player.rd.velocity = new Vector2(player.rd.velocityX, player.jumpForce);
            player.isdoubleJump = false;
        }
        if(player.IsWall())
        {
            stateMachine.ChangeState(player.slide);
        }
    }
    
}
