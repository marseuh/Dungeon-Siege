using UnityEngine;
using UnityEngine.AI;

public class MoveBackNode : IBehaviorNode
{
    protected BlackBoard _blackBoard;
    Transform _target;
    Transform _entityTransform;
    float _range;
    Vector3 _direction;
    float _distanceToTarget;
    NodeNavMeshCoord _navMeshMove;


    public void SetBlackBoard(BlackBoard bb)
    {
        _blackBoard = bb;
    }
    public MoveBackNode(Transform entityTransform,BlackBoard bb)
    {
        SetBlackBoard(bb);
        this._entityTransform = entityTransform;
        _target = _blackBoard.GetVariable<Transform>("target");
        _range = _blackBoard.GetVariable<float>("range");
        _navMeshMove = new NodeNavMeshCoord(bb);
    }
    public void Execute()
    {
        _direction = _target.position - _entityTransform.position;
        _direction.y = 0;
        Vector3 destination = _entityTransform.position - _direction.normalized;
        _navMeshMove.Target = destination;
        //Debug.Log(_direction.magnitude+" " +_target.position+" "+_entityTransform.position);

        
        _navMeshMove.Execute();
        
    }
    public void Stop()
    {
        
    }
    public bool Evaluate()
    {
        _direction = _target.position - _entityTransform.position;
        _direction.y = 0;
        _distanceToTarget = _direction.magnitude;
        //Debug.Log(_distanceToTarget+" range : "+_range);
        if (_distanceToTarget <_range)
        {
            //Debug.Log("MoveBack true");
            return true;
        }
        //Debug.Log("MoveBack False");
        return false;
    }
}
