using UnityEngine;

public class CircleCasterVisualizer : MonoBehaviour
{
    [Range(0f, 360f)]
    [SerializeField] private float normalDegLatitude;
    [Range(0f, 180f)]
    [SerializeField] private float normalDegLongitude;
    [SerializeField] private float normalDisplayMagnitude = 2f;
    [SerializeField] private float circleRadius = 1f;
    [SerializeField] private int circlePointsCount = 10;

    private void OnDrawGizmosSelected()
    {
        Vector3 origin = transform.position;
        Vector3 normal = GeometryHelper.ConvertSphericalToCartesianCoords_AnglesInDegrees(normalDegLatitude, normalDegLongitude);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(origin, normal * normalDisplayMagnitude);

        if (GeometryHelper.GetPlaneOrthonormalBasisFromNormal(out Vector3 planeVector1, out Vector3 planeVector2, normal))
        {
            GeometryHelper.InitCircleApproximationLoop(out Vector3 rayOrigin, out Vector3 nextPoint, out float angle, origin, planeVector1, planeVector2, circleRadius, circlePointsCount);
            for (int i = 0; i < circlePointsCount; ++i)
            {
                Vector3 rayDirection = nextPoint - rayOrigin;

                Gizmos.color = Color.red;
                Gizmos.DrawRay(rayOrigin, rayDirection);

                GeometryHelper.UpdateCircleApproximationLoop(ref rayOrigin, ref nextPoint, ref angle, origin, planeVector1, planeVector2, circleRadius, circlePointsCount);
            }
        }
    }
}
