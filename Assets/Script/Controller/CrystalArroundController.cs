using Unity.VisualScripting;
using UnityEngine;

public class CrystalArroundController : MonoBehaviour
{
    private bool rotate;
    private float speed;
    private float maxDistance;
    private Transform center;
    private Vector2 target;
    private bool isExplode;
    private float speedGrow;
    private Animator anim;
    private float timeValue;
    private Player player;
    private int damage;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void SetUpPrefabs(Vector2 _target, float _speed, float _maxDistance, Transform temp,float _speedGrow,float lifeTime,Player _player,int _damage)
    {
        target = _target;
        speed = _speed;
        maxDistance = _maxDistance;
        center = temp;
        speedGrow = _speedGrow;
        timeValue = lifeTime;
        player = _player;
        damage = _damage;
    }

    void Update()
    {
        timeValue -= Time.deltaTime;
        if (Mathf.Abs(transform.localPosition.x) >= maxDistance)
        {
            rotate = true;
        }
        if (rotate)
        {
            transform.RotateAround(center.position, Vector3.forward, speed * Time.deltaTime);

        }
        else
        {

            transform.localPosition = Vector2.MoveTowards(transform.localPosition, target, speed * Time.deltaTime);

        }

        if (isExplode || timeValue < 0)
        {
            anim.SetBool("Explode", true);

            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(2, 2), speedGrow * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            isExplode = true;
            CharacterStats enemy = collision.GetComponent<EnemyStats>();
            enemy.SetBunrnDamge(5);
            enemy.TakeDamage(damage);
            enemy.ApplyAliment(true, false, false);
        }
            
    }
    public void CheckEneneyAroundAtTime()
    {
        var enemyCollider = Physics2D.OverlapCircleAll(transform.position, 2f);
        foreach (var enemy in enemyCollider)
        {
            CharacterStats enemyCheck = enemy.GetComponent<EnemyStats>();
            if (enemyCheck != null)
            {
                enemyCheck.TakeDamage(damage);
            }
        }

    }
    public void SelfDestroy() => Destroy(gameObject);
}
