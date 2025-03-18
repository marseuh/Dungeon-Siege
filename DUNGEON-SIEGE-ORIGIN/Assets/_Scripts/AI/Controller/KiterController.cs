using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class KiterController : AIBaseController
{
    [Expandable]
    [SerializeField] private AIRangeSpecificDataSO _kiterData;

    private NavMeshAgent _agent;

    private new void Start()
    {
        base.Start();
        _agent = GetComponent<NavMeshAgent>();
        _behaviorTree = new KiterStrategy(transform, _agent, Target, _characterDataManager.Data, _kiterData);
    }
}
