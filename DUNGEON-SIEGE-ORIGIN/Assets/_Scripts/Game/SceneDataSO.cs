using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class SceneDataSO : ScriptableObject
{
    // [BETTER] reference EnemyDataSO instead of GameObject
    [ReadOnly]
    public List<GameObject> EnemiesType = new();

    [ReadOnly]
    public List<int> EnemiesCount = new();
}
