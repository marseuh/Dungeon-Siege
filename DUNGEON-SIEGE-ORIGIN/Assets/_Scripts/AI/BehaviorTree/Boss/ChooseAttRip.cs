using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseAttRip : IBehaviorNode
{
    BlackBoard _blackBoard;
    public void Execute()
    {

    }
    public bool Evaluate()
    {
        return true;
    }
    public void Stop()
    {

    }
    public void SetBlackBoard(BlackBoard bb)
    {
        _blackBoard = bb;
    }
}
