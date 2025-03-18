using System.Collections.Generic;

public class Selector : IBehaviorNode
{
    private List<IBehaviorNode> _childNodes = new List<IBehaviorNode>();

    public Selector(params IBehaviorNode[] nodes)
    {
        _childNodes.AddRange(nodes);
        //Debug.Log(_childNodes.Count);
    }

    public bool Evaluate()
    {
        return true;
    }

    public void Execute()
    {
        foreach (IBehaviorNode node in _childNodes)
        {
            if (node.Evaluate())
            {
                //Debug.Log("on rentre dans evaluate : "+node.ToString());
                node.Execute();
                
            }
        }
        
    }

    public void Stop()
    {
    }

    public void SetBlackBoard(BlackBoard bb)
    {
    }
}
