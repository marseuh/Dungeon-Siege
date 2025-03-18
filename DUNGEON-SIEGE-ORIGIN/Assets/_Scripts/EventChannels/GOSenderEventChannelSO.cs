using NaughtyAttributes;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Events/GOSenderEventChannel", fileName = "GOSenderEventChannel")]
public class GOSenderEventChannelSO : OneParamSenderEventChannelSO<GameObject>
{
}

public class OneParamSenderEventChannelSO<T> : ScriptableObject
{
    public event Action<T> OnEventTrigger;

    [Label("Enable missing listener warning")]
    [SerializeField] private bool _isEventListenerMissingNotified;
    [ShowIf("_isEventListenerMissingNotified")]
    [SerializeField] private FailMessagePrinter _failMessagePrinter;

    public void RequestRaiseEvent(T toSend)
    {
        if (OnEventTrigger != null)
        {
            OnEventTrigger.Invoke(toSend);
        }
        else if (_isEventListenerMissingNotified)
        {
            _failMessagePrinter.PrintFailMessage();
        }
    }
}

public class OneParamSenderEventChannelSOWithDebug<T> : OneParamSenderEventChannelSO<T>
{
    [Space(20)]
    [Header("Debug")]
    [SerializeField] private T _debugValueToSend;
    [Button]
    public void RequestRaiseDebugEvent()
    {
        RequestRaiseEvent(_debugValueToSend);
    }
}