using UnityEngine;

public class SmoothPlayerMovement : MonoBehaviour, IPlayerMovement
{
    float currentSpeed = 0;
    float smoothVelocity = 0;

    [SerializeField] float smoothTime = 1;

    Vector3 move = Vector3.zero;

    public void Move(Vector2 direction, float playerSpeed)
    {
        // Magnitude = normale du vecteur = positif
        // Mathf.Epsilon = très petit
        if (direction.magnitude > Mathf.Epsilon) {
            move = Vector3.Normalize(new Vector3(direction.x, 0, direction.y));
        }

        currentSpeed = Mathf.SmoothDamp(currentSpeed, playerSpeed * direction.magnitude, ref smoothVelocity, smoothTime);

        transform.position += move * Time.deltaTime * currentSpeed;
    }
}
