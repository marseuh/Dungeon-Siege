using UnityEngine;

public class AbilityUtils : MonoBehaviour
{
    public static bool GetNearestTargetPosition(out Vector3 result, Vector3 fromPosition, float range, int layerMask)
    {
        result = Vector3.zero;

        Collider[] detectedTargets = Physics.OverlapSphere(fromPosition, range, layerMask);
        float minDistance = Mathf.Infinity;
        int minIndex = -1;
        for (int i = 0; i < detectedTargets.Length; ++i)
        {
            float distanceToCurrentEnemy = Vector3.Distance(fromPosition, detectedTargets[i].transform.position);
            if (distanceToCurrentEnemy < minDistance)
            {
                minDistance = distanceToCurrentEnemy;
                minIndex = i;
            }
        }

        if (minIndex > -1)
        {
            result = detectedTargets[minIndex].transform.position;
            return true;
        }
        return false;
    }

    public static void TryHitCharacter(GameObject target, int damages)
    {
        ICharacterHealth healthComponent = target.GetComponent<ICharacterHealth>();
        healthComponent?.TakeDamage(damages);
    }


    public static void LaunchAbilityVisualizer(GameObject visualizer, Vector3 position, Quaternion rotation, float lifetime, float scale = 1.0f)
    {
        // [TEMP] Do something better for feedback
        // visualizer should be responsive to halfAngle and range variables, I don't know how yet
        GameObject visualizerGO = Instantiate(visualizer, position, rotation);
        visualizerGO.transform.localScale *= scale;
        Destroy(visualizerGO, lifetime);
    }
}
