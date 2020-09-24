using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDeformationExample : MonoBehaviour
{

    public Material material;
    public Mesh originalMesh;
    public float waveCount = 2; //per unity units
    public float waveAmount = 0.1f;
    public float waveSpeed = 2;
    public float twistAmount = 2;

    Mesh _mesh;
    Vector3[] _originalVertices;
    Vector3[] _deformedVertices;

    float waveAngleOffset;

    // Start is called before the first frame update
    void Awake()
    {
        _originalVertices = originalMesh.vertices;
        int[] triangleindicies = originalMesh.triangles;

        _mesh = new Mesh();
        _mesh.vertices = _originalVertices;
        _mesh.triangles = triangleindicies;
        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();

        _deformedVertices = new Vector3[_originalVertices.Length];
         
    }

    // Update is called once per frame
    void Update()
    {
        waveAngleOffset += Time.deltaTime * waveSpeed;
        for (int v = 0; v < _originalVertices.Length; v++)
        {
            Vector3 vertexPosition = _originalVertices[v];
            //Manipulate!
            float angle = vertexPosition.x * Mathf.PI * 2*waveCount+waveAngleOffset;
            vertexPosition.y += Mathf.Sin(angle)*waveAmount;

            //Twist.
            vertexPosition = Quaternion.Euler(0, vertexPosition.y*twistAmount, 0) * vertexPosition;


            _deformedVertices[v] = vertexPosition;
        }

        _mesh.vertices = _deformedVertices;
        _mesh.RecalculateNormals();

        Graphics.DrawMesh(_mesh, transform.localToWorldMatrix, material, gameObject.layer);
    }
}
