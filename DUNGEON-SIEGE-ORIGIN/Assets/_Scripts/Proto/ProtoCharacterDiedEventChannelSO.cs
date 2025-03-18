using System;
using UnityEngine;

// [DEPRECATED] Use VoidEventChannelSO
[CreateAssetMenu(menuName = "ScriptableObjects/Events/CharacterDiedEventChannel", fileName = "ProtoCharacterDiedEventChannel")]
public class ProtoCharacterDiedEventChannelSO : ScriptableObject
{
    public event Action OnCharacterDied;

    public void RaiseEvent()
    {
        OnCharacterDied?.Invoke();
    }
}
