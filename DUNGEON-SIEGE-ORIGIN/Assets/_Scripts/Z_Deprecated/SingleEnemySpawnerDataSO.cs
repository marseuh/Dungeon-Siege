using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Spawn/SingleEnemySpawnerData", fileName = "SingleEnemySpawnerData")]
public class SingleEnemySpawnerDataSO : ScriptableObject
{
    public GameObject Prefab;
    public List<bool> DoSpawnOnWaveIndexArray = new();
}