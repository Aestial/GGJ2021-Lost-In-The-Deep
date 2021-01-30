using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputMovement : MonoBehaviour
{
    public enum MovementType
    {
        AddForce,
        MoveTransform,        
    }
    
    [SerializeField]
    MovementType moveType = default;
    [Header("Add Force Parameters")]
    public float thrust = 1.0f;
    [Header("Move Transform Parameters")]
    public float speed = 1.0f;

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
        switch(moveType)
        {
            case MovementType.MoveTransform:
                MoveTransform();
                break;
            case MovementType.AddForce:
            default:
                AddForce();
                break;   
        }
    }

    void AddForce()
    {
        rb.AddForce(MoveDirection * thrust);
    }

    void MoveTransform()
    {
        transform.position += MoveDirection * speed * Time.deltaTime;
    }
}
