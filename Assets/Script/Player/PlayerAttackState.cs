
using UnityEngine;

public class PlayerAttackState : StateActor
{
    private int combo;
    private float comboCoolDown;
    public PlayerAttackState(Player _player, StateMachine _stateMachine, string _animString) : base(_player, _stateMachine, _animString)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        if (combo > 2 || Time.time >= comboCoolDown + 1.5f)
        {
            combo = 0;
        }
        player.anim.SetInteger("combo", combo);
        timeValue = 0.1f;
        player.StartAvoidMove();
        player.transform.position += new Vector3(player.attackForce[combo].x * player.transform.right.x, player.attackForce[combo].y) * Time.deltaTime;

    }

    public override void Exit()
    {
        base.Exit();
        player.isCombo = false;
        combo++;
        comboCoolDown = Time.time;
  
    }

    public override void Update()
    {
        base.Update();
        if (player.isCombo)
        {
            stateMachine.ChangeState(player.idel);
        }
        if(timeValue < 0)
        {
            player.rd.velocity = Vector2.zero;
        }
        
    }
}
