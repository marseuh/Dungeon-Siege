using UnityEngine;
using UnityEngine.AI;

public class NodeNavMeshCoord : IBehaviorNode
{
    public Vector3 Target = new Vector3(0,0,0);
   
    private BlackBoard _blackBoard;
    private NavMeshAgent _agent;
    private float _speed;

    public NodeNavMeshCoord(BlackBoard bb)
    {
        SetBlackBoard(bb);
        
        this._agent = _blackBoard.GetVariable<NavMeshAgent>("agent");
        this._speed = _blackBoard.GetVariable<float>("speed");
        _agent.speed = _speed;
    }

    public void Execute()
    {
        if( _agent !=null)
        {         
            _agent.speed = _speed;
            _agent.isStopped = false;
            _agent.SetDestination(Target);          
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
