using UnityEngine;

public class Skill : MonoBehaviour
{
    public float cooldownTime;
    public float timeValue {  get; private set; }
    public Player player;
    public virtual void Start()
    {
        player = PlayerManager.instance.player;
    }
    private void Update()
    {
        timeValue -= Time.deltaTime;
    }
    public virtual bool CanUseSkill()
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
    public void SetTimeValue(float _time)
    {
        timeValue = _time;
    }
}
