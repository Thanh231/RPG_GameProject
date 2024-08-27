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
        if (player.isDead) return;

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGround())
        {
            stateMachine.ChangeState(player.jump);
        }
        if(Input.GetKeyDown(KeyCode.F) && player.IsGround())
        {
            AudioController.Ins.PlaySound(AudioController.Ins.sword);
            stateMachine.ChangeState(player.attack);
        }
        if(Input.GetKeyDown(KeyCode.Q) && !player.isDead && player.counterTimer < 0)
        {
            stateMachine.ChangeState(player.counterAttack);
            player.counterTimer = player.counterCoolDowm;
        }
        if(Input.GetMouseButtonDown(0) && CheckSword())
        {
            stateMachine.ChangeState(player.aimState);
        }
        if(Input.GetKeyDown(KeyCode.R) && !player.isDead && player.skill.blackHoleSkill.CanUseSkill() && player.skill.blackHoleSkill.canUseBlackHole)
        {
            stateMachine.ChangeState(player.blackHoleState);
        }
    }
    public bool CheckSword()
    {
        if(!player.sword)
        {
            return true;
        }
        player.sword.GetComponent<SwordController>().ReTurnSword();
        return false;
    }
}
