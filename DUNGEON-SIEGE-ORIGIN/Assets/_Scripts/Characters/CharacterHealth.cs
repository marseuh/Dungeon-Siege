using NaughtyAttributes;
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterDataManager))]
public class CharacterHealth : MonoBehaviour, ICharacterHealth
{
    public event Action OnHitEvent;
    public event Action OnInvincibilityEvent;

    public event Action OnDeathEvent;

    [SerializeField] private bool _isDeathNotified;
    [SerializeField] private bool _isHitNotified;
    [SerializeField] public bool _isInInvincibleState;
    [SerializeField] private float _invincibilityTime = 1.5f;
    [BoxGroup("Broadcast on")]
    [ShowIf("_isDeathNotified")]
    [SerializeField] private VoidEventChannelSO _deathChannel;
    [BoxGroup("Broadcast on")]
    [ShowIf("_isHitNotified")]
    [SerializeField] private VoidEventChannelSO _hitChannel;

    private CharacterDataManager _characterDataManager;
    [SerializeField] private float _currentHealth;

    public void TakeDamage(float amount)
    {
        if (!isActiveAndEnabled) return;

        if (_isInInvincibleState) return;

        if (_isHitNotified)
        {
            _hitChannel.RequestRaiseEvent();
        }

        OnHitEvent?.Invoke();

        _currentHealth -= amount;
        if (_currentHealth <= 0)
        {
            Die();
        }
        else if(tag == "Player")
        {
            _isInInvincibleState = true;
            StartCoroutine(PlayerInvincible());
            OnInvincibilityEvent?.Invoke();
        }

    }

    [Button]
    public void Die()
    {
        if (_isDeathNotified)
        {
            _deathChannel.RequestRaiseEvent();
            OnDeathEvent?.Invoke();
        }
    }

    public float GetCurrentHealth()
    {
        return _currentHealth;
    }

    public float GetMaxHealth()
    {
        return _characterDataManager.Data.MaxHealth;
    }

    private void Awake()
    {
        _characterDataManager = GetComponent<CharacterDataManager>();
        _currentHealth = _characterDataManager.Data.MaxHealth;
    }

    IEnumerator PlayerInvincible()
    {
        yield return new WaitForSeconds(_invincibilityTime);
        _isInInvincibleState = false;
    }
}