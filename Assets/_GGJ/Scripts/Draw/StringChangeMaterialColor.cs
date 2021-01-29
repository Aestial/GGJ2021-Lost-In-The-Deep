using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringChangeMaterialColor : MonoBehaviour
{
    [System.Serializable]
    public struct StringColor {
        public string name;
        public Color color;
    }

    [SerializeField]
    Renderer renderer = default;
    [SerializeField]
    StringColor[] colors = default;
    [SerializeField]
    string start = default;
    
    void Start()
    {
        SetColor(start);
    }

    public void SetColor (string name)
    {
        for (int i = 0; i < colors.Length; i++)
        {
            if (colors[i].name == name)
            {
                Color c = colors[i].color;
                renderer.material.SetColor("_Color", c);
            }            
        }
    }
}
