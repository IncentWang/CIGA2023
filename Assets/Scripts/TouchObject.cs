using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchObject : MonoBehaviour
{
    public Camera cam;
    public List<int> GoodPart = new List<int>();
    public List<int> BadPart = new List<int>();

    private Mesh _mesh;
    private int[] _triangles;
    private List<HashSet<int>> _trianglesOnVertices;

    void Start()
    {
        _mesh = GetComponent<MeshFilter>().mesh;
        _triangles = _mesh.GetTriangles(0);

        // Prepare all triangles on vertices data.
        _trianglesOnVertices = new List<HashSet<int>>(_mesh.vertexCount);

        for (int i = 0; i < _mesh.vertexCount; i++)
        {
            _trianglesOnVertices.Add(new HashSet<int>());
        }

        Debug.Log(_trianglesOnVertices.Count);

        for (int i = 0; i < _triangles.Length / 3; i++)
        {
            _trianglesOnVertices[_triangles[i * 3 + 0]].Add(i);
            _trianglesOnVertices[_triangles[i * 3 + 1]].Add(i);
            _trianglesOnVertices[_triangles[i * 3 + 2]].Add(i);
        }

        RandomParts(20, true);
        RandomParts(10, false);
    }

    void RandomParts(int count, bool good)
    {
        for (int i = 0; i < count; i++)
        {
            int x = Random.Range(0, _mesh.vertexCount);
            ShowTriAroundVertex(x, good);
        }
    }

    void ShowTriAroundVertex(int vertexIndex, bool good)
    {
        foreach (int x in _trianglesOnVertices[vertexIndex])
        {
            if (good)
            {
                GoodPart.Add(x);
                Vector3 p0 = _mesh.vertices[_triangles[x * 3 + 0]];
                Vector3 p1 = _mesh.vertices[_triangles[x * 3 + 1]];
                Vector3 p2 = _mesh.vertices[_triangles[x * 3 + 2]];
                p0 = transform.TransformPoint(p0);
                p1 = transform.TransformPoint(p1);
                p2 = transform.TransformPoint(p2);
                Debug.DrawLine(p0, p1, Color.green, 1000.0f);
                Debug.DrawLine(p1, p2, Color.green, 1000.0f);
                Debug.DrawLine(p0, p2, Color.green, 1000.0f);
            }
            else
            {
                BadPart.Add(x);
                Vector3 p0 = _mesh.vertices[_triangles[x * 3 + 0]];
                Vector3 p1 = _mesh.vertices[_triangles[x * 3 + 1]];
                Vector3 p2 = _mesh.vertices[_triangles[x * 3 + 2]];
                p0 = transform.TransformPoint(p0);
                p1 = transform.TransformPoint(p1);
                p2 = transform.TransformPoint(p2);
                Debug.DrawLine(p0, p1, Color.red, 1000.0f);
                Debug.DrawLine(p1, p2, Color.red, 1000.0f);
                Debug.DrawLine(p0, p2, Color.red, 1000.0f);
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}