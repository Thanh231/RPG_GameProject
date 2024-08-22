using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    public DashSkill dash {get; private set;}
    public CloneSkill clone { get; private set;}
    public SwordSkill swordSkill { get; private set;}
    public BlackHoleSkill blackHoleSkill { get; private set;}
    public CrystalSkill crystalSkill { get; private set;}
    public AttackArroundSkill attackArroundSkill { get; private set;}
    private void Awake()
    {
        if (instance != null) 
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        dash = GetComponent<DashSkill>();
        clone = GetComponent<CloneSkill>();
        swordSkill = GetComponent<SwordSkill>();
        blackHoleSkill = GetComponent<BlackHoleSkill>();
        crystalSkill = GetComponent<CrystalSkill>();
        attackArroundSkill = GetComponent<AttackArroundSkill>();
    }
}
