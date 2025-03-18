using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class LevelTransitionTrigger : MonoBehaviour
{
    [BoxGroup("Listens to")]
    [SerializeField] private VoidEventChannelSO _levelCompletedChannel;
    [BoxGroup("Broadcast on")]
    [SerializeField] private VoidEventChannelSO _launchLevelTransitionChannel;

    private Collider _trigger;

    private void Awake()
    {
        _trigger = GetComponent<Collider>();
        _trigger.enabled = false;
    }

    private void OnEnable()
    {
        _levelCompletedChannel.OnEventTrigger += EnableTrigger;
    }

    private void OnDisable()
    {
        _levelCompletedChannel.OnEventTrigger -= EnableTrigger;
    }

    private void EnableTrigger()
    {
        _trigger.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _launchLevelTransitionChannel.RequestRaiseEvent();
        }
    }
}
