public interface IBehaviorNode
{
    bool Evaluate();
    
   
    void Execute();
    void Stop();
    public void SetBlackBoard(BlackBoard board);
}
