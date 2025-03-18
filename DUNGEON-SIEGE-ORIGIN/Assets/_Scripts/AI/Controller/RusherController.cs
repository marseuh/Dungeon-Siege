using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class RusherController : AIBaseController
{
    private NavMeshAgent _agent;

    private new void Start()
    {
        base.Start();
        _agent = GetComponent<NavMeshAgent>();
        _behaviorTree = new MoveToTargetStrategy(Target, _agent, _characterDataManager.Data);
    }
}
