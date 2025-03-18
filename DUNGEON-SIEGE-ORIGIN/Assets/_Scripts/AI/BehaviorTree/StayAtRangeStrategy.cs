using UnityEngine;
using UnityEngine.AI;
/*
Stay at Range and auto attack 
*/
// [DEPRECATED]
public class StayAtRangeStrategy : IBehaviorTree
{
    private NavMeshAgent _agent;
    private Transform _target;
    private float _deltaRange = 1;
    private float _attackrange;
    //private float _projectileSpeed;
    //private float _projectileLifeTime;
    //private float _speed;
    private float _attackCooldown;
    private float _lastAttackTime;
    
    private IBehaviorNode _attackNode;
    private IBehaviorNode _navMeshMouvNode;

    // [HOTFIX] Just to avoid creating new GO each frame when AI wants to go back
    private GameObject _myPersonalTarget;
    // [HOTFIX] End

    public StayAtRangeStrategy(Transform target,NavMeshAgent agent, float attackrange, float speed, float attackCooldown,Transform entityTransform,float projectileLifeTime,float projectileSpeed,BlackBoard bb)
    {
        this._agent = agent;
        this._target = target;
        this._attackrange = attackrange;
        //this._projectileSpeed = projectileSpeed;
        //this._projectileLifeTime = projectileLifeTime;
        //this._speed = speed;
        this._attackCooldown = attackCooldown;
    
        _attackNode = new RangeAttackStrategy(entityTransform,bb);

        // [HOTFIX]
        _myPersonalTarget = new GameObject("MyPersonalTarget");
        _myPersonalTarget.transform.parent = _agent.transform;
        _myPersonalTarget.transform.position = Vector3.zero;
        // [HOTFIX] End
    }

    public void Execute(Transform EntityTransform)
    {
        
        if (_target != null)
        {
            Vector3 direction = _target.position - EntityTransform.position;
            direction.y = 0;
            float distanceToTarget = direction.magnitude;
           
            if (distanceToTarget < _attackrange+ _deltaRange && distanceToTarget > _attackrange - _deltaRange)
            {
                if(_navMeshMouvNode != null)
                    _navMeshMouvNode.Stop();

                if (Time.time - _lastAttackTime >= _attackCooldown)
                {
                    _attackNode.Execute();                   
                    _lastAttackTime = Time.time;
                    
                }
            }
            else if (distanceToTarget > _attackrange)
            {
                // [DEPRECATED] NavMeshMove changed !
                //_navMeshMouvNode = new NavMeshMove(_target, _agent, _speed);
            }
            else if (distanceToTarget < _attackrange)
            {
                Vector3 destination = EntityTransform.position - direction.normalized;

                // [HOTFIX]
                _myPersonalTarget.transform.position = destination;
                // [DEPRECATED] NavMeshMove changed !
                //_navMeshMouvNode = new NavMeshMove(_myPersonalTarget.transform, _agent, _speed);
                // [HOTFIX] End
            }

            if (_navMeshMouvNode != null)
                _navMeshMouvNode.Execute();
        }
    }
}
