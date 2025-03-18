using System.Collections.Generic;
using UnityEngine;

public class CircleCaster
{
    public static bool CircleCast(out List<Collider> hitColliders, Vector3 origin, Vector3 normal, float radius, int pointsCount, int layerMask)
    {
        hitColliders = new List<Collider>();

        if (GeometryHelper.GetPlaneOrthonormalBasisFromNormal(out Vector3 planeVector1, out Vector3 planeVector2, normal))
        {
            GeometryHelper.InitCircleApproximationLoop(out Vector3 rayOrigin, out Vector3 nextPoint, out float angle, origin, planeVector1, planeVector2, radius, pointsCount);

            for (int i = 0; i < pointsCount; ++i)
            {
                Vector3 rayDirection = nextPoint - rayOrigin;

                // Raycasts will not detect colliders for which the raycast origin is inside the collider. (cf unity doc)
                RaycastHit[] hits = Physics.RaycastAll(rayOrigin, rayDirection.normalized, rayDirection.magnitude, layerMask);

                hitColliders.Capacity += hits.Length;
                for (int j = 0, resultIndex = hitColliders.Count; j < hits.Length; ++j, ++resultIndex)
                {
                    hitColliders.Add(hits[j].collider);
                }

                GeometryHelper.UpdateCircleApproximationLoop(ref rayOrigin, ref nextPoint, ref angle, origin, planeVector1, planeVector2, radius, pointsCount);
            }

            if (hitColliders.Count > 0)
            {
                return true;
            }
        }

        return false;
    }
}