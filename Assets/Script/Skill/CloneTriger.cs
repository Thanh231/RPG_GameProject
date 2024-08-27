using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneTriger : MonoBehaviour
{

    public Transform attackCheck;
    public float attackRadius;
    public CloneController cloneController;
    public void Trigger()
    {
        cloneController.timeValue = - 1f;
    }
    public void AttackEnemy()
    {
        var collider2D = Physics2D.OverlapCircleAll(attackCheck.position,attackRadius);
        foreach (var hit in collider2D)
        {
            if (hit.GetComponent<EnemyStats>() != null)
            {
                EnemyStats enemy = hit.GetComponent<EnemyStats>();
                PlayerManager.instance.player.stats.DoDamage(enemy);
            }
        }
    }
}
