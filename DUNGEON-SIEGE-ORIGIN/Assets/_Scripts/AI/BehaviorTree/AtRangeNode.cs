using UnityEngine;
using UnityEngine.AI;

public class AtRangeNode : IBehaviorNode
{
    protected BlackBoard _blackBoard;
    private float _range;
    private Transform _target;
    private Transform _entityTransform;
    private float _deltaRange;
    private float _lastAttackTime;
    private float _attackCooldown;
    private IBehaviorNode _attackNode;
    private IBehaviorNode _stopMoveNode;

    public void SetBlackBoard(BlackBoard bb)
    {
        _blackBoard = bb;
    }

    public AtRangeNode(Transform entityTransform, BlackBoard bb)
    {
        SetBlackBoard(bb);
        _range = _blackBoard.GetVariable<float>("range");
        _target = _blackBoard.GetVariable<Transform>("target");
        _attackCooldown = _blackBoard.GetVariable<float>("attackCooldown");
        _deltaRange = _blackBoard.GetVariable<float>("deltaRange");

        _entityTransform = entityTransform;

        _attackNode = new RangeAttackStrategy(_entityTransform,bb);
        _stopMoveNode = new NavMeshMove(entityTransform, bb);
    }

    public bool Evaluate()
    {
  
        Vector3 direction = _target.position - _entityTransform.position;
        direction.y = 0;
        float distanceToTarget = direction.magnitude;
        if (distanceToTarget < _range + _deltaRange && distanceToTarget > _range - _deltaRange)
        {
            
            return true;
            
        }
        //Debug.Log("At range False");
        return false;
    }

    public void Execute()
    {
        _stopMoveNode.Stop();
        if (Time.time - _lastAttackTime >= _attackCooldown)
        {
            //Debug.Log("At range reussite");
            _lastAttackTime = Time.time;
            _attackNode.Execute();
        }
    }

    public void Stop()
    {
    }
}
