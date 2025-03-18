using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BossController : MonoBehaviour
{
    public Transform Target;
    protected IBehaviorTree _behaviorTree;
    BlackBoard _board;
    private NavMeshAgent _agent;
    [SerializeField]
    private float _speed = 4, _range = 3, _cooldownAttack = 2, _cooldownRip = 10, _cooldownDash = 10, _vitesseExec = 3;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _board = new BlackBoard();
        _board.SetVariable<NavMeshAgent>("agent", _agent);
        _board.SetVariable<Transform>("target", Target);
        _board.SetVariable<float>("speed",_speed);
        _board.SetVariable<float>("range", _range);
        _board.SetVariable<float>("cooldownAttack", _cooldownAttack);
        _board.SetVariable<float>("cooldownRip", _cooldownRip);
        _board.SetVariable<float>("cooldownDash", _cooldownRip);
        _board.SetVariable<float>("vitesseExec", _vitesseExec);
        _behaviorTree = new BossStrategy(transform, _board);
        
    }
    private void Update()
    {
        if (Target!=null)
        {
            _behaviorTree.Execute(transform);
        }
    }
}
