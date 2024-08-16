using UnityEngine;

public class StateActor 
{
    public StateMachine stateMachine;
    public Player player;
    public string animString;
    public float timeValue;
    
    public StateActor(Player _player, StateMachine _stateMachine,string _animString)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animString = _animString;
    }
    public virtual void Enter()
    {
        player.anim.SetBool(animString, true);   
    }
    public virtual void Update()
    {
        timeValue -= Time.deltaTime;
        player.anim.SetFloat("yVelocity",player.rd.velocityY);
        if(!player.IsGround() && player.IsWall() )
        {
            stateMachine.ChangeState(player.slide);
        }
    }
    public virtual void Exit()
    {
        player.anim.SetBool(animString, false);   
    }

}
