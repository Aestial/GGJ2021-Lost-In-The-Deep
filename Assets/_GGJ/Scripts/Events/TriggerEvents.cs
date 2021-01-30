using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ObjectEvent : UnityEvent<Collider> {}

public class TriggerEvents : MonoBehaviour
{
    [SerializeField]
    ObjectEvent onTriggerEnter = default;
    [SerializeField]
    ObjectEvent onTriggerExit = default;

    private void OnTriggerEnter(Collider other) 
    {
        onTriggerEnter.Invoke(other);
    }
    private void OnTriggerExit(Collider other) 
    {
        onTriggerExit.Invoke(other);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
