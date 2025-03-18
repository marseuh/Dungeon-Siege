using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int currentLevel;

    public byte characterID;

    public byte weaponID;

    public SerializableDictionary<byte, int> weaponUpgrade; 

    /// <summary>
    /// Constructor
    /// </summary>
    public GameData()
    {
        currentLevel = 0;
        characterID = 255;
        weaponID = 255;
        weaponUpgrade = new SerializableDictionary<byte, int>();
        for (byte i = 0; i < Byte.MaxValue; i++)
        {
            weaponUpgrade[i] = 0;
        }
    }
}
