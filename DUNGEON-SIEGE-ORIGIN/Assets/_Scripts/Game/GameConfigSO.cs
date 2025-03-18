using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfigSO", menuName = "ScriptableObjects/Game/GameConfigSO")]
public class GameConfigSO : ScriptableObject
{
    public static byte INVALID_ID = 255;
    
    // [DEL]
    [SerializeField] public PlayerDataSO _playerData;
    
    public List<CharacterDataSO> BaseCharacters;
    public List<WeaponDataSO> BaseWeapons;
    public List<GameConfigWeaponUpgradesSO> WeaponsUpgradesConfig;

    private Dictionary<byte, CharacterDataSO> _characterById = new();
    private Dictionary<CharacterDataSO, byte> _idByCharacter = new();

    private Dictionary<byte, WeaponDataSO> _weaponById = new();
    private Dictionary<WeaponDataSO, byte> _idByWeapon = new();

    private Dictionary<byte, WeaponStatisticUgradeSO> _upgradeById = new();
    private Dictionary<WeaponStatisticUgradeSO, byte> _idByUpgrade = new();

    private Dictionary<byte, byte[]> _upgradeIdsByWeaponId = new();

    private void Init()
    {
        byte id = 0;

        foreach(CharacterDataSO character in BaseCharacters) 
        {
            _characterById.Add(id, character);
            _idByCharacter.Add(character, id);
            ++id;
        }
        foreach (WeaponDataSO weapon in BaseWeapons)
        {
            _weaponById.Add(id, weapon);
            _idByWeapon.Add(weapon, id);
            ++id;
        }
        foreach (GameConfigWeaponUpgradesSO upgradeConfig in WeaponsUpgradesConfig)
        {
            int nbUpgrades = upgradeConfig.Upgrades.Count;
            if (upgradeConfig.LinkedWeapon != null && nbUpgrades > 0)
            {
                byte[] currentWeaponUpgrades = new byte[nbUpgrades];
                int weaponIdx = 0;

                foreach (WeaponStatisticUgradeSO statisticUpgrade in upgradeConfig.Upgrades)
                {
                    _upgradeById.Add(id, statisticUpgrade);
                    _idByUpgrade.Add(statisticUpgrade, id);
                    currentWeaponUpgrades[weaponIdx++] = id;
                    _playerData.CountByUpgradeId.Add(id, 0);
                    ++id;
                }
                _upgradeIdsByWeaponId.Add(_idByWeapon[upgradeConfig.LinkedWeapon], currentWeaponUpgrades);
            }
        }
        Debug.Log("INIT GAME CONFIG : " + id + " ids created");
    }

    private void Awake()
    {
        Refresh();
    }

    private void OnValidate()
    {
        Refresh();
    }

    [Button]
    public void Refresh()
    {
        CleanUp();
        Init();
    }

    private void CleanUp()
    {
        _characterById = new();
        _idByCharacter = new();

        _weaponById = new();
        _idByWeapon = new();

        _upgradeById = new();
        _idByUpgrade = new();

        _upgradeIdsByWeaponId = new();

        _playerData.CountByUpgradeId = new();
    }

    public byte GetId(CharacterDataSO character)
    {
        if (_idByCharacter.TryGetValue(character, out byte id))
        {
            return id;
        }
        return INVALID_ID;
    }
    
    public byte GetId(WeaponDataSO weapon)
    {
        if (_idByWeapon.TryGetValue(weapon, out byte id))
        {
            return id;
        }
        return INVALID_ID;
    }
    
    public byte GetId(WeaponStatisticUgradeSO upgrade)
    {
        if (_idByUpgrade.TryGetValue(upgrade, out byte id))
        {
            return id;
        }
        return INVALID_ID;
    }

    public bool TryGetCharacter(byte id, out CharacterDataSO character)
    {
        return _characterById.TryGetValue(id, out character);
    }

    public CharacterDataSO GetCharacter(byte id)
    {
        if (TryGetCharacter(id, out CharacterDataSO character))
        {
            return character;
        }
        return null;
    }

    public bool TryGetWeapon(byte id, out WeaponDataSO weapon)
    {
        return _weaponById.TryGetValue(id, out weapon);
    }

    public WeaponDataSO GetWeapon(byte id)
    {
        if (_weaponById.TryGetValue(id, out WeaponDataSO weapon))
        {
            return weapon;
        }
        return null;
    }

    public bool TryGetUpgrade(byte id, out WeaponStatisticUgradeSO upgrade)
    {
        return _upgradeById.TryGetValue(id, out upgrade);
    }

    public WeaponStatisticUgradeSO GetUpgrade(byte id)
    {
        if (TryGetUpgrade(id, out WeaponStatisticUgradeSO upgrade))
        {
            return upgrade;
        }
        return null;
    }

    public bool TryGetWeaponUpgrades(WeaponDataSO weapon, out WeaponStatisticUgradeSO[] upgrades)
    {
        if (_upgradeIdsByWeaponId.TryGetValue(GetId(weapon), out byte[] upgradeIds))
        {
            int nbUpgrades = upgradeIds.Length;
            upgrades = new WeaponStatisticUgradeSO[nbUpgrades];
            for (int i = 0; i < nbUpgrades; ++i)
            {
                upgrades[i] = GetUpgrade(upgradeIds[i]);
            }
            return true;
        }
        upgrades = null;
        return false;
    }
}
