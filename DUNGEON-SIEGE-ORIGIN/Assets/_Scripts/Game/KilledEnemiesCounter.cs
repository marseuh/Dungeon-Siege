using NaughtyAttributes;
using UnityEngine;

// For now we only count kills  
// [MAYBE] Count kills by enemy type : listen to a death event that sends a CharacterDataSO and increment a unique value for this character

public class KilledEnemiesCounter : MonoBehaviour
{
    [SerializeField] private SceneDataSO currentSceneData;

    [BoxGroup("Listens to")]
    [SerializeField] private VoidEventChannelSO _enemyDeathChannel;
    [BoxGroup("Broadcast on")]
    [SerializeField] private VoidEventChannelSO _mapClearedChannel;

    private int _count = 0;
    private int _objective = 0;

    private void OnEnable()
    {
        _enemyDeathChannel.OnEventTrigger += IncrementKilledEnemiesCount;
    }

    private void OnDisable()
    {
        _enemyDeathChannel.OnEventTrigger -= IncrementKilledEnemiesCount;
    }

    private void Start()
    {
        for (int i = 0; i < currentSceneData.EnemiesCount.Count; i++)
        {
            _objective += currentSceneData.EnemiesCount[i];
        }

        if (_count >= _objective)
        {
            _mapClearedChannel.RequestRaiseEvent();
        }
    }

    private void IncrementKilledEnemiesCount()
    {
        ++_count;
        if (_count >= _objective)
        {
            _mapClearedChannel.RequestRaiseEvent();
        }
    }
}