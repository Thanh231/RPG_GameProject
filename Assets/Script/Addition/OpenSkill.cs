using UnityEngine;

public class OpenSkill : MonoBehaviour
{
    public GameObject coolDown;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            PlayerManager.instance.player.canCounterAttack = true;
            SkillManager.instance.dash.canDash = true;
            SkillManager.instance.crystalSkill.canUseCrystal = true;
            SkillManager.instance.blackHoleSkill.canUseBlackHole = true;
            coolDown.SetActive(false);
            Destroy(gameObject);
        }
        
    }
}
