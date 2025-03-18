using NaughtyAttributes;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    [BoxGroup("Listens to")]
    [SerializeField] private VoidEventChannelSO _levelCompletedChannel;

    [SerializeField] private Animator _animator;

    private void OnEnable()
    {
        _levelCompletedChannel.OnEventTrigger += SetDoorOpen;
    }

    private void OnDisable()
    {
        _levelCompletedChannel.OnEventTrigger -= SetDoorOpen;
    }

    private void SetDoorOpen()
    {
        _animator.SetBool("bIsAllEnemiesDead", true);
    }
}
