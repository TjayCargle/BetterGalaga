using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public MeshRenderer mesh;

    public void SetColor(Color color)
    {
        if (mesh != null)
        {
            if (mesh.material != null)
            {
                mesh.material.color = color;

            }
        }
    }
}
