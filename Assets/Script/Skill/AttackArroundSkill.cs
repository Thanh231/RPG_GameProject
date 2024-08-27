using UnityEngine;
using UnityEngine.UIElements;

public class AttackArroundSkill : Skill
{
    public GameObject prefab;
    public int ammount;
    public float distance;
    private Vector2[] direction;
    public float moveSpeed;
    private float angle;
    public float lifeTIme;
    public bool canAttackArround;
    public int damage;
    public bool canUseAttackArround;
    public override void Start()
    {
        base.Start();
        direction = new Vector2[ammount];

        angle = 360f / ammount;
    }
    public override void UseSkill()
    {
        if(canAttackArround)
        {
            AddObject();
        }

    }
    public override bool CanUseSkill()
    {
        if( timeValue < 0)
        {
            SetTimeValue(cooldownTime);
            return true;
        }
        return false;
    }
    public void AddObject()
    {
        for (int i = 0; i < ammount; i++)
        {
            float radians = angle * i * Mathf.Deg2Rad;
            direction[i] = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
            direction[i] = direction[i].normalized;
            float maxdis = Mathf.Abs(direction[i].x) * distance;

            GameObject crystalTemp = Instantiate(prefab, player.transform.position, Quaternion.identity, player.attackCounterCheck);


           CrystalArroundController crystal = crystalTemp.GetComponent<CrystalArroundController>();
            crystal.SetUpPrefabs(direction[i] * distance, moveSpeed, maxdis, player.transform,moveSpeed,lifeTIme,player,damage);
        }
    }
}
