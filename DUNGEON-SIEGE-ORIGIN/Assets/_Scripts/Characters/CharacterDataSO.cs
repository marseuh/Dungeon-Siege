using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "ScriptableObjects/Character")]
public class CharacterDataSO : ScriptableObject
{
    public string Name;
    public Sprite UISprite;

    [Space(20)]
    public float MaxHealth;
    public float MovementSpeed;
    public float BaseDamages;
    public float BaseAttackSpeed;
    public float BaseRange;

    [Expandable]
    public WeaponDataSO BaseWeapon;
    public GameObject CharacterLook;
}
