using UnityEngine;
using UnityEngine.AI;

public class KiterStrategy : IBehaviorTree
{
    
    public BlackBoard _blackboard;
    private IBehaviorNode _root;

    public KiterStrategy(Transform entityTransform, NavMeshAgent agent, Transform target, CharacterDataSO characterData, AIRangeSpecificDataSO kiterData)
    {
        _blackboard = new BlackBoard();
        _blackboard.SetVariable<NavMeshAgent>("agent", agent);
        _blackboard.SetVariable<Transform>("target", target);

        _blackboard.SetVariable<float>("speed", characterData.MovementSpeed);
        _blackboard.SetVariable<float>("damages", characterData.BaseDamages);
        _blackboard.SetVariable<float>("range", characterData.BaseRange);
        _blackboard.SetVariable<float>("attackCooldown", 1.0f / characterData.BaseAttackSpeed);

        _blackboard.SetVariable<float>("projectileLifeTime", kiterData.ProjectileLifeTime);
        _blackboard.SetVariable<float>("projectileSpeed", kiterData.ProjectileSpeed);
        _blackboard.SetVariable<float>("deltaRange", kiterData.DeltaRange);

        _root = new Selector(
            new AtRangeNode(entityTransform, _blackboard),           
            new MoveBackNode(entityTransform, _blackboard),
            new KiterMovetoPlayer(entityTransform, _blackboard)
            );
    }
    
    public void Execute(Transform entityTransform)
    {
        //root.Evaluate();      
        _root.Execute();
    }
}
