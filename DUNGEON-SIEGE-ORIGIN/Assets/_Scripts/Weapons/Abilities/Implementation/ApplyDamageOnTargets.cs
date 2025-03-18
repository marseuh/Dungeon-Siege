using UnityEngine;

[CreateAssetMenu(fileName = "ApplyDamageOnTargets", menuName = "ScriptableObjects/Weapons/Abilities/ApplyDamageOnTargets")]
public class ApplyDamageOnTargets : BaseAbilitySO
{
    public override void Use(ref AbilityBlackboard abilityData)
    {
        foreach(Transform target in abilityData.Targets)
        {
            ICharacterHealth healthComponent = target.gameObject.GetComponent<ICharacterHealth>();
            healthComponent?.TakeDamage((int)abilityData.Damages);
        }
    }
}
