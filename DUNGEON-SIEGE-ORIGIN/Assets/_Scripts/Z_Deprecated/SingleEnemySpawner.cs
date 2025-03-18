using NaughtyAttributes;
using UnityEngine;

public class SingleEnemySpawner : MonoBehaviour
{
    [Expandable]
    [SerializeField] private SingleEnemySpawnerDataSO _spawnerData;

    [BoxGroup("Listens to")]
    [SerializeField] private IntSenderEventChannelSO _launchWaveChannel;

    private void OnEnable()
    {
        _launchWaveChannel.OnEventTrigger += Spawn;
    }

    private void OnDisable()
    {
        _launchWaveChannel.OnEventTrigger -= Spawn;
    }

    private void Spawn(int waveIndex)
    {
        if (waveIndex >= _spawnerData.DoSpawnOnWaveIndexArray.Count)
        {
            return;
        }

        if (!_spawnerData.DoSpawnOnWaveIndexArray[waveIndex])
        {
            return;
        }

        Instantiate(_spawnerData.Prefab, transform.position, transform.rotation);
    }
}
