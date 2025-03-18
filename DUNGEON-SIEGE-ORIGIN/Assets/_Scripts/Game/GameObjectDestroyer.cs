using NaughtyAttributes;
using UnityEngine;

public class GameObjectDestroyer : MonoBehaviour
{
    [BoxGroup("Listen To")]
    [SerializeField] private GOSenderEventChannelSO _destroyObjectChannel;

    private void OnEnable()
    {
        _destroyObjectChannel.OnEventTrigger += DestroyGameObject;
    }

    private void OnDisable()
    {
        _destroyObjectChannel.OnEventTrigger -= DestroyGameObject;
    }

    private void DestroyGameObject(GameObject go)
    {
        Destroy(go);
    }
}
