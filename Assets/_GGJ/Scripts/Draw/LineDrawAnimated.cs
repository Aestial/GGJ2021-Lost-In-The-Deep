using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawAnimated : MonoBehaviour
{
    [SerializeField]
    LineRenderer line = default;
    [SerializeField]
    Transform pivot = default;

    bool hasTarget = false;
    bool hasEndedShot = false;
    // bool hasEndedReload = false;
    Vector3 target = Vector3.zero;
    Vector3 start = Vector3.zero;

    public void SetTarget(Vector3 target)
    {
        this.target = target;
        hasTarget = true;
    }

    public void OnShootingEnded()
    {
        hasEndedShot = true;
    }

    public void RemoveTarget()
    {
        hasTarget = false;
        hasEndedShot = false;
    }

    public void DrawAnim(float progress)
    {
        if (hasTarget)
            DrawSegment(progress);
    }

    public void UndrawAnim(float progress)
    {
        progress = 1 - progress;
        DrawSegment(progress);
    }

    private void DrawSegment(float progress)
    {
        Vector3 startPos = pivot.position;
        Vector3 direction = target - startPos;
        Vector3 segment = startPos + direction * progress;
        DrawLine(startPos, segment);
    }

    private void DrawLine(Vector3 start, Vector3 end)
    {
        line.SetPosition(0, start);
        line.SetPosition(1, end);
    }

    void Update()
    {
        if (hasEndedShot)
        {
            Vector3 startPos = pivot.position;
            DrawLine(startPos, target);
        }
        else if (!hasTarget)
        {
            Vector3 startPos = pivot.position;
            DrawLine(startPos, startPos);
        }
        
    }
}
