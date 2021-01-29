using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputAim2D : MonoBehaviour
{    
    [SerializeField] 
    float speed = 1.0f;
    
    public Vector3 direction;
    public float angle = 0.0f;
    float x, y;

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
        // angle = RotateAngleTwoAxis(singleStep);
        angle += IncrementAngleOneAxis(singleStep);
        // Debug.Log($"Angle value: {angle}");
        // React with object
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // React with debug rays
        // float rayLength = 5.0f;
        // Debug.DrawRay(transform.position, targetDirection * rayLength, Color.white);
        // Debug.DrawRay(transform.position, direction * rayLength, Color.yellow);
    }

    float IncrementAngleOneAxis(float step)
    {        
        return step * -x;
    }

    float RotateAngleTwoAxis(float step)
    {
        Vector3 targetDirection = Vector3.right * x + Vector3.up * y;
        // Set Direction
        direction = Vector3.RotateTowards(transform.right, targetDirection, step, 0.0f);
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }
}
