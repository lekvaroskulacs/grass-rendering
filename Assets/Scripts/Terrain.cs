using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    
    [SerializeField] ComputeShader displacePlane;

    ComputeBuffer verticesCompute;
    ComputeBuffer uvsCompute;

    readonly int 
        verticesId = Shader.PropertyToID("Vertices"),
        uvsId = Shader.PropertyToID("UVs"),
        heightMapId = Shader.PropertyToID("_HeightMap"),
        displaceStrengthId = Shader.PropertyToID("DisplaceStrength");

    void Start()
    {
        //displacePlane = Resources.Load<ComputeShader>("DisplacePlane");

        var mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] verts = mesh.vertices;
        Vector2[] uvs = mesh.uv;

        verticesCompute = new ComputeBuffer(verts.Length, sizeof(float) * 3);
        uvsCompute = new ComputeBuffer(uvs.Length, sizeof(float) * 2);
        verticesCompute.SetData(verts);
        uvsCompute.SetData(uvs);

        displacePlane.SetBuffer(0, verticesId, verticesCompute);
        displacePlane.SetBuffer(0, uvsId, uvsCompute);
        displacePlane.SetTexture(0, heightMapId, GetComponent<MeshRenderer>().material.GetTexture("_HeightMap"));
        displacePlane.SetFloat(displaceStrengthId, GetComponent<MeshRenderer>().material.GetFloat("_DisplaceStrength"));

        displacePlane.Dispatch(0, Mathf.CeilToInt(verts.Length / 128.0f), 1, 1);

        verticesCompute.GetData(verts);
        verticesCompute.Release();
        uvsCompute.Release();

        mesh.vertices = verts;
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        MeshCollider mc = GetComponent<MeshCollider>();
        mc.sharedMesh = mesh;

    }

    private void Update() {
        var mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] verts = mesh.vertices;
        Vector2[] uvs = mesh.uv;

        verticesCompute = new ComputeBuffer(verts.Length, sizeof(float) * 3);
        uvsCompute = new ComputeBuffer(uvs.Length, sizeof(float) * 2);
        verticesCompute.SetData(verts);
        uvsCompute.SetData(uvs);

        displacePlane.SetBuffer(0, verticesId, verticesCompute);
        displacePlane.SetBuffer(0, uvsId, uvsCompute);
        displacePlane.SetTexture(0, heightMapId, GetComponent<MeshRenderer>().material.GetTexture("_HeightMap"));
        displacePlane.SetFloat(displaceStrengthId, GetComponent<MeshRenderer>().material.GetFloat("_DisplaceStrength"));

        displacePlane.Dispatch(0, Mathf.CeilToInt(verts.Length / 128.0f), 1, 1);

        verticesCompute.GetData(verts);
        verticesCompute.Release();
        uvsCompute.Release();

        mesh.vertices = verts;
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        MeshCollider mc = GetComponent<MeshCollider>();
        mc.sharedMesh = mesh;
    }
    
}
