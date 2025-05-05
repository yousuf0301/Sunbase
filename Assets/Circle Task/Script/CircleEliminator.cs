using System.Collections.Generic;
using UnityEngine;

public class CircleEliminator : MonoBehaviour
{
    public static CircleEliminator Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void CheckIntersectingCircles(List<Vector3> linePoints)
    {
        GameObject[] circles = GameObject.FindGameObjectsWithTag("Circle");

        List<GameObject> circlesToDestroy = new List<GameObject>();

        foreach (var circle in circles)
        {
            Collider2D col = circle.GetComponent<Collider2D>();
            if (col == null) continue;

            for (int i = 1; i < linePoints.Count; i++)
            {
                Vector2 p1 = linePoints[i - 1];
                Vector2 p2 = linePoints[i];

                
                if (col.OverlapPoint(p1) || col.OverlapPoint(p2))
                {
                    circlesToDestroy.Add(circle);
                    break;
                }
            }
        }

        foreach (var circle in circlesToDestroy)
        {
            Destroy(circle);
        }

       
        StartCoroutine(CheckIfAllCirclesGoneNextFrame());
    }

    private System.Collections.IEnumerator CheckIfAllCirclesGoneNextFrame()
    {
        
        yield return new WaitForEndOfFrame();

        if (GameObject.FindGameObjectsWithTag("Circle").Length == 0)
        {
            RestartPopup.instance.ShowPopup();
        }
    }
}
