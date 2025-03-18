using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

// Maybe add AbilitySuccess and implement actual Sequence and Selector ? Although it might not be needed for abilities
[CreateAssetMenu(fileName = "AbilityForcedSequence", menuName = "ScriptableObjects/Weapons/Abilities/AbilityForcedSequence")]
public class AbilityForcedSequenceSO : BaseAbilitySO
{
    [Expandable]
    [SerializeField] private List<BaseAbilitySO> children;

    public override void Use(ref AbilityBlackboard abilityData)
    {
        foreach (BaseAbilitySO child in children)
        {
            child.Use(ref abilityData);
        }
    }
}