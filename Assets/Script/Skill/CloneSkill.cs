using UnityEngine;

public class CloneSkill : Skill
{
    public GameObject prefabsClone;
    public float cloneTime;
    public bool canAttack;
    public bool cloneAtStart;
    public bool cloneAtEnd;
    public void CreateClone(Transform _trans, Vector2 offset)
    {
        GameObject clone = Instantiate(prefabsClone);
        CloneController cloneControl = clone.GetComponent<CloneController>();
        cloneControl.SetClone(_trans, cloneTime,canAttack,offset);
        
    }    

    public void CreateCloneAtStart(Transform player, Vector2 offset)
    {
        if(cloneAtStart) 
            CreateClone(player, offset);
    }
    public void CreateCloneAtEnd(Transform player, Vector2 offset)
    {
        if (cloneAtEnd)
            CreateClone(player, offset);
    }
}
