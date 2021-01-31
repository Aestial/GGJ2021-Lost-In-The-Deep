using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEvents : MonoBehaviour
{
    [SerializeField]
    CollisionEvent onCollisionEnter = default;
    [SerializeField]
    CollisionEvent onCollisionExit = default;

    private void OnCollisionEnter(Collision collision) 
    {
        onCollisionEnter.Invoke(collision);
    }
    private void OnCollisionExit(Collision collision) 
    {
        onCollisionExit.Invoke(collision);
    }
}
