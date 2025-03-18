using UnityEngine;

[CreateAssetMenu(fileName = "PlayVisualEffect", menuName = "ScriptableObjects/Weapons/Abilities/PlayVisualEffect")]
public class PlayVisualEffect : BaseAbilitySO
{
    public override void Use(ref AbilityBlackboard abilityData)
    {
        if (abilityData.VisualEffect == null) return;
        abilityData.VisualEffect.Play();
    }
}
