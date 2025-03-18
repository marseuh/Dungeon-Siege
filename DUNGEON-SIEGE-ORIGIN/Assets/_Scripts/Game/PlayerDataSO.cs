using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    public byte CurrentCharacterId = GameConfigSO.INVALID_ID;
    public byte CurrentWeaponId = GameConfigSO.INVALID_ID;
    public Dictionary<byte, int> CountByUpgradeId = new();

    public void IncrementUpgrade(byte upgradeID)
    {
        CountByUpgradeId[upgradeID]++;
    }
}
