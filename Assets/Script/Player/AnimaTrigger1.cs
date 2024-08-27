using UnityEngine;

public class AnimaTrigger1 : MonoBehaviour
{
    public Player player;
    public void Trigger()
    {
        player.isCombo = true;
    }
    public void AttackEnemy()
    {
        var collider2D = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackRadius);
        foreach(var hit in  collider2D)
        {
            if(hit.GetComponent<Enemy>() != null)
            {

                CharacterStats target = hit.GetComponent<CharacterStats>();

                player.stats.DoDamage(target);

            }
        }
    }
    public void ThrowSword()
    {
        player.skill.swordSkill.CreateSword();
    }
    public void CatchSword()
    {
        player.stateMachine.ChangeState(player.idel);
    }
}
