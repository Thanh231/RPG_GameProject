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

    public override void Start()
    {
        base.Start();
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
