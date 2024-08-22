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
            if (hit.GetComponent<Enemy>() != null)
            {
                Enemy enemy = hit.GetComponent<Enemy>();
                enemy.Damage();
            }
        }
    }
}
