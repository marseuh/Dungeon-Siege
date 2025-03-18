using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/Weapons/WeaponData")]
public class WeaponDataSO : ScriptableObject
{
    public string Name;
    public Sprite UISprite;
    public Sprite TradeIcon;

    [Space(20)]
    public int Damages;
    public float AttackSpeed;
    public float Range;

    [Expandable]
    public BaseAbilitySO Ability;
    public GameObject AbilityVisualEffectPrefab;
}