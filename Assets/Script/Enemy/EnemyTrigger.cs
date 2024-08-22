using UnityEngine;

public class EnemyTrigger : MonoBehaviour 
{
    public Enemy_Skeleton skeleton;
   private void AttackPlayer()
   {
        var collider2D = Physics2D.OverlapCircleAll(skeleton.attackCheck.position, skeleton.attackRadius);
        foreach (var hit in collider2D)
        {
            if (hit.GetComponent<Player>() != null)
            {
                CharacterStats playerTarget = hit.GetComponent<CharacterStats>();

                skeleton.stats.DoDamage(playerTarget);
            }
        }
   }
    private void OnAttackCounterWindow() => skeleton.OpenCounterAttackWindow();
    private void CloseAttackCounterWindow() => skeleton.CloseCounterAttackWindow();
}
