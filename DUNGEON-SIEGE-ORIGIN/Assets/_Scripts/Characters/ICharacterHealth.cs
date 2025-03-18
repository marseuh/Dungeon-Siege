public interface ICharacterHealth
{
    public void TakeDamage(float amount);
    public void Die();
    public float GetCurrentHealth();
    public float GetMaxHealth();
}