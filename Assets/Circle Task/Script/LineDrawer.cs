using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineDrawer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private List<Vector3> points = new List<Vector3>();
    private bool isDrawing = false;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        lineRenderer.widthMultiplier = 0.1f;

       
        Material mat = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.material = mat;

        
        lineRenderer.sortingLayerName = "Default";
        lineRenderer.sortingOrder = 10;

        lineRenderer.useWorldSpace = true;
        lineRenderer.alignment = LineAlignment.TransformZ;
    }

    void Update()
    {
        HandleMouseInput();
        HandleTouchInput();
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
            StartDrawing();
        else if (Input.GetMouseButton(0))
            AddPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        else if (Input.GetMouseButtonUp(0))
            EndDrawing();
    }

    void HandleTouchInput()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);
        Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
        touchPos.z = 0f;

        switch (touch.phase)
        {
            case TouchPhase.Began:
                StartDrawing();
                break;
            case TouchPhase.Moved:
            case TouchPhase.Stationary:
                AddPoint(touchPos);
                break;
            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                EndDrawing();
                break;
        }
    }

    void StartDrawing()
    {
        isDrawing = true;
        points.Clear();
        lineRenderer.positionCount = 0;
    }

    void AddPoint(Vector3 point)
    {
        point.z = 0f;
        if (points.Count == 0 || Vector3.Distance(points[points.Count - 1], point) > 0.05f)
        {
            points.Add(point);
            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPositions(points.ToArray());
        }
    }

    void EndDrawing()
    {
        isDrawing = false;
        CircleEliminator.Instance.CheckIntersectingCircles(points);
        ClearLine();
    }

    void ClearLine()
    {
        lineRenderer.positionCount = 0;
        points.Clear();
    }
}
