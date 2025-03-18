using NaughtyAttributes;
using UnityEngine;

public abstract class BaseAbilityDecoratorSO : BaseAbilitySO
{
    [Expandable]
    [SerializeField] protected BaseAbilitySO Child;
}