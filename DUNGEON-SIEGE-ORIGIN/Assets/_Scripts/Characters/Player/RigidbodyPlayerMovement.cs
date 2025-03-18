using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyPlayerMovement : MonoBehaviour, IPlayerMovement
{
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector2 direction, float playerSpeed)
    {
        rb.velocity = direction.normalized * playerSpeed;
    }
}
