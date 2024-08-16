using UnityEngine;

public class Enemy : Entity
{
    public float idleTime;
    public Vector2 stunDir;
    public bool canbeStun;
    [SerializeField] private GameObject attackImage;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        
    }
    protected override void Update()
    {
        base.Update();
    }
    public void OpenCounterAttackWindow()
    {
        canbeStun = true;
        attackImage.SetActive(true);
    }
    public void CloseCounterAttackWindow()
    {
        canbeStun = false;
        attackImage.SetActive(false);
    }
    public virtual bool CanBeStun()
    {
        if (canbeStun)
        {
            CloseCounterAttackWindow();
            return true;
        }
        return false;
    }

}
