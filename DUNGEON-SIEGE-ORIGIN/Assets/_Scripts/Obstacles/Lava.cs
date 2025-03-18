using UnityEngine;

public class Lava: MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float timeBetweenTwoTick = 1f;

    private void OnTriggerEnter(Collider other)
    {
        LavaInteraction lavaInteractionComponent = other.gameObject.GetComponent<LavaInteraction>();
        if (lavaInteractionComponent != null)
        {
            lavaInteractionComponent.StartDotCoroutine(damage, timeBetweenTwoTick);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        LavaInteraction lavaInteractionComponent = other.gameObject.GetComponent<LavaInteraction>();
        if (lavaInteractionComponent != null)
        {
            lavaInteractionComponent.StopDotCoroutine();
        }
    }
}
