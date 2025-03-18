using UnityEngine;

public class ArrayHelper
{
    public static int GetIndexOfMax(params float[] array)
    {
        int maxIndex = 0;
        float max = array[maxIndex];
        for (int i = 1; i < array.Length; ++i)
        {
            if (array[i] > max)
            {
                maxIndex = i;
                max = array[maxIndex];
            }
        }
        return maxIndex;
    }

    public static bool GetFirstNonZeroIndex(out int index, Vector3 vector)
    {
        return GetFirstNonZeroIndex(out index, vector.x, vector.y, vector.z);
    }

    public static bool GetFirstNonZeroIndex(out int index, params float[] array)
    {
        index = 0;
        for (int i = 0; i < array.Length; ++i)
        {
            if (Mathf.Abs(array[i]) > Mathf.Epsilon)
            {
                index = i;
                return true;
            }
        }
        return false;
    }
}