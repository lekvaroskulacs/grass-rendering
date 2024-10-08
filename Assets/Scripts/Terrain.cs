using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    
    ComputeShader displacePlane;

    void Start()
    {
        var mesh = GetComponent<Mesh>();
        Vector3[] verts = mesh.vertices;
        Vector2[] uvs = mesh.uv;

        ComputeBuffer vertices = new ComputeBuffer(verts.Length, sizeof(float) * 3);
        ComputeBuffer uvs = new ComputeBuffer(uvs.Length, sizeof(float) * 2);

        
        
        
    }

    
}
