using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputMovement : MonoBehaviour
{
    public float speed = 1.0f;
    public float thrust = 1.0f;

    Rigidbody rb;
    float x, y;

    Vector3 MoveDirection 
    {
        get {
            return Vector3.right * x + Vector3.up * y;
        }
    }

    public void OnMoveInput(float x, float y) 
    {
        this.x = x;
        this.y = y;
        // Debug.Log($"Player movement: Input: {x} {y}");
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    void FixedUpdate()
    {
        // MoveTransform();
        MoveAddForce();
    }

    void MoveAddForce()
    {
        rb.AddForce(MoveDirection * thrust);
    }

    void MoveTransform()
    {
        transform.position += MoveDirection * speed * Time.deltaTime;
    }
}
