using UnityEngine;

public class Skeleton_Battle_State : EnemyState
{
    private GameObject player;
    private Enemy_Skeleton skeleton;
    public Skeleton_Battle_State(Enemy _enemy, EnemyStateMachine _stateMachine, string _animString, Enemy_Skeleton _skeleton) : base(_enemy, _stateMachine, _animString)
    {
        skeleton = _skeleton;
    }
    public override void Enter()
    {
        base.Enter();
        skeleton.speed = 3f;
        player = PlayerManager.instance.player.gameObject;
    }

    public override void Exit()
    {
        base.Exit();
        skeleton.speed = skeleton.defaultSpeed;
    }

    public override void Update()
    {
        base.Update();

        if (enemy.isKnockBack || skeleton.isFreeze) return;
        skeleton.SetVelocity(skeleton.facingDir * skeleton.speed, skeleton.rd.velocityY);
        if (!enemy.IsDetectPlayer())
        {
            stateMachine.ChangeState(skeleton.enemy_move);
        }
       if(Vector2.Distance(player.transform.position, skeleton.transform.position) < 1.5f)
        {
            stateMachine.ChangeState(skeleton.enemy_attack);
        }
    }
}
