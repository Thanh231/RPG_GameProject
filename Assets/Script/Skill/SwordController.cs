using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    
    private Animator anim;
    private Rigidbody2D rd;
    private Player player;
    private CircleCollider2D cd;
    public bool canRotate = true;
    private Player Player;
    private float returnSpeed;

    private int piereceAmount;

    private float freezeTime;
    private bool isReturning;


    [Header("Bounce Info")]
    private bool isBounce;
    private int amountBounce;
    private List<Transform> enemyTrans = new List<Transform>();
    private int targetIndex;

    [Header("Spin Info")]
    private bool isSpin;
    private float maxDistance;
    private bool wasStop;
    private float spinDuration;
    private float spinTime;

    private float hitTime;
    private float hitCoolDown;
    private float spinDir;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rd = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
    }
    private void Update()
    {
        if (Vector2.Distance(player.transform.position,transform.position) > 20f)
        {
            StopSpin();
        }
            if (canRotate)
        {
            transform.right = rd.velocity;
        }
        if (isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, player.transform.position) < 0.5f)
            {
                player.CatchSword();
            }
        }

        BounceSword();
        if(isSpin)
        {
            if (Vector2.Distance(player.transform.position, transform.position) > maxDistance && !wasStop)
            {
                
                StopSpin();
            }
            if (wasStop)
            {
                spinTime -= Time.deltaTime;
               
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + spinDir, transform.position.y), 2f * Time.deltaTime);
                if (spinTime < 0)
                {
                    isSpin = false;
                    isReturning = true;
                }
                hitTime -= Time.deltaTime;
                if(hitTime < 0)
                {
                    hitTime = hitCoolDown;
                    var enemies = Physics2D.OverlapCircleAll(transform.position, 1f);
                    foreach(var enemy in enemies)
                    {
                        EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();   
                        if(enemyStats != null)
                        {
                            player.stats.DoDamage(enemyStats);
                        }
                    }

                }
            }
        }
    }

    private void StopSpin()
    {
        wasStop = true;
        rd.constraints = RigidbodyConstraints2D.FreezeAll;
        spinTime = spinDuration;
    }

    private void BounceSword()
    {
        if (isBounce && enemyTrans.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemyTrans[targetIndex].position, Time.deltaTime * 10f);

            if (Vector2.Distance(transform.position, enemyTrans[targetIndex].position) < 0.1f)
            {
                Enemy enemy = enemyTrans[targetIndex].GetComponent<Enemy>();
                TakeDamage_Frezee(enemy);

                targetIndex++;
                amountBounce--;
                if (amountBounce <= 0)
                {
                    isReturning = true;
                    isBounce = false;
                }
                if (targetIndex >= enemyTrans.Count)
                {
                    targetIndex = 0;
                }
            }
        }
    }

    public void ReTurnSword()
    {
        
        isReturning = true;
        rd.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = null;
    }
    public void SetUpSword(Vector2 dir,float gravity, Player _player,float _freezeTime, float _returnSpeed,float _hitCoolDown)
    {
        player = _player;
        rd.velocity = dir;
        rd.gravityScale = gravity;
        anim.SetBool("Rotate",true);
        freezeTime = _freezeTime;
        returnSpeed = _returnSpeed;
        hitCoolDown = _hitCoolDown;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReturning)
        {
            return;
        }
        spinDir = Mathf.Clamp(rd.velocity.x, -1 ,1);

        if(collision.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            TakeDamage_Frezee(enemy);
        }


        if (collision.GetComponent<Enemy>() != null)
        {
            if (isBounce && enemyTrans.Count <= 0)
            {
                var enemies = Physics2D.OverlapCircleAll(transform.position, 10f);
                foreach (var enemy in enemies)
                {
                    if (enemy.GetComponent<Enemy>() != null)
                    {
                        enemyTrans.Add(enemy.transform);
                    }
                }
            }
        }

        Stuck(collision);
    }

    private void TakeDamage_Frezee(Enemy enemy)
    {
        CharacterStats _enemyStats = enemy.GetComponent<EnemyStats>();
        player.stats.DoDamage(_enemyStats);
        enemy.StartCoroutine("FrezeTimer", freezeTime);
    }

    private void Stuck(Collider2D collision)
    {
        if (piereceAmount > 0 && collision.GetComponent<Enemy>() != null)
        {
            piereceAmount--;
            return;
        }
        if(isSpin)
        {
            StopSpin();
            return;
        }
        
        canRotate = false;
        cd.enabled = false;
        rd.isKinematic = true;
        rd.constraints = RigidbodyConstraints2D.FreezeAll;

        if (isBounce && enemyTrans.Count > 0)
        {
            return;
        }
        anim.SetBool("Rotate", false);
        transform.parent = collision.transform;
    }

    public void SetUpBounce(int _amountBounce,bool _isBounce)
    {
        amountBounce = _amountBounce;
        isBounce = _isBounce;
    }
    public void SetUpPirece(int _pireceAmount)
    {
        piereceAmount = _pireceAmount;
    }
    public void SetUpSpin(float _spinDuration, float _maxDistance,bool _isSpin, float _hitDuration)
    {
        spinDuration = _spinDuration;
        maxDistance = _maxDistance;
        isSpin = _isSpin;
        hitCoolDown = _hitDuration;
    }
}
