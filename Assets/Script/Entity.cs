using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float attackDistance;
    public float facingDir;
    public LayerMask playerMask;

    public Transform attackCheck;
    public float attackRadius;
    public EnetityFx fx;

    [Header("Knock Back")]
    public Vector2 knockBackDir;
    public float knockBackTime;
    public bool isKnockBack;

    #region Component
    public Animator anim { get; private set; }
    public Rigidbody2D rd { get; private set; }
    public LayerMask groundLayer;
    private bool isFlip = true;
    public float speed = 8;

    #endregion

    #region Collision
    [Header("Collision Check")]
    public Transform groundCheck;
    public float groundDistance;
    public Transform wallCheck;
    public float wallDistance;
    #endregion

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rd = GetComponent<Rigidbody2D>();
        fx = GetComponent<EnetityFx>();
    }
    protected virtual void Start()
    {
    }
    protected virtual void Update()
    {
    }
    public void Damage()
    {
        Debug.Log(gameObject.name + " was damaged");
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
    public bool IsWall() => Physics2D.Raycast(wallCheck.position, transform.right, wallDistance, groundLayer);
    public RaycastHit2D IsDetectPlayer() => Physics2D.Raycast(wallCheck.position, transform.right, attackDistance ,playerMask);

    #endregion

    #region Collision

    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x,groundCheck.position.y - groundDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2((wallCheck.position.x + facingDir*wallDistance), wallCheck.position.y));
        Gizmos.DrawLine(wallCheck.position, new Vector2((wallCheck.position.x + facingDir * attackDistance), wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackRadius);
    }
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
            transform.Rotate(0, 180, 0);
            isFlip = true;
            //StartCoroutine(ChangeFlip(x));
        }
        else if (x < 0 && isFlip)
        {
            transform.Rotate(0, -180, 0);
            facingDir = -1;
            isFlip = false;
            //StartCoroutine(ChangeFlip(x));
        }
    }
    #endregion
}
