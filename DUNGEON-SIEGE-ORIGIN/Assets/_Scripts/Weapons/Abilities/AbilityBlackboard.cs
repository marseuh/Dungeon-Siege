using System.Collections.Generic;
using UnityEngine;

public class AbilityBlackboard
{
    public Transform Caster;
    public List<Transform> Targets;
    public IAbilityVisualEffect VisualEffect;
    public float Damages;
    public float Range;
}