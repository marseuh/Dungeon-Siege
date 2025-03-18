using NaughtyAttributes;
using UnityEngine;

public class CharacterDataManager : MonoBehaviour
{
    [Expandable]
    [SerializeField] protected CharacterDataSO _characterData;
    public CharacterDataSO Data { get { return _characterData; } }
}
