public class PlayerStats : CharacterStats
{
    private Player player;
    protected override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        player.Damage();
    }
    public override void Die()
    {
        base.Die();
        player.Die();
    }
}
