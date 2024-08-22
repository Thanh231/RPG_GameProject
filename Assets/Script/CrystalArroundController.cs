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
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void SetUpPrefabs(Vector2 _target, float _speed, float _maxDistance, Transform temp,float _speedGrow,float lifeTime)
    {
        target = _target;
        speed = _speed;
        maxDistance = _maxDistance;
        center = temp;
        speedGrow = _speedGrow;
        timeValue = lifeTime;
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
            collision.GetComponent<Enemy>().Damage();
        }
            
    }
    public void CheckEneneyAroundAtTime()
    {
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
    public void SelfDestroy() => Destroy(gameObject);
}
