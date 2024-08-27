using UnityEngine;

public class RH_AttackState : EnemyState
{
    private RedHood rh_enemy;

    public RH_AttackState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animString,RedHood _redhood) : base(_enemy, _stateMachine, _animString)
    {
        rh_enemy = _redhood;
    }

    public override void Enter()
    {
        base.Enter();
        rh_enemy.transform.position += new Vector3(rh_enemy.facingDir * rh_enemy.attackImpact.x,rh_enemy.attackImpact.y) * Time.deltaTime;
        timeValue = 2;
    }
    public override void Exit()
    {
        base.Exit();
        rh_enemy.rd.velocity = Vector2.zero;
    }


    public override void Update()
    {
        base.Update();
        if(timeValue < 0)
        {
            rh_enemy.rhStateMachine.ChangeState(rh_enemy.idelState);
        }
    }
}
