using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCommonComponents : MonoBehaviour
{
    [SerializeField]
    bool m_Renderer = true;
    [SerializeField]
    bool m_Collider = true;

    Renderer renderer;
    Collider collider;
    
    public void Enable()
    {
        if (m_Renderer) renderer.enabled = true;
        if (m_Collider) collider.enabled = true;
    }

    public void Disable()
    {
        if (m_Renderer) renderer.enabled = false;
        if (m_Collider) collider.enabled = false;
    }

    void Awake()
    {
        collider = GetComponent<Collider>();
        renderer = GetComponent<Renderer>();
    }
}
