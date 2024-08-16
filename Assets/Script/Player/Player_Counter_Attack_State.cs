using UnityEngine;
public class Player_Counter_Attack_State : StateActor
{
    public Player_Counter_Attack_State(Player _player, StateMachine _stateMachine, string _animString) : base(_player, _stateMachine, _animString)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.rd.velocity = Vector2.zero;
        timeValue = 1f;
    }

    public override void Exit()
    {
        base.Exit();
        player.anim.SetBool("SuccessFullCounterAttack", false);
    }

    public override void Update()
    {
        base.Update();
        var enemy = Physics2D.OverlapCircleAll(player.attackCounterCheck.position, player.attackCheckRadius);
        foreach (var hit in enemy) 
        {
            var hitEnemy = hit.GetComponent<Enemy>();
            if (hitEnemy != null)
            {
                if(hitEnemy.CanBeStun())
                {
                    timeValue = 1f;
                    player.anim.SetBool("SuccessFullCounterAttack",true);
                }
            }
        }
        if(timeValue < 0)
        {
            stateMachine.ChangeState(player.idel);
        }
    }
}
