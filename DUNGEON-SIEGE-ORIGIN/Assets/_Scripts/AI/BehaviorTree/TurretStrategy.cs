using UnityEngine;

public class TurretStrategy : IBehaviorTree
{
    BlackBoard _blackBoard;
    private Transform _target;
    private float _attackCooldown;
    private float _lastAttackTime;
    private IBehaviorNode _attackNode;

    public TurretStrategy(Transform entityTransform, Transform target, CharacterDataSO characterData, AIRangeSpecificDataSO turretData)
    {
        _blackBoard = new BlackBoard();

        _blackBoard.SetVariable<Transform>("target", target);
        _blackBoard.SetVariable<float>("damages", characterData.BaseDamages);
        _blackBoard.SetVariable<float>("projectileSpeed", turretData.ProjectileSpeed);
        _blackBoard.SetVariable<float>("projectileLifeTime", turretData.ProjectileLifeTime);

        _target = target;
        _attackCooldown = 1.0f / characterData.BaseAttackSpeed;
        _attackNode = new RangeAttackStrategy(entityTransform, _blackBoard);
    }

    public void Execute(Transform entityTransform)
    {
        if (_target != null)
        {
            if (Time.time - _lastAttackTime >= _attackCooldown)
            {
                _attackNode.Execute();
                _lastAttackTime = Time.time;
            }     
        }
    }
}
