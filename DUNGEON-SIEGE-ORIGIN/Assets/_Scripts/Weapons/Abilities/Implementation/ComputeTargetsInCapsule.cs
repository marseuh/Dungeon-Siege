using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "ComputeTargetsInCapsule", menuName = "ScriptableObjects/Weapons/Abilities/ComputeTargetsInCapsule")]
public class ComputeTargetsInCapsule : BaseAbilitySO
{
    [Layer]
    [Label("Layer for hitable objects")]
    [SerializeField] private string _hitableLayerName = "Hitable";

    public override void Use(ref AbilityBlackboard abilityData)
    {
        if (abilityData.Targets.Count == 0) return;

        Transform caster = abilityData.Caster;
        float width = caster.localScale.x;

        Vector3 abilityDirection = Vector3.Normalize(abilityData.Targets[0].position - caster.position);
        Vector3 capsuleStart = caster.position + (width / 2) * abilityDirection;
        Vector3 capsuleEnd = caster.position + (abilityData.Range + width / 2) * abilityDirection;

        int layerMask = 1 << LayerMask.NameToLayer(_hitableLayerName);
        Collider[] hitColliders = Physics.OverlapCapsule(capsuleStart, capsuleEnd, width, layerMask);

        abilityData.Targets.Clear();
        abilityData.Targets.Capacity = hitColliders.Length;
        foreach (Collider collider in hitColliders)
        {
            abilityData.Targets.Add(collider.transform);
        }
    }
}
