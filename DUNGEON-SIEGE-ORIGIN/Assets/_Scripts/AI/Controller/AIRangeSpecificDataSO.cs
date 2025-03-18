using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AI/AIRangeSpecificDataSO", fileName = "AIRangeSpecificDataSO")]
public class AIRangeSpecificDataSO : ScriptableObject
{
    public float DeltaRange;
    public float ProjectileSpeed;
    public float ProjectileLifeTime;
}
