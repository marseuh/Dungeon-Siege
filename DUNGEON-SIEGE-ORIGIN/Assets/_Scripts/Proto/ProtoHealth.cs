using NaughtyAttributes;
using UnityEngine;

public class ProtoHealth : MonoBehaviour, ICharacterHealth
{
    [SerializeField] private int maxHealth = 3;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    [Button]
    public void Die()
    {
        Destroy(gameObject);
    }
}