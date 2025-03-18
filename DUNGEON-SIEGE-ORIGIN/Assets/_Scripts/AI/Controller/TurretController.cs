using NaughtyAttributes;
using UnityEngine;

public class TurretController : AIBaseController
{
    [Expandable]
    [SerializeField] private AIRangeSpecificDataSO _turretData;

    private new void Start()
    {
        base.Start();
        _behaviorTree = new TurretStrategy(transform, Target, _characterDataManager.Data, _turretData);
    }

    private void Update()
    {
        _behaviorTree.Execute(transform);
    }
}
