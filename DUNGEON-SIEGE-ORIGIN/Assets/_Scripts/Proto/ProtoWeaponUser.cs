using System.Collections;
using UnityEngine;

public class ProtoWeaponUser : MonoBehaviour, IWeaponUser
{
    // Variable that should belong to a SO
    [SerializeField] private float _attackSpeed;
    [SerializeField] private int _damages;

    [SerializeField] private WeaponDataSO _weapon;
    private GameObject _weaponVisuals;
    private IAbilityVisualEffect _weaponAbilityVisualEffect;

    private Coroutine runningCoroutine = null;

    private void Start()
    {
        _weaponVisuals = Instantiate(_weapon.AbilityVisualEffectPrefab, transform);
        _weaponAbilityVisualEffect = _weaponVisuals.GetComponent<IAbilityVisualEffect>();
    }

    public void StartWeaponUse()
    {
        if (runningCoroutine == null)
        {
            runningCoroutine = StartCoroutine(HandleUseWeapon());
        }
    }

    public void StopWeaponUse()
    {
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
            runningCoroutine = null;
        }
    }

    public IEnumerator HandleUseWeapon()
    {
        if (Mathf.Abs(_attackSpeed) < Mathf.Epsilon)
        {
            yield break;
        }

        while (true)
        {
            AbilityBlackboard abilitydata = new()
            {
                Caster = transform,
                Damages = _damages,
                VisualEffect = _weaponAbilityVisualEffect
            };
            _weapon.Ability.Use(ref abilitydata);
            yield return new WaitForSeconds(1.0f / _attackSpeed);
        }
    }

    private void OnDisable()
    {
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }
    }
}
