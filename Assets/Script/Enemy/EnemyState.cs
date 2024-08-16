using UnityEngine;

public class EnemyState 
{
    public float timeValue;
    public  Enemy enemy;
    public EnemyStateMachine stateMachine;
    public string animString;
    public EnemyState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animString)
    {
        this.enemy = _enemy;
        this.stateMachine = _stateMachine;
        this.animString = _animString;
    }
    public virtual void Enter()
    {

    }
    public virtual void Exit()
    {
        enemy.anim.SetBool(animString, false);
    }
    public virtual void Update()
    {
        enemy.anim.SetBool(animString, true);
        timeValue -= Time.deltaTime;
        
    }
}
