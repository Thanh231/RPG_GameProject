using UnityEngine;
public class SkeletonDeadStats : EnemyState
{
    private Enemy_Skeleton skeleton;
    public SkeletonDeadStats(Enemy _enemy, EnemyStateMachine _stateMachine, string _animString, Enemy_Skeleton _skeleton) : base(_enemy, _stateMachine, _animString)
    {
        skeleton = _skeleton;
    }

    public override void Enter()
    {
        base.Enter();
        AudioController.Ins.PlaySound(AudioController.Ins.aiDeath);
        skeleton.anim.SetBool(skeleton.lastAnim,true);
        skeleton.anim.speed = 0;
        timeValue = 0.3f;
    }


    public override void Update()
    {
        base.Update();
        skeleton.cd.enabled = false;
        if (timeValue < 0)
        {
            skeleton.DestroyGO();
        }
    }
}
