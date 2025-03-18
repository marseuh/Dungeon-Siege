using UnityEngine;

[CreateAssetMenu(fileName = "OrientTowardTarget", menuName = "ScriptableObjects/Weapons/Abilities/OrientTowardTarget")]
public class OrientTowardTarget : BaseAbilitySO
{
    public override void Use(ref AbilityBlackboard abilityData)
    {
        if (abilityData.Targets.Count == 0)
        {
            return;
        }

        abilityData.Caster.transform.LookAt(abilityData.Targets[0]);
    }
}
