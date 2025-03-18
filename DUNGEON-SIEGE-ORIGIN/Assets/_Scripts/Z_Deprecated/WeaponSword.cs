using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObjects/Sword")]
public class CapsuleSword : BaseAbilitySO
{
    [Layer]
    [Label("Layer for hitable objects")]
    [SerializeField] private string _hitableLayerName = "Hitable";

    [SerializeField] private int _damages = 1;
    [SerializeField] private float _range = 1.0f;
    [SerializeField] private float _width = 0.5f;
    [SerializeField] private float _enemiesDetectionRange = 10.0f;
    [Range(0f, 180f)]
    [SerializeField] private float _visualizerLifetime = 0.5f;
    [SerializeField] private GameObject _visualizerPrefab;

    public List<Vector3> GetLastHitPositions()
    {
        return new List<Vector3>();
    }

    public override void Use(ref AbilityBlackboard abilityData)
    {
        Transform casterTransform = abilityData.Caster;
        int bitShiftedLayerMask = 1 << LayerMask.NameToLayer(_hitableLayerName);
        if (!AbilityUtils.GetNearestTargetPosition(out Vector3 nearestEnemyPosition, casterTransform.position, _enemiesDetectionRange, bitShiftedLayerMask))
        {
            return;
        }

        // The sword auto attack is directed towards the nearest target
        Vector3 abilityDirection = Vector3.Normalize(nearestEnemyPosition - casterTransform.position);
        AbilityUtils.LaunchAbilityVisualizer(_visualizerPrefab, casterTransform.position, Quaternion.LookRotation(abilityDirection, casterTransform.up), _visualizerLifetime);

        // First we check targets which positions are inside the circle sector that represent sword hitbox, and store others for later

        Vector3 capsuleStart = casterTransform.position + (_width / 2) * abilityDirection;
        Vector3 capsuleEnd = casterTransform.position + (_range + _width / 2) * abilityDirection;
        Collider[] hitColliders = Physics.OverlapCapsule(capsuleStart, capsuleEnd, _width, bitShiftedLayerMask);
        foreach (Collider collider in hitColliders)
        {
            AbilityUtils.TryHitCharacter(collider.gameObject, _damages);
        }
    }
}

public class CircleSectorHitboxAbility : BaseAbilitySO
{
    [Layer]
    [Label("Layer for hitable objects")]
    [SerializeField] private string _hitableLayerName = "Hitable";

    [SerializeField] private int _damages = 1;
    [SerializeField] private float _range = 1.0f;
    [SerializeField] private float _enemiesDetectionRange = 10.0f;
    [Range(0f, 180f)]
    [SerializeField] private float _circleSectorHitboxHalfAngleInDeg = 30.0f;
    [SerializeField] private float _visualizerLifetime = 0.5f;
    [SerializeField] private GameObject _visualizerPrefab;

    public List<Vector3> GetLastHitPositions()
    {
        return new List<Vector3>();
    }

    public override void Use(ref AbilityBlackboard abilityData)
    {
        Transform casterTransform = abilityData.Caster;
        int bitShiftedLayerMask = 1 << LayerMask.NameToLayer(_hitableLayerName);
        if (!AbilityUtils.GetNearestTargetPosition(out Vector3 nearestEnemyPosition, casterTransform.position, _enemiesDetectionRange, bitShiftedLayerMask))
        {
            return;
        }

        // The sword auto attack is directed towards the nearest target
        Vector3 abilityDirection = Vector3.Normalize(nearestEnemyPosition - casterTransform.position);
        AbilityUtils.LaunchAbilityVisualizer(_visualizerPrefab, casterTransform.position, Quaternion.LookRotation(abilityDirection, casterTransform.up), _visualizerLifetime);

        Dictionary<Collider, bool> targetsNearBoundariesCandidates = new Dictionary<Collider, bool>();

        // First we check targets which positions are inside the circle sector that represent sword hitbox, and store others for later
        Collider[] hitColliders = Physics.OverlapSphere(casterTransform.position, _range, bitShiftedLayerMask);
        foreach (Collider collider in hitColliders)
        {
            Vector3 hitTargetDirection = collider.transform.position - casterTransform.position;
            float currentTargetAngle = Vector3.Angle(Vector3.ProjectOnPlane(hitTargetDirection, casterTransform.up), abilityDirection);
            if (currentTargetAngle < _circleSectorHitboxHalfAngleInDeg)
            {
                AbilityUtils.TryHitCharacter(collider.gameObject, _damages);
            }
            else
            {
                targetsNearBoundariesCandidates.Add(collider, false);
            }
        }

        // Then we check within outside targets which one still overlaps with the sword hitbox
        FindTargetsHitByBoundary(targetsNearBoundariesCandidates, casterTransform, abilityDirection, _circleSectorHitboxHalfAngleInDeg, bitShiftedLayerMask);
        FindTargetsHitByBoundary(targetsNearBoundariesCandidates, casterTransform, abilityDirection, -_circleSectorHitboxHalfAngleInDeg, bitShiftedLayerMask);

        foreach (Collider candidate in targetsNearBoundariesCandidates.Keys)
        {
            if (targetsNearBoundariesCandidates[candidate])
            {
                AbilityUtils.TryHitCharacter(candidate.gameObject, _damages);
            }
        }
    }

    private void FindTargetsHitByBoundary(Dictionary<Collider, bool> candidates, Transform casterTransform, Vector3 attackDirection, float boundaryAngle, int layerMask)
    {
        Quaternion boundaryRotation = Quaternion.AngleAxis(boundaryAngle, casterTransform.up);
        Vector3 boundDirection = boundaryRotation * attackDirection;
        RaycastHit[] boundaryHits = Physics.RaycastAll(casterTransform.position, boundDirection, _range, layerMask);
        foreach (RaycastHit hit in boundaryHits)
        {
            if (candidates.ContainsKey(hit.collider))
            {
                candidates[hit.collider] = true;
            }
        }
    }
}
