using NaughtyAttributes;
using UnityEngine;

public class WaveSpawner : BaseSpawner
{
    [Expandable]
    [InfoBox("WARNING : modifying SpawnerDataSO here also modifies every spawner that uses this scriptable object, other scene spawners and prefabs included", EInfoBoxType.Warning)]
    [SerializeField] private WaveSpawnerDataSO _spawnerDataSO;

    [BoxGroup("Listens to")]
    [SerializeField] private IntSenderEventChannelSO _launchWaveChannel;

    private void OnEnable()
    {
        _launchWaveChannel.OnEventTrigger += OnLaunchWaveEvent;
    }

    private void OnDisable()
    {
        _launchWaveChannel.OnEventTrigger -= OnLaunchWaveEvent;
    }

    private void OnLaunchWaveEvent(int waveIndex)
    {
        if (_spawnerDataSO.WaveOptions.Count <= waveIndex)
            return;

        if (_spawnerDataSO.WaveOptions[waveIndex].DoSpawn)
        {
            Spawn(_spawnerDataSO.WaveOptions[waveIndex].Prefab);
        }
    }

    public WaveSpawnerDataSO GetData()
    {
        return _spawnerDataSO;
    }
}
