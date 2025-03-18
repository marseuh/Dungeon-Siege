using NaughtyAttributes;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ProtoWeapon : MonoBehaviour, IAbility
{
    [Layer]
    [Label("Layer for hitable objects")]
    [SerializeField] private string hitableLayerName = "Hitable";

    [SerializeField] private int damages = 1;
    [SerializeField] private float damageZoneLifeTime = 0.4f;
    [SerializeField] private float damageZoneRadius = 1.0f;
    [SerializeField] private int damageZoneRefinement = 10;
    [SerializeField] private GameObject damageZonePrefab;
    [SerializeField] private Color damageZoneAlphaWhenInactive;

    private List<Vector3> lastHitPositions = new();

    private GameObject damageZoneInstance;
    private SpriteRenderer damageZoneRenderer;

    private void Start()
    {
        damageZoneInstance = Instantiate(damageZonePrefab, transform.position, damageZonePrefab.transform.rotation, transform);
        Vector3 newScale = damageZoneInstance.transform.localScale;
        newScale.x *= damageZoneRadius / transform.localScale.x;
        newScale.y *= damageZoneRadius / transform.localScale.y;
        newScale.z *= damageZoneRadius / transform.localScale.z;
        damageZoneInstance.transform.localScale = newScale;

        damageZoneRenderer = damageZoneInstance.GetComponent<SpriteRenderer>();
        damageZoneRenderer.color = damageZoneAlphaWhenInactive;
        //damageZoneInstance.SetActive(false);
    }

    private IEnumerator EnableBrieflyDamageZone()
    {
        damageZoneRenderer.color = Color.white;
        //damageZoneInstance.SetActive(true);
        yield return new WaitForSeconds(damageZoneLifeTime);
        damageZoneRenderer.color = damageZoneAlphaWhenInactive;
        //damageZoneInstance.SetActive(false);
        yield break;
    }

    public void Use(ref AbilityBlackboard abilityData)
    {
        StartCoroutine(EnableBrieflyDamageZone());
        Transform casterTransform = abilityData.Caster;
        int bitShiftedLayerMask = 1 << LayerMask.NameToLayer(hitableLayerName);
        if (CircleCaster.CircleCast(out List<Collider> hitColliders, casterTransform.position, Vector3.up, damageZoneRadius, damageZoneRefinement, bitShiftedLayerMask))
        {
            foreach (Collider collider in hitColliders)
            {
                ICharacterHealth healthComponent = collider.gameObject.GetComponent<ICharacterHealth>();
                healthComponent?.TakeDamage(damages);
            }
        }
    }

    public List<Vector3> GetLastHitPositions()
    {
        return lastHitPositions;
    }
}
