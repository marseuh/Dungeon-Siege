using UnityEngine;

public class GeometryHelper
{
    public static bool GetPlaneOrthonormalBasisFromNormal(out Vector3 basisVector1, out Vector3 basisVector2, Vector3 normal)
    {
        basisVector1 = new();
        basisVector2 = new();

        if (ArrayHelper.GetFirstNonZeroIndex(out int nonZeroIndex, normal))
        {
            basisVector1[nonZeroIndex] = -normal[(nonZeroIndex + 1) % 3] / normal[nonZeroIndex];
            basisVector1[(nonZeroIndex + 1) % 3] = 1f;
            basisVector1[(nonZeroIndex + 2) % 3] = 0f;
            basisVector1 = Vector3.Normalize(basisVector1);

            basisVector2 = Vector3.Cross(basisVector1, normal);
            basisVector2 = Vector3.Normalize(basisVector2);
            return true;
        }
        return false;
    }

    public static void InitCircleApproximationLoop(out Vector3 currentPoint, out Vector3 nextPoint, out float angle, Vector3 origin, Vector3 basisVector1, Vector3 basisVector2, float radius, float pointsCount)
    {
        nextPoint = origin + radius * basisVector1;
        currentPoint = new();
        angle = 0;
        UpdateCircleApproximationLoop(ref currentPoint, ref nextPoint, ref angle, origin, basisVector1, basisVector2, radius, pointsCount);
    }

    public static void UpdateCircleApproximationLoop(ref Vector3 currentPoint, ref Vector3 nextPoint, ref float angle, Vector3 origin, Vector3 basisVector1, Vector3 basisVector2, float radius, float pointsCount)
    {
        currentPoint = nextPoint;
        angle += GetCircleApproximationAngleStep(pointsCount);
        nextPoint = origin + radius * (Mathf.Cos(angle) * basisVector1 + Mathf.Sin(angle) * basisVector2);
    }

    public static float GetCircleApproximationAngleStep(float pointsCount)
    {
        return 2 * Mathf.PI / pointsCount;
    }

    public static Vector3 ConvertSphericalToCartesianCoords_AnglesInDegrees(float latitude, float longitude)
    {
        return ConvertSphericalToCartesianCoords(Mathf.Deg2Rad * latitude, Mathf.Deg2Rad * longitude);
    }

    public static Vector3 ConvertSphericalToCartesianCoords(float latitude, float longitude)
    {
        return new Vector3(Mathf.Sin(longitude) * Mathf.Cos(latitude),
                           Mathf.Cos(longitude),
                           Mathf.Sin(longitude) * Mathf.Sin(latitude));
    }
}