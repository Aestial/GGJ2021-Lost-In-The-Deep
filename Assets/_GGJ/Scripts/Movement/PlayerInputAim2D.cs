using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputAim2D : MonoBehaviour
{    
    public enum InputType
    {
        OneAxis,
        TwoAxis,
    }

    [SerializeField] 
    float speed = 1.0f;
    [SerializeField]
    InputType inputType = default;
    [SerializeField]
    bool invert = default;
    
    [Header("Debug Inspector")]
    public float angle = 0.0f;

    float x, y;

    [Header("Debug")]
    [SerializeField]
    bool showDebugRays = false;
    [SerializeField]
    float rayLength = 10.0f;

    public void OnMoveInput(float x, float y) 
    {
        this.x = x;
        this.y = y;
        // Debug.Log($"Player Aim 2D direction input: {x} {y}");
    }
    void Update()
    {        
        float singleStep = speed * Time.deltaTime;
        // Set angle
        switch (inputType) {
            case InputType.OneAxis:            
                angle += IncrementAngle1Axis(singleStep);
                break;
            case InputType.TwoAxis:
            default:
                angle = RotateAngle2Axis(singleStep); 
                break;           
        }
        // Debug.Log($"Angle value: {angle}");
        // React with object
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);        
    }

    float IncrementAngle1Axis(float step)
    {        
        // Input direction
        float direction = x;
        // Is inverted
        direction *= invert ? -1 : 1;
        // Debug
        if (showDebugRays)
            Debug.DrawRay(transform.position, Vector3.right * direction * rayLength, Color.yellow);
        // Angle
        return direction * step;
    }

    float RotateAngle2Axis(float step)
    {
        // Input direction
        Vector3 targetDirection = Vector3.right * x + Vector3.up * y;
        // Smooth direction
        Vector3 direction = Vector3.RotateTowards(transform.right, targetDirection, step, 0.0f);
        // Debug
        if (showDebugRays) {            
            Debug.DrawRay(transform.position, targetDirection * rayLength, Color.white);
            Debug.DrawRay(transform.position, direction * rayLength, Color.yellow);
        }
        // Angle
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;        
    }
}
