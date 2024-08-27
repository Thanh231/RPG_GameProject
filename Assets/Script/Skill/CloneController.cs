using UnityEngine;

public class CloneController : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator anim;
    public float timeValue;
    private float lossingSpped = 1f;
    public Transform attackCheck;
    
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }
    private void Update()
    {
        timeValue -= Time.deltaTime;
        
        if(timeValue < 0)
        {
            sr.color = new Color(1,1,1,sr.color.a - (Time.deltaTime * lossingSpped));
        }
        if(sr.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void SetClone(Transform _transform, float _cloneTime, bool canAttack,Vector3 offset)
    {
        if(canAttack)
        {
            anim.SetInteger("Attack", Random.Range(0, 3));
        }
        transform.position = _transform.position + offset;
        timeValue = _cloneTime;

        SetDirForClone();
    }
    public void SetDirForClone()
    {
        var enemies = Physics2D.OverlapCircleAll(attackCheck.position, 25f);

        float minDis = Mathf.Infinity;
        Enemy enemyTemp = null;

        foreach (var enemy in enemies)
        {
            if(enemy.GetComponent<Enemy>() != null)
            {
                float distanceTemp = Vector2.Distance(transform.position, enemy.transform.position);
                if (distanceTemp < minDis && enemy.GetComponent<Enemy>() != enemyTemp)
                {
                    minDis = distanceTemp;
                    enemyTemp = enemy.GetComponent<Enemy>();
                }
            }
            
        }
        if(enemyTemp != null)
        {
            if(enemyTemp.transform.position.x < transform.position.x)
            {
                transform.Rotate(0, 180, 0);
            }
        }
    }
    
}
