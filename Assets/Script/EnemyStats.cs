public class EnemyStats : CharacterStats
{
    private Enemy enemy;
    protected override void Start()
    {
        base.Start();
        enemy = GetComponent<Enemy>();
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        enemy.Damage();
    }
    public override void Die()
    {
        base.Die();
        enemy.Die();
    }
}
