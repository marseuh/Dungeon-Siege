using Unity.Mathematics;
using UnityEngine;

public class Convex2DObjectsCollision
{
    public static void ComputeConstrainedMove(ref Vector3 moveDirection, Vector3 collisionNormal)
    {
        float proj = Vector3.Dot(moveDirection, collisionNormal);
        if (proj < 0)
        {
            moveDirection -= proj * collisionNormal;
        }
    }

    public static void ComputeConstrainedMove(ref Vector3 moveDirection, Vector3 collisionNormal0, Vector3 collisionNormal1)
    {
        Vector3 nConstraint0 = collisionNormal0.normalized;
        Vector3 nConstraint1 = collisionNormal1.normalized;
        Vector3 nMove = moveDirection.normalized;

        float projc = Vector3.Dot(nConstraint0, nConstraint1);
        float proj0 = Vector3.Dot(nMove, nConstraint0);
        float proj1 = Vector3.Dot(nMove, nConstraint1);

        float det = nConstraint0.x * nConstraint1.z - nConstraint0.z * nConstraint1.x;
        if (Mathf.Abs(det) < Mathf.Epsilon)
        {
            if (Mathf.Abs(projc) > 0)
            {
                nMove -= proj0 * nConstraint0;
            }
            else
            {
                Debug.LogError("Convex2DCollisionHandler : One or both collision normals are zero");
                return;
            }
        }
        else
        {
            float2x2 constraintMatInv = new();
            constraintMatInv.c0.x = nConstraint1.z;
            constraintMatInv.c0.y = -nConstraint0.z;
            constraintMatInv.c1.x = -nConstraint1.x;
            constraintMatInv.c1.y = nConstraint0.x;
            constraintMatInv /= 1.0f / det;

            Vector2 moveInConstraintsCoords = new()
            {
                x = constraintMatInv.c0.x * nMove.x + constraintMatInv.c1.x * nMove.z,
                y = constraintMatInv.c0.y * nMove.x + constraintMatInv.c1.y * nMove.z
            };

            if (moveInConstraintsCoords.x < 0 && moveInConstraintsCoords.y < 0)
            {
                nMove = Vector3.zero;
            }
            else if (proj0 > 0 && proj1 > 0) { } // This is really needed
            else if (moveInConstraintsCoords.x < 0 || (projc <= 0 && proj0 < 0))
            {
                nMove -= proj0 * nConstraint0;
            }
            else if (moveInConstraintsCoords.y < 0 || (projc <= 0 && proj1 < 0))
            {
                nMove -= proj1 * nConstraint1;
            }
        }
        moveDirection = moveDirection.magnitude * nMove;
    }
};