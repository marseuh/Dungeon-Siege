using UnityEngine;

public class RangeAttackStrategy : IBehaviorNode
{
    private BlackBoard _blackBoard;
    private Transform _target;
    private Transform _entityTransform;
    private float _projectileDamages;
    private float _projectileSpeed;
    private float _projectileLifeTime;

    public RangeAttackStrategy(Transform entityTransform,BlackBoard bb)
    {
        SetBlackBoard(bb);
        this._entityTransform = entityTransform;
        this._target = _blackBoard.GetVariable<Transform>("target");
        this._projectileDamages = _blackBoard.GetVariable<float>("damages");
        this._projectileSpeed = _blackBoard.GetVariable<float>("projectileSpeed");
        this._projectileLifeTime = _blackBoard.GetVariable<float>("projectileLifeTime");
    }

    public void Execute()
    {
        //Debug.Log("execute attack");
        if (_target != null)
        {
            //fire logic
            //Debug.DrawLine(_entityTransform.transform.position, _target.position, Color.red, 3f);

            // object pooling in projectile pool
            GameObject projectile = ProjectilePool.Instance.GetProjectile();
            projectile.transform.position = _entityTransform.position;
            projectile.transform.rotation = _entityTransform.rotation;

            ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
            projectileController.Launch(_target.position, _projectileDamages, _projectileSpeed, _projectileLifeTime);
        }
    }

    public void Stop()
    { 
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
