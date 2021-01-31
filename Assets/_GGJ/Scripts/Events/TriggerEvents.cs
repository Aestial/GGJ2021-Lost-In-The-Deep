using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvents : MonoBehaviour
{
    [SerializeField]
    ColliderEvent onTriggerEnter = default;
    [SerializeField]
    ColliderEvent onTriggerExit = default;

    private void OnTriggerEnter(Collider other) 
    {
        onTriggerEnter.Invoke(other);
    }
    private void OnTriggerExit(Collider other) 
    {
        onTriggerExit.Invoke(other);
    }
}
