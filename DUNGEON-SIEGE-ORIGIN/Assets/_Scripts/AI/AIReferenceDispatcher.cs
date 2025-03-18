using NaughtyAttributes;
using UnityEngine;

public class AIReferenceDispatcher : MonoBehaviour
{
    [Header("References to dispatch")]
    [SerializeField] private Transform _aiTarget;

    [BoxGroup("Listens to")]
    [SerializeField] private GOSenderEventChannelSO _enemySpawnedChannel;

    private void OnEnable()
    {
        _enemySpawnedChannel.OnEventTrigger += DispatchReferences;
    }

    private void OnDisable()
    {
        _enemySpawnedChannel.OnEventTrigger -= DispatchReferences;
    }

    private void DispatchReferences(GameObject target)
    {
        if (target.TryGetComponent(out AIBaseController aiController))
        {
            aiController.Target = _aiTarget;
        }
    }
}
