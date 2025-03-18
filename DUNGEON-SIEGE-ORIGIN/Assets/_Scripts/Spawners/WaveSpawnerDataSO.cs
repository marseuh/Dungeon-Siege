using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Spawn/SpawnerData", fileName = "SpawnerData")]
public class WaveSpawnerDataSO : ScriptableObject
{
    public List<WaveSpawnerOptions> WaveOptions = new();
}

[Serializable]
public struct WaveSpawnerOptions
{
    public bool DoSpawn;
    [EnableIf("DoSpawn")]
    [AllowNesting]
    public GameObject Prefab;
}