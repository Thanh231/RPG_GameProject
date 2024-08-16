using UnityEngine;

public class Skeleton_Stun_State : EnemyState
{
    Enemy_Skeleton skeleton;
    private float timeStun;
    
    public Skeleton_Stun_State(Enemy _enemy, EnemyStateMachine _stateMachine, string _animString, Enemy_Skeleton _skeleton) : base(_enemy, _stateMachine, _animString)
    {
        this.skeleton = _skeleton;
    }
    public override void Enter()
    {
        base.Enter();
        timeValue = 1f;
        timeStun = 0.3f;
        skeleton.SetVelocity(-skeleton.facingDir * skeleton.stunDir.x, skeleton.stunDir.y);
        skeleton.fx.InvokeRepeating("BlinkRed", 0, 0.1f);
    }

    public override void Exit()
    {
        base.Exit();
        skeleton.fx.Invoke("CancelBlinkRed", 0);
    }
    public override void Update()
    {
        base.Update();
        timeStun -= Time.deltaTime;
        if(timeStun < 0)
        {
            skeleton.rd.velocity = Vector2.zero;
        }
        if (timeValue < 0)
        {
            stateMachine.ChangeState(skeleton.enemy_move);
        }
    }
}
