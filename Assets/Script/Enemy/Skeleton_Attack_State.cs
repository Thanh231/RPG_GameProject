using UnityEngine;
public class Skeleton_Attack_State : EnemyState
{
    Enemy_Skeleton skeleton;
    public Skeleton_Attack_State(Enemy _enemy, EnemyStateMachine _stateMachine, string _animString, Enemy_Skeleton _skeleton) : base(_enemy, _stateMachine, _animString)
    {
        skeleton = _skeleton;
    }

    public override void Enter()
    {
        
        base.Enter();
        timeValue = 1.8f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (enemy.isKnockBack || skeleton.isFreeze) return;

        skeleton.rd.velocity = Vector2.zero;
        if (timeValue < 0)
        {
            stateMachine.ChangeState(skeleton.enemy_idel);
        }
    }
}
