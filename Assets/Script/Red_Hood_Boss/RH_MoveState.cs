using UnityEngine;
public class RH_MoveState : EnemyState
{
    private RedHood rh_enemy;
    public RH_MoveState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animString, RedHood redHood) : base(_enemy, _stateMachine, _animString)
    {
        rh_enemy = redHood;
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
        rh_enemy.rd.velocity = new Vector2(rh_enemy.facingDir * rh_enemy.speed, rh_enemy.rd.velocityY);

        if(rh_enemy.IsWallCheck())
        {
            rh_enemy.FlipBoss();
            rh_enemy.rhStateMachine.ChangeState(rh_enemy.idelState);
        }

        if(rh_enemy.IsPlayerCheckArround()) 
        {
           rh_enemy.rhStateMachine.ChangeState(rh_enemy.attackState);
        }
    }
}
