using UnityEngine;

[RequireComponent(typeof(CharacterDataManager))]
public class AIBaseController : MonoBehaviour
{
    public Transform Target
    {
        get { return _target; }
        set { _target = value; }
    }

    public IBehaviorTree _behaviorTree;
    protected CharacterDataManager _characterDataManager;
    
    private Transform _target;

    protected void Start()
    {
        _characterDataManager = GetComponent<CharacterDataManager>();
    }

    private void Update()
    {
        if (_target != null)
        {
            _behaviorTree.Execute(transform);
        }
    }
}
