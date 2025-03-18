using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossStrategy : IBehaviorTree
{
    private Transform _transform;
    private BlackBoard _board;
    IBehaviorNode root;
    public BossStrategy(Transform entityTransform,BlackBoard board)
    {
        root = new Selector(
            new ChooseAttRip(),
            new Dash(),
            new WalkToPlayer()

            );
    }
    public void Execute(Transform entityTransform)
    {

    }
}
