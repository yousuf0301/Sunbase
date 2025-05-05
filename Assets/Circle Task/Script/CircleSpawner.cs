using System.Collections.Generic;
using UnityEngine;

public class CircleSpawner : MonoBehaviour
{
    public GameObject circlePrefab;
    public int minCount = 5;
    public int maxCount = 10;
    public float radius = 5f;

    private List<GameObject> circles = new List<GameObject>();

    private void Start()
    {
        SpawnRandomCircles();
    }

    public void SpawnRandomCircles()
    {
        ClearAllCircles();

        int count = RandomGenerator.GetRandomCount(minCount, maxCount);
        for (int i = 0; i < count; i++)
        {
            Vector2 pos = RandomGenerator.GetRandomPosition(radius);
            GameObject circle = Instantiate(circlePrefab, pos, Quaternion.identity);
            circles.Add(circle);
        }

    }

    public void ClearAllCircles()
    {
        foreach (var circle in circles)
        {
            if (circle != null)
                Destroy(circle);
        }

        circles.Clear();
    }
}
