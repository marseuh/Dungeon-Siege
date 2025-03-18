using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "FindNearestTargetInRange", menuName = "ScriptableObjects/Weapons/Abilities/FindNearestTargetInRange")]
public class FindNearestTargetInRange : BaseAbilitySO
{
    [Layer]
    [Label("Layer for targets to find")]
    [SerializeField] private string _targetLayerName = "Hitable";

    public override void Use(ref AbilityBlackboard abilityData)
    {
        Vector3 origin = abilityData.Caster.position;
        int layer = 1 << LayerMask.NameToLayer(_targetLayerName);
        Collider[] detectedTargets = Physics.OverlapSphere(origin, abilityData.Range, layer);

        if (detectedTargets.Length == 0) return;

        float minDistance = Mathf.Infinity;
        Transform nearestTarget = null;
        foreach (Collider targetCollider in detectedTargets)
        {
            Transform currentTarget = targetCollider.transform;
            float distanceToCurrentEnemy = Vector3.Distance(origin, currentTarget.position);
            if (distanceToCurrentEnemy < minDistance)
            {
                nearestTarget = currentTarget;
                minDistance = distanceToCurrentEnemy;
            }
        }

        abilityData.Targets.Clear();
        abilityData.Targets.Add(nearestTarget);

        //Debug.Log("ABILITIES : FindNearestTarget | " + abilityData.Target);
    }
}
