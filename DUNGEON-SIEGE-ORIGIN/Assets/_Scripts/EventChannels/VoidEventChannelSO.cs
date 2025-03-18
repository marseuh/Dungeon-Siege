using NaughtyAttributes;
using System;
using UnityEngine;

[Serializable]
public class FailMessagePrinter
{
    [ResizableTextArea]
    [SerializeField] private string _warningLog;

    public void PrintFailMessage()
    {
        Debug.LogWarning(_warningLog);
    }
}

[CreateAssetMenu(menuName = "ScriptableObjects/Events/VoidEventChannel", fileName = "VoidEventChannel")]
public class VoidEventChannelSO : ScriptableObject
{
    public event Action OnEventTrigger;

    [Label("Enable missing listener warning")]
    [SerializeField] private bool _isEventListenerMissingNotified;
    [ShowIf("_isEventListenerMissingNotified")]
    [SerializeField] FailMessagePrinter _failMessagePrinter;

    [Button]
    public void RequestRaiseEvent()
    {
        if (OnEventTrigger != null)
        {
            OnEventTrigger.Invoke();
        }
        else if (_isEventListenerMissingNotified)
        {
            _failMessagePrinter.PrintFailMessage();
        }
    }
}
