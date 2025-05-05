using UnityEngine;

public static class RandomGenerator
{
    public static int GetRandomCount(int min, int max)
    {
        return Random.Range(min, max + 1);
    }

    public static Vector2 GetRandomPosition(float radius)
    {
        return Random.insideUnitCircle * radius;
    }
}
