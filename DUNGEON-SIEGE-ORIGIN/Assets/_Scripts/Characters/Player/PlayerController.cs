using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterDataManager))]
public class PlayerController : MonoBehaviour
{
    [BoxGroup("Listens to")]
    [SerializeField] private VoidEventChannelSO _playerDeathChannel;
    [BoxGroup("Listens to")]
    [SerializeField] private VoidEventChannelSO _exitLevelChannel;

    private CharacterDataManager _characterDataManager;
    private PlayerAction _playerInput;
    private IPlayerMovement _playerMovement;
    private IWeaponUser _weaponUser;
    private float _playerSpeed;

    private void Awake()
    {
        _characterDataManager = GetComponent<CharacterDataManager>();
        _playerInput = new PlayerAction();
        _playerMovement = GetComponent<IPlayerMovement>();
        _weaponUser = GetComponent<IWeaponUser>();
        _playerSpeed = _characterDataManager.Data.MovementSpeed;
    }

    private void OnEnable()
    {
        EnableInputs();

        _playerDeathChannel.OnEventTrigger += DisableInputs;
        _exitLevelChannel.OnEventTrigger += DisableInputs;
    }

    private void OnDisable()
    {
        DisableInputs();

        _playerDeathChannel.OnEventTrigger -= DisableInputs;
        _exitLevelChannel.OnEventTrigger -= DisableInputs;
    }

    private void Start()
    {
        if (_weaponUser != null)
        {
            _weaponUser.StartWeaponUse();
        }
    }

    private void FixedUpdate()
    {
        Vector3 inputDirection = _playerInput.Player.Move.ReadValue<Vector2>();
        _playerMovement.Move(inputDirection.normalized, _playerSpeed);
    }

    private void OnMoveStarted(InputAction.CallbackContext ctx)
    {
        if (_weaponUser != null)
        {
            _weaponUser.StopWeaponUse();
        }
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        if (!isActiveAndEnabled)
        {
            return;
        }

        if (_weaponUser != null)
        {
            _weaponUser.StartWeaponUse();
        }
    }

    private void EnableInputs()
    {
        _playerInput.Enable();
        _playerInput.Player.Move.started += OnMoveStarted;
        _playerInput.Player.Move.canceled += OnMoveCanceled;
    }

    private void DisableInputs()
    {
        _playerInput.Player.Move.started -= OnMoveStarted;
        _playerInput.Player.Move.canceled -= OnMoveCanceled;
        _playerInput.Disable();
    }

    public float GetPlayerSpeed()
    {
        return _playerSpeed;
    }

    public void SetPlayerSpeed(float _speed)
    {
        _playerSpeed = _speed;
    }

    public void ResetSpeed()
    {
        _playerSpeed = _characterDataManager.Data.MovementSpeed;
    }

}
