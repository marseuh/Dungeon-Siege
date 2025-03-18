using UnityEngine;

public class DelayedAbility : BaseAbilityDecoratorSO
{
    [SerializeField] private float _delay;

    public override void Use(ref AbilityBlackboard abilityData)
    {
        // how to delay something without coroutines stuff ???
        Child.Use(ref abilityData);
    }
}