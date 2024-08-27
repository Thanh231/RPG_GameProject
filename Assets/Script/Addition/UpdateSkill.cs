using UnityEngine;

public class UpdateSkill : MonoBehaviour
{
    private GameObject Boss;
    public GameObject Ui_Hp;
    public void Start()
    {
        Boss = GameManager.ins.boss.gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            SkillManager.instance.attackArroundSkill.canUseAttackArround = true;
            Boss.SetActive(true);
            Destroy(gameObject);
            Ui_Hp.SetActive(true);
        }
    }
}
