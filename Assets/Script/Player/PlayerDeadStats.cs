using UnityEngine;

public class PlayerDeadStats : StateActor
{
    public PlayerDeadStats(Player _player, StateMachine _stateMachine, string _animString) : base(_player, _stateMachine, _animString)
    {

    }

    public override void Enter()
    {
        base.Enter();
        player.isDead = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.rd.velocity = Vector2.zero;
    }
}
