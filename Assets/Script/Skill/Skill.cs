using UnityEngine;

public class Skill : MonoBehaviour
{
    public float cooldownTime;
    private float timeValue;
    public Player player;
    public virtual void Start()
    {
        player = PlayerManager.instance.player;
    }
    private void Update()
    {
        timeValue -= Time.deltaTime;
    }
    public bool CanUseSkill()
    {
        if(timeValue < 0)
        {
            UseSkill();
            timeValue = cooldownTime;
            return true;
        }
        Debug.Log("Skill is cooldown");
        return false;
    }
    public virtual void UseSkill()
    {

    }
}
