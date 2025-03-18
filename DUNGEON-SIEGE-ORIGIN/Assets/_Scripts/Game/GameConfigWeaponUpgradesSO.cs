using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfigWeaponUpgrades", menuName = "ScriptableObjects/Game/GameConfigWeaponUpgrades")]
public class GameConfigWeaponUpgradesSO : ScriptableObject
{
    public WeaponDataSO LinkedWeapon;
    public List<WeaponStatisticUgradeSO> Upgrades;
}
