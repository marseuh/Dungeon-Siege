using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComputeTargetsOnCircle", menuName = "ScriptableObjects/Weapons/Abilities/ComputeTargetsOnCircle")]
public class ComputeTargetsOnCircle : BaseAbilitySO
{
    [Layer]
    [Label("Layer for hitable objects")]
    [SerializeField] private string _hitableLayerName = "Hitable";
    [SerializeField] private int _circleAccuracy = 10;

    public override void Use(ref AbilityBlackboard abilityData)
    {
        int layer = 1 << LayerMask.NameToLayer(_hitableLayerName);
        if (CircleCaster.CircleCast(out List<Collider> hitColliders, abilityData.Caster.position, Vector3.up, abilityData.Range, _circleAccuracy, layer))
        {
            abilityData.Targets.Clear();
            abilityData.Targets.Capacity = hitColliders.Count;
            foreach (Collider collider in hitColliders)
            {
                abilityData.Targets.Add(collider.transform);
            }
        }
    }
}
