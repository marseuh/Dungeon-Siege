using NaughtyAttributes;
using UnityEngine;

public class LookAtNearestTargetPreview : MonoBehaviour
{
    [Tag]
    [SerializeField] private string targetsContainerTag;

    private GameObject[] _targetsContainers;

    private void Awake()
    {
        _targetsContainers = GameObject.FindGameObjectsWithTag(targetsContainerTag);
    }

    // [TODO] Find something better for directional attack preview
    private void Update()
    {
        float minDistance = Mathf.Infinity;
        Transform nearestTarget = null;
        foreach (GameObject targetContainer in _targetsContainers) 
        {
            Transform currentContainer = targetContainer.transform;
            int childrenCount = currentContainer.childCount;
            for (int i = 0; i < childrenCount; i++)
            {
                Transform currentChild = currentContainer.GetChild(i);
                float currentDistance = Vector3.Distance(currentChild.position, transform.position);
                if (currentDistance < minDistance)
                {
                    nearestTarget = currentChild;
                    minDistance = currentDistance;
                }
            }
        }
        
        if (nearestTarget != null)
        {
            transform.LookAt(nearestTarget.position);
        }
    }
}
