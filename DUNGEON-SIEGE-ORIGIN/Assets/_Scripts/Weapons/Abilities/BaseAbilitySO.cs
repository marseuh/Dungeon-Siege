using UnityEngine;

public abstract class BaseAbilitySO : ScriptableObject, IAbility
{
    public abstract void Use(ref AbilityBlackboard abilityData);
}
