using UnityEngine;
using UnityEngine.AI;

public class NavMeshMove : IBehaviorNode
{
    BlackBoard _blackBoard;
    //private Vector3 _target;
    private Transform _targetTransform;
    private NavMeshAgent _agent;
    private float _speed;

    public NavMeshMove(Transform targetTransform, BlackBoard bb)
    {
        SetBlackBoard(bb);
        this._agent = _blackBoard.GetVariable<NavMeshAgent>("agent");
        this._speed = _blackBoard.GetVariable<float>("speed");
        this._targetTransform = targetTransform;
        _agent.speed = _speed;
    }

    public void Execute()
    {
        //Debug.Log("l'ia avance");
        if (_targetTransform != null && _agent !=null)
        {
            //Debug.Log("l'ia avance");
            _agent.speed = _speed;
            _agent.isStopped = false;
            _agent.SetDestination(_targetTransform.position);      
        }
    }

    public void Stop()
    {
       
        _agent.speed = 0;   
        //_agent.isStopped = true;
    }

    public bool Evaluate()
    {
        return true;
    }

    public void SetBlackBoard(BlackBoard bb)
    {
        _blackBoard = bb;
    }
}
