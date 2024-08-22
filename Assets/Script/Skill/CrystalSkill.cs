using UnityEngine;

public class CrystalSkill : Skill
{
    public GameObject crystalPrefabs;

    public bool canMove;
    private GameObject currentCrystal;
    public float lifeTime;
    public float speedGrow;
    public float speedMove;

    
    public override void Start()
    {
        base.Start();

        
    }
    public override void UseSkill()
    {
        if (currentCrystal == null)
        {
            currentCrystal = Instantiate(crystalPrefabs, player.transform.position, Quaternion.identity);
            CrystalController crystalController = currentCrystal.GetComponent<CrystalController>();
            crystalController.SetUpCrystal(lifeTime, speedGrow,speedMove,canMove);
        }
        else
        {
            if (!canMove) return;

            Vector2 tempPos = player.transform.position;

            player.transform.position = currentCrystal.transform.position;

            currentCrystal.transform.position = tempPos;

            player.skill.clone.CreateClone(currentCrystal.transform, Vector2.zero);
            
            Destroy(currentCrystal);
        }
    }
    /*public void AddObject()
    {
        for (int i = 0; i < ammount; i++)
        {
            float radians = angle * i * Mathf.Deg2Rad;
            direction[i] = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
            direction[i] = direction[i].normalized;
            float maxdis = Mathf.Abs(direction[i].x)*distance;

            GameObject crystalTemp = Instantiate(crystalPrefabs, player.transform.position, Quaternion.identity,player.attackCounterCheck);


            CrystalController crystalController = crystalTemp.GetComponent<CrystalController>();
            float delaytemp = (i + 1) * delay;
            Debug.Log(direction[i]);
            crystalController.SetUpCrystal(lifeTime, speedGrow, speedMove, canMove);
            crystalController.SetUpCrystalUpgrade(direction[i] *    distance, moveSpeed, maxdis, player.transform.position, delaytemp,true);
        }
    }*/
}
