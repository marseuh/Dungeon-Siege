using UnityEngine;
using UnityEngine.AI;

public class MoveToTargetStrategy : IBehaviorTree
{
    public BlackBoard _blackboard;
    private Transform _target;
    private float _attackRange;
    private float _attackCooldown;
    private float _lastAttackTime;

    private IBehaviorNode _attackNode;
    private IBehaviorNode _navMeshMouvNode;

    public MoveToTargetStrategy(Transform target, NavMeshAgent agent, CharacterDataSO characterData)
    {
        this._target = target;
        this._attackRange = characterData.BaseRange;
        this._attackCooldown = characterData.BaseAttackSpeed;

        _blackboard = new BlackBoard();
        _blackboard.SetVariable<float>("damages", characterData.BaseDamages);
        _blackboard.SetVariable<NavMeshAgent>("agent", agent);
        _blackboard.SetVariable<float>("speed", characterData.MovementSpeed);

        _attackNode = new MeleeAttackStrategy(target, _blackboard);
        _navMeshMouvNode = new NavMeshMove(target, _blackboard);
    }

    public void Execute(Transform entityTransform)
    {
        if (_target != null)
        {
            //Debug.Log(_target.position);
            float distanceToTarget = Vector3.Distance(entityTransform.position, _target.position);
            if (distanceToTarget <= _attackRange)
            {
                _navMeshMouvNode.Stop();
                if (Time.time - _lastAttackTime >= _attackCooldown)
                {
                    //Debug.Log(_agent.isStopped + " " + distanceToTarget);
                    _attackNode.Execute();
                    _lastAttackTime = Time.time;
                }
            }
            else
            {
                //_navMeshMouvNode = new NavMeshMove(_target, _agent, _speed);
            }
            _navMeshMouvNode.Execute();
        }
    }
}
