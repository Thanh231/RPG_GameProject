using UnityEngine;

public class CrystalController : MonoBehaviour
{
    private float lifeTime;
    private Animator anim;
    private bool isExplode;
    private float speedGrow;
    private float speedMove;
    private bool canMove;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void SetUpCrystal(float time, float _speedGrow, float _speedMove,bool _canMove)
    {
        speedGrow = _speedGrow;
        lifeTime = time;
        speedMove = _speedMove;
        canMove = _canMove;

    
    }
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0 && !canMove)
        {
            isExplode = true;
        }
        if(isExplode)
        {
            anim.SetBool("Explode", true);

            transform.localScale = Vector2.Lerp(transform.localScale,new Vector2(2,2),speedGrow * Time.deltaTime);
        }

        Enemy enemy = FindEneny();

        if (enemy != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemy.transform.position, speedMove * Time.deltaTime);
                
            if (Vector2.Distance(transform.position, enemy.transform.position) < 0.5f)
            {
                isExplode = true;
            }
        }
       

    }
    
    public void SelfDestroy() => Destroy(gameObject);
    public void CheckEneneyAroundAtTime()
    {
        if (canMove) return;
        var enemyCollider = Physics2D.OverlapCircleAll(transform.position, 2f);
        foreach (var enemy in enemyCollider)
        {
            Enemy enemyCheck = enemy.GetComponent<Enemy>();
            if (enemyCheck != null)
            {
                enemyCheck.Damage();
            }
        }
        
    }
    public Enemy FindEneny()
    {
        if (canMove) return null;
        var enemies = Physics2D.OverlapCircleAll(transform.position, 8f);
        float minDis = Mathf.Infinity;
        Enemy enemyTemp = null;
        foreach (var enemy in enemies)
        {
            if (enemy.GetComponent<Enemy>() != null)
            {
                float distanceTemp = Vector2.Distance(transform.position, enemy.transform.position);
                if (distanceTemp < minDis && enemy.GetComponent<Enemy>() != enemyTemp)
                {
                    minDis = distanceTemp;
                    enemyTemp = enemy.GetComponent<Enemy>();
                }
            }

        }


        return enemyTemp;
    }
}
