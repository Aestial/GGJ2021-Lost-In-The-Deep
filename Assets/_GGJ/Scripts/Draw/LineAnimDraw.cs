using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAnimDraw : MonoBehaviour
{
    [SerializeField]
    LineRenderer line = default;
    [SerializeField]
    Transform startTransform = default;

    bool hasTarget = false;
    Vector3 target = Vector3.zero;

    public void SetTarget(Vector3 target)
    {
        this.target = target;
        hasTarget = true;
    }

    public void RemoveTarget()
    {
        hasTarget = false;
    }

    private void DrawLine(Vector3 start, Vector3 end)
    {
        line.SetPosition(0, start);
        line.SetPosition(1, end);
    }

    void Start()
    {

    }

    void Update()
    {
        if (hasTarget)
        {
            Vector3 startPos = startTransform.position;
            DrawLine(startPos, target);
        }
        else
        {
            Vector3 startPos = startTransform.position;
            DrawLine(startPos, startPos);
        }
        
    }
}
