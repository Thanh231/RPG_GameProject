using UnityEngine;

public class PlayerAimState : StateActor
{
    public PlayerAimState(Player _player, StateMachine _stateMachine, string _animString) : base(_player, _stateMachine, _animString)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.rd.velocity = Vector2.zero;
        player.skill.swordSkill.SetBoolDot(true);
    }

    public override void Exit()
    {
        base.Exit();
        player.rd.velocity = Vector2.zero;
    }

    public override void Update()
    {
        base.Update();
        if(Input.GetMouseButtonUp(0))
        {
            stateMachine.ChangeState(player.idel);
        }
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x > player.transform.position.x && player.facingDir == -1)
            player.SetVelocity(0.001f, player.rd.velocityY);
        else if(mousePos.x < player.transform.position.x && player.facingDir == 1)
            player.SetVelocity(-0.001f, player.rd.velocityY);
    }
}
