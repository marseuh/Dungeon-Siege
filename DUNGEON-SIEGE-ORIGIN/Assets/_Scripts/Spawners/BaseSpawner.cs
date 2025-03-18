using NaughtyAttributes;
using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    [SerializeField] private Transform childContainer;

    [BoxGroup("Broadcast on")]
    [SerializeField] private GOSenderEventChannelSO _somebodySpawnedChannel;

    private void Awake()
    {
        if (childContainer == null)
        {
            childContainer = transform;
        }
    }

    protected void Spawn(GameObject prefab)
    {
        GameObject go = Instantiate(prefab, transform.position, transform.rotation, childContainer);
        _somebodySpawnedChannel.RequestRaiseEvent(go);
    }
}
