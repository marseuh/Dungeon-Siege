using UnityEngine;

[RequireComponent (typeof(CollisionHandler))]
public class BrutalPlayerMovement : MonoBehaviour, IPlayerMovement
{
    private CollisionHandler _collisionHandler;

    private void Awake()
    {
        _collisionHandler = GetComponent<CollisionHandler>();
    }

    public void Move(Vector2 direction, float playerSpeed)
    {
        Vector3 stepMovement = playerSpeed * Time.fixedDeltaTime * Vector3.Normalize(new Vector3(direction.x, 0, direction.y));
        if (_collisionHandler.isActiveAndEnabled)
        {
            _collisionHandler.AlterMovement(ref stepMovement);
        }
        transform.position += stepMovement;
    }
}
