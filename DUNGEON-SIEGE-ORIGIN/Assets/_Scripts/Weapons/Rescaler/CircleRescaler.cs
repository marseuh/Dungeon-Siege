using UnityEngine;

public class CircleRescaler : MonoBehaviour, IRescaler
{
    public void Rescale(float weaponRange)
    {
        Vector3 newScale = transform.localScale;
        newScale.x *= weaponRange;
        newScale.y *= weaponRange;
        newScale.z *= weaponRange;
        transform.localScale = newScale;
    }
}
