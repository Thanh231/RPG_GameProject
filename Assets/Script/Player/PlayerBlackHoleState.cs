using UnityEngine;

public class PlayerBlackHoleState : StateActor
{
    private bool usedSkill;
    private float flytime = 0.15f;
    public PlayerBlackHoleState(Player _player, StateMachine _stateMachine, string _animString) : base(_player, _stateMachine, _animString)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.rd.gravityScale = 0;
        timeValue = flytime;
        
    }

    public override void Exit()
    {
        base.Exit();
        player.rd.gravityScale = player.gravity;
        usedSkill = false;
    }

    public override void Update()
    {
        base.Update();
        if(timeValue > 0)
        {
            player.rd.velocity = new Vector2(0, 15f);
        }
        else if(timeValue < 0) 
        {
            player.rd.velocity = new Vector2(0, -0.01f);
            if(!usedSkill)
            {
                player.skill.blackHoleSkill.UseSkill();
                usedSkill = true;
            }
        }
        if(player.skill.blackHoleSkill.CheckCompleteAttack())
        {
            stateMachine.ChangeState(player.air);
        }
    }
}
