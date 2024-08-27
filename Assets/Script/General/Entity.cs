using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public CharacterStats stats {  get; private set; }
    #region Collision Check
    public float attackDistance;
    public float facingDir;
    public LayerMask playerMask;

    public Transform attackCheck;
    public float attackRadius;
    public EnetityFx fx { get; private set; }

    [Header("Knock Back")]
    public Vector2 knockBackDir;
    public float knockBackTime;
    public bool isKnockBack;
    #endregion

    #region Component
    public Animator anim { get; private set; }
    public Rigidbody2D rd { get; private set; }
    public LayerMask groundLayer;
    private bool isFlip = true;

    public float speed;
    public GameObject thunderPrefabs;

    #endregion

    #region Collision
    [Header("Collision Check")]
    public Transform groundCheck;
    public float groundDistance;
    public Transform wallCheck;
    public float wallDistance;
    #endregion

    public System.Action onFlip;

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rd = GetComponent<Rigidbody2D>();
        fx = GetComponent<EnetityFx>();
        
        stats = GetComponent<CharacterStats>();
    }
    protected virtual void Start()
    {
    }
    protected virtual void Update()
    {
    }
    public void DamageEffect()
    {
        fx.StartCoroutine(fx.Flash());
        StartCoroutine(KnockBack());
    }
    public IEnumerator KnockBack()
    {
        isKnockBack = true;

        rd.velocity = new Vector2(knockBackDir.x * -facingDir, knockBackDir.y);

        yield return new WaitForSeconds(knockBackTime);

        isKnockBack = false;
    }
    #region Check Physic

    public bool IsGround() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistance, groundLayer);
    public bool IsWall() => Physics2D.Raycast(wallCheck.position, new Vector2(facingDir,0), wallDistance, groundLayer);
    public bool IsDetectPlayer() => Physics2D.Raycast(wallCheck.position, new Vector2(facingDir,0), attackDistance ,playerMask);

    #endregion

    #region Collision

    /*public void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x,groundCheck.position.y - groundDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2((wallCheck.position.x + facingDir*wallDistance), wallCheck.position.y));
        Gizmos.DrawLine(wallCheck.position, new Vector2((wallCheck.position.x + facingDir * attackDistance), wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackRadius);
    }*/
    #endregion

    #region SetVelocity and Flip
    public void SetVelocity(float x, float y)
    {
        rd.velocity = new Vector2(x, y);
        Flip(x);
        
    }
    IEnumerator ChangeFlip(float x)
    {
        // đôi khi mình chạy mà bị quay đầu lại thì nhân vật vẫn đi theo hướng cũ một lát sau mới inputX mới ve 0
        rd.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.01f);
        SetVelocity(x, rd.velocity.y);
    }
    public void Flip(float x)
    {
        if (x > 0 && !isFlip)
        {
            facingDir = 1;
            transform.localScale = new Vector3(transform.localScale.x*(-1),transform.localScale.y,transform.localScale.z);
            onFlip?.Invoke();
            //transform.Rotate(0, 180, 0);
            isFlip = true;
            //StartCoroutine(ChangeFlip(x));

        }
        else if (x < 0 && isFlip)
        {
            onFlip?.Invoke();
            //ransform.Rotate(0, -180, 0);
            transform.localScale = new Vector3(transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
            facingDir = -1;
            isFlip = false;
            //StartCoroutine(ChangeFlip(x));
        }
    }
    #endregion
    public virtual void Die()
    {

    }
    public virtual void DecreaseSpeed(float decreasePercent,float second)
    { 
    
    }
    public void CreateThunder()
    {
        Debug.Log("test");
        GameObject thunder = Instantiate(thunderPrefabs);
        thunder.transform.parent = transform;
        thunder.transform.localPosition = new Vector3(0,1.5f); 
        
        
    }
    
}
