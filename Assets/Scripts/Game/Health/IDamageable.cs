public interface IDamageable
{
    bool IsDamageable(Character attacker);
    void ApplyDamage(int damage, Character attacker);
}
