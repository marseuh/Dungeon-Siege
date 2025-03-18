using UnityEngine;

public class MeleeAttackStrategy : IBehaviorNode
{
    private BlackBoard _blackBoard;
    private float _damages;
    private Transform _target;

    public MeleeAttackStrategy(Transform target, BlackBoard bb)
    {
        SetBlackBoard(bb);
        this._damages = _blackBoard.GetVariable<float>("damages");
        this._target = target;
    }

    public void Execute()
    {
        if (_target != null)
        {
            //Debug.Log("touché");
            bool proHealth = _target.gameObject.TryGetComponent<ICharacterHealth>(out var health);
            if (proHealth)
            {
                health.TakeDamage(_damages);
            }
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
