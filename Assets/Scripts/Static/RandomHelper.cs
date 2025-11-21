using UnityEngine;

public static class RandomHelper
{
    /// <summary>
    /// Returns a random float between min (inclusive) and max (inclusive).
    /// </summary>
    public static float Float(float min, float max)
    {
        return Random.Range(min, max);
    }

    /// <summary>
    /// Returns a random int between min (inclusive) and max (exclusive).
    /// </summary>
    public static int Int(int min, int max)
    {
        return Random.Range(min, max);
    }

    /// <summary>
    /// Returns a random sign: either +1 or -1.
    /// </summary>
    public static int Sign()
    {
        return Random.value < 0.5f ? -1 : 1;
    }

    /// <summary>
    /// Returns true with probability p (0 to 1).
    /// </summary>
    public static bool Chance(float p)
    {
        return Random.value < p;
    }

    /// <summary>
    /// Returns a random point inside a rectangle.
    /// </summary>
    public static Vector2 InsideRect(float minX, float maxX, float minY, float maxY)
    {
        return new Vector2(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY)
        );
    }

    /// <summary>
    /// Returns a random element of an array.
    /// </summary>
    public static T Element<T>(T[] array)
    {
        if (array == null || array.Length == 0)
        {
            Debug.LogError("RandomHelper.Element called with empty array");
            return default;
        }

        int index = Random.Range(0, array.Length);
        return array[index];
    }

    /// <summary>
    /// Returns a random element of a list.
    /// </summary>
    public static T Element<T>(System.Collections.Generic.List<T> list)
    {
        if (list == null || list.Count == 0)
        {
            Debug.LogError("RandomHelper.Element called with empty list");
            return default;
        }

        int index = Random.Range(0, list.Count);
        return list[index];
    }
}
