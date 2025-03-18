using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "ShootProjectile", menuName = "ScriptableObjects/Weapons/Abilities/ShootProjectile")]
public class ShootProjectile : BaseAbilitySO
{
    [Expandable]
    [SerializeField] private BaseAbilitySO _abilityOnHit;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _projectileSpeed;

    public override void Use(ref AbilityBlackboard abilityData)
    {
        if (abilityData.Targets.Count == 0) return;

        // [TODO] create a pool for this projectile
        GameObject projectileGO = Instantiate(_projectilePrefab, abilityData.Caster.position, _projectilePrefab.transform.rotation);
        LogicSenderProjectile projectile = projectileGO.GetComponent<LogicSenderProjectile>();
        if (projectile != null)
        {
            Vector3 direction = abilityData.Targets[0].position - abilityData.Caster.transform.position;
            projectile.Shoot(_abilityOnHit, abilityData, direction.normalized, _projectileSpeed);
        }
    }
}