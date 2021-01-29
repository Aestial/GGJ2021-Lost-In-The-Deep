using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputAim2D : MonoBehaviour
{    
    [SerializeField] 
    float speed = 1.0f;
    
    public Vector3 direction;   
    float x, y;
    float rayLength = 5.0f;

    public void OnMoveInput(float x, float y) 
    {
        this.x = x;
        this.y = y;
        // Debug.Log($"Hook direction: Input: {x} {y}");
    }
    void Update()
    {
        Vector3 targetDirection = Vector3.right * x + Vector3.up * y;
        float singleStep = speed * Time.deltaTime;
        // Set Direction
        direction = Vector3.RotateTowards(transform.right, targetDirection, singleStep, 0.0f);
        // React with object
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // React with debug rays
        // Debug.DrawRay(transform.position, targetDirection * rayLength, Color.white);
        // Debug.DrawRay(transform.position, direction * rayLength, Color.yellow);
    }
}
