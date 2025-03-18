using UnityEngine;
using UnityEngine.AI;

public class KiterMovetoPlayer : IBehaviorNode
{
    protected BlackBoard _blackBoard;
    Transform _target;
    Transform _entityTransform;
    float _range;
    Vector3 _direction;
    float _distanceToTarget;

    IBehaviorNode _navMeshMove;
    public void SetBlackBoard(BlackBoard bb)
    {
        _blackBoard = bb;
    }
    public KiterMovetoPlayer(Transform entityTransform,BlackBoard bb)
    {
        SetBlackBoard(bb);
        this._entityTransform = entityTransform;
        _target = _blackBoard.GetVariable<Transform>("target");
        _range = _blackBoard.GetVariable<float>("range");
        _navMeshMove = new NavMeshMove(_target, bb);
    }
    public void Execute()
    {
        //Debug.Log("on rentre dans execute de movetoplayer");
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
       //Debug.Log(_distanceToTarget+" range : "+_range );
        if (_distanceToTarget > _range)
        {
            //Debug.Log("MoveToplayer true");
            return true;
        }
        //Debug.Log("MoveToplayer False");
        return false;
    }
}
