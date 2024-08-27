using UnityEngine;
public class Skeleton_Idle_State : Skeleton_Ground_State
{
    public Skeleton_Idle_State(Enemy _enemy, EnemyStateMachine _stateMachine, string _animString, Enemy_Skeleton _skeleton) : base(_enemy, _stateMachine, _animString, _skeleton)
    {
    }

    public override void Enter()
    {
        base.Enter();
        timeValue = skeleton.idleTime;
        skeleton.rd.velocity = Vector2.zero;

    }

    public override void Exit()
    {
        base.Exit();
        
    }

    public override void Update()
    {
        base.Update();
        if(!skeleton.isKnockBack)
        {
            skeleton.rd.velocity = Vector2.zero;
        }
        if (timeValue < 0)
        {
            stateMachine.ChangeState(skeleton.enemy_move);
        }
    }
}
