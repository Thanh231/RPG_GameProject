using UnityEngine;

public class BlackHoleSkill : Skill
{
    public GameObject blachHolePrefabs;
    [SerializeField] private float maxGrowUp;
    [SerializeField] private float speedGrowUp;
    [SerializeField] private int amountAttack;
    [SerializeField] private float cloneAttackCoolDown;
    [SerializeField] private float shrinkSpeed;
    BlackHoleController currenBlackHole;
    public float blackHoleDuration;
    public float timeBlackHole;
    public bool canUseBlackHole;
    public override void Start()
    {
        base.Start();
    }
    private void Update()
    {
        timeBlackHole -= Time.deltaTime;
    }
    public override bool CanUseSkill()
    {
        if (timeBlackHole < 0)
        {
            timeBlackHole = cooldownTime;
            return true;
        }
        Debug.Log("Skill is cooldown");
        return false;
    }
    public override void UseSkill()
    {
        GameObject blackHole = Instantiate(blachHolePrefabs,player.transform.position,Quaternion.identity);
        currenBlackHole =  blackHole.GetComponent<BlackHoleController>();

        currenBlackHole.SetUpBackHole(maxGrowUp,speedGrowUp,amountAttack,cloneAttackCoolDown,shrinkSpeed,blackHoleDuration);

    }
    public bool CheckCompleteAttack()
    {
        if (currenBlackHole == null) return false;

        if (currenBlackHole.completeAttack) 
        {
            currenBlackHole = null; 
            return true;
        }
        return false;
    }
}
