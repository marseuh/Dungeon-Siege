using UnityEngine;

[CreateAssetMenu(fileName = "KnockBackTargets", menuName = "ScriptableObjects/Weapons/Abilities/KnockBackTargets")]
public class KnockBackTargets : BaseAbilitySO
{
    [SerializeField] private float intensity = 5.0f;

    public override void Use(ref AbilityBlackboard abilityData)
    {
        foreach (Transform target in abilityData.Targets)
        {
            if (target.TryGetComponent(out Knockback kb))
            {
                Vector3 dir = (target.position - abilityData.Caster.position).normalized;
                kb.DoKnockback(dir, intensity);
            }
        }
    }
}