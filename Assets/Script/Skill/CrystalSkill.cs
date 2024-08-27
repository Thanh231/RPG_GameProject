using UnityEngine;

public class CrystalSkill : Skill
{
    public GameObject crystalPrefabs;
    private Skill_Ui skillUi;

    public bool canMove;
    private GameObject currentCrystal;
    public float lifeTime;
    public float speedGrow;
    public float speedMove;
    public int damage;
    public bool canUseCrystal;
    public override void Start()
    {
        base.Start();
        skillUi = GetComponent<Skill_Ui>();
    }
    public override void UseSkill()
    {
        if (currentCrystal == null)
        {
            currentCrystal = Instantiate(crystalPrefabs, player.transform.position, Quaternion.identity);
            CrystalController crystalController = currentCrystal.GetComponent<CrystalController>();
            canMove = false;
            crystalController.SetUpCrystal(lifeTime, speedGrow, speedMove, canMove,damage);
            skillUi.TranformPlayer();
        }
        else
        {
            canMove = true;     
            ExchangeTransform();
            skillUi.Fire();
        }

    }

    private void ExchangeTransform()
    {
        if (!canMove) return;
        Vector2 tempPos = player.transform.position;

        player.transform.position = currentCrystal.transform.position;

        currentCrystal.transform.position = tempPos;

        player.skill.clone.CreateClone(currentCrystal.transform, Vector2.zero);

        
        Destroy(currentCrystal);
        
    }
}
