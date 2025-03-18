using UnityEngine;

[RequireComponent(typeof(PlayerDataManager))]
public class CharacterLookSetter : MonoBehaviour
{
    private PlayerDataManager _playerDataManager;

    private void Start()
    {
        _playerDataManager = GetComponent<PlayerDataManager>();
        Instantiate(_playerDataManager.Data.CharacterLook, transform);
    }
}
