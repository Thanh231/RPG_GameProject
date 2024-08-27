using UnityEngine;
using UnityEngine.UI;



public class Skill_Ui : MonoBehaviour
{
    public Image skillQ;
    public Image skillW;
    public Image skillE;
    public GameObject skillECoolDown;
    public Image skillR;

    public Sprite fireSprite;
    public Sprite tranformSprite;

    private float maxCoolDownQ;
    private float maxCoolDownW;
    private float maxCoolDownE;
    private float maxCoolDownR;

    private float curremtTimeQ;
    private float curremtTimeW;
    private float curremtTimeE;
    private float curremtTimeR;

    void Start()
    {
        maxCoolDownQ = PlayerManager.instance.player.counterCoolDowm;

        maxCoolDownW = SkillManager.instance.dash.cooldownTime;
        maxCoolDownR = SkillManager.instance.blackHoleSkill.cooldownTime;

    }

    // Update is called once per frame
    void Update()
    {
        skillQ.fillAmount = PlayerManager.instance.player.counterTimer / maxCoolDownQ;

        skillW.fillAmount = SkillManager.instance.dash.canDash ? SkillManager.instance.dash.timeValue / maxCoolDownW : 1;
        skillR.fillAmount = SkillManager.instance.blackHoleSkill.canUseBlackHole ? SkillManager.instance.blackHoleSkill.timeBlackHole / maxCoolDownR : 1;

    }
    public void Fire()
    {
        skillE.sprite = fireSprite;
    }
    public void TranformPlayer()
    {
        skillE.sprite = tranformSprite;
    }
    public void SetActiceSkillE(bool status)
    {
        skillECoolDown.SetActive(status);
    }
}
