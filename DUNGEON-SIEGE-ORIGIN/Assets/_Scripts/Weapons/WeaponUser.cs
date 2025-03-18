using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerDataManager))]
public class WeaponUser : MonoBehaviour, IWeaponUser
{
    private PlayerDataManager _playerDataManager;
    private GameObject _weaponVisuals;
    private IAbilityVisualEffect _weaponAbilityVisualEffect;
    private Coroutine _runningCoroutine = null;

    private IAbility _ability;
    private float _damages;
    private float _attackSpeed;
    private float _range;

    public void StartWeaponUse()
    {
        if (_runningCoroutine != null) return;
        _runningCoroutine = StartCoroutine(HandleUseWeapon());
    }

    public void StopWeaponUse()
    {
        if (_runningCoroutine == null) return;
        StopCoroutine(_runningCoroutine);
        _runningCoroutine = null;
    }

    private void Start()
    {
        _playerDataManager = GetComponent<PlayerDataManager>();
        _ability = _playerDataManager.Weapon.Ability;
        _damages = _playerDataManager.Damages;
        _attackSpeed = _playerDataManager.AttackSpeed;
        _range = _playerDataManager.Range;

        WeaponDataSO weapon = _playerDataManager.Weapon;
        if (weapon.AbilityVisualEffectPrefab != null)
        {
            _weaponVisuals = Instantiate(weapon.AbilityVisualEffectPrefab, transform);
            _weaponAbilityVisualEffect = _weaponVisuals.GetComponentInChildren<IAbilityVisualEffect>();
            IRescaler weaponEffectRescaler = _weaponVisuals.GetComponentInChildren<IRescaler>();

            // [HOTFIX] Rescale according to player's scale to match visuals and logic
            float newScale = weapon.Range / transform.localScale.x;
            weaponEffectRescaler?.Rescale(newScale);
        }
    }

    private void OnDisable()
    {
        if (_runningCoroutine == null) return;
        StopCoroutine(_runningCoroutine);
    }

    private IEnumerator HandleUseWeapon()
    {
        if (Mathf.Abs(_attackSpeed) < Mathf.Epsilon) yield break;

        while (true)
        {
            AbilityBlackboard abilitydata = new()
            {
                Caster = transform,
                Targets = new(),
                VisualEffect = _weaponAbilityVisualEffect,
                Damages = _damages,
                Range = _range
            };
            _ability.Use(ref abilitydata);
            yield return new WaitForSeconds(1.0f / _attackSpeed);
        }
    }

    public float Damages
    {
        get { return _damages; }
        set { _damages = value; }
    }

    public void ResetDamages()
    {
        _damages = _playerDataManager.Damages;
    }
}
