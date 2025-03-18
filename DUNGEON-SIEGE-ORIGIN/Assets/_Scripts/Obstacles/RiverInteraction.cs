using UnityEngine.AI;
using UnityEngine;

public class RiverInteraction : MonoBehaviour
{
    private PlayerController _playerControllerComponent;
    private NavMeshAgent _navMeshAgentComponent;
    private int _countSlowCaseTrigger = 0;

    // Start is called before the first frame update
    void Start()
    {
        _playerControllerComponent = GetComponent<PlayerController>();
        _navMeshAgentComponent = GetComponent<NavMeshAgent>();
    }

    public void SlowPlayerSpeed(float slowdown)
    {
        if (_countSlowCaseTrigger == 0)
        {
            _playerControllerComponent.SetPlayerSpeed(_playerControllerComponent.GetPlayerSpeed() * slowdown);
        }
            _countSlowCaseTrigger++;
    }

    public void RemoveSlowPlayerSpeed(float slowdown)
    {
        _countSlowCaseTrigger--;
        if (_countSlowCaseTrigger == 0)
        {
            _playerControllerComponent.ResetSpeed();
        }
    }

    public void SlowAiSpeed(float slowdown)
    { 
        _navMeshAgentComponent.speed = _navMeshAgentComponent.speed * slowdown;
    }

    public void RemoveSlowAiSpeed(float slowdown)
    {
        _navMeshAgentComponent.speed = _navMeshAgentComponent.speed / slowdown;
    }
}
