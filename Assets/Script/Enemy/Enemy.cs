using System.Collections;
using UnityEngine;

public class Enemy : Entity
{
    public float idleTime;
    public Vector2 stunDir;
    public bool canbeStun;
    [SerializeField] private GameObject attackImage;
    public bool isFreeze;
    public float defaultSpeed;

    public string lastAnim { get; private set; }
    public CapsuleCollider2D cd;

    protected override void Awake()
    {
        base.Awake();
        defaultSpeed = base.speed;
        cd = GetComponent<CapsuleCollider2D>();
    }

    protected override void Start()
    {
        base.Start();
        
    }
    protected override void Update()
    {
        base.Update();
        if (isFreeze && !isKnockBack)
        {
            SetVelocity(0, 0);
        }
    }
    public void OpenCounterAttackWindow()
    {
        canbeStun = true;
        attackImage.SetActive(true);
    }
    public void CloseCounterAttackWindow()
    {
        canbeStun = false;
        attackImage.SetActive(false);
    }
    public virtual bool CanBeStun()
    {
        if (canbeStun)
        {
            CloseCounterAttackWindow();
            return true;
        }
        return false;
    }
    public void Frozen(bool _isFrozen)
    {
        isFreeze = _isFrozen;
        if(isFreeze)
        {
            CloseCounterAttackWindow();
            anim.speed = 0;
            base.speed = 0;
        }
        else
        {
            anim.speed = 1;
            base.speed = defaultSpeed;
        }
    }
    protected virtual IEnumerator FrezeTimer(float timer)
    {
        Frozen(true);
        yield return new WaitForSeconds(timer);
        Frozen(false);
    }
    public void LastAnimString(string anim)
    {
        lastAnim = anim;
    }
    public override void DecreaseSpeed(float decreasePercent, float second)
    {
        speed = speed * (1 - decreasePercent);
        anim.speed = 1 - decreasePercent;

        StartCoroutine(RollBackSpeed(second));
    }

    IEnumerator RollBackSpeed(float second)
    {
        yield return new WaitForSeconds(second);
        speed = defaultSpeed;
        anim.speed = 1;
    }
    public void DestroyGO()
    {
        Destroy(gameObject);
    }
}
