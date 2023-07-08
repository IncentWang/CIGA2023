using UnityEngine;

public class Edge
{
    public int Vertex0;
    public int Vertex1;

    public static bool operator ==(Edge edge1, Edge edge2)
    {
        bool forward = edge1.Vertex0 == edge2.Vertex0 && edge1.Vertex1 == edge2.Vertex1;
        bool backward = edge1.Vertex0 == edge2.Vertex1 && edge1.Vertex1 == edge2.Vertex0;
        return forward || backward;
    }

    public static bool operator !=(Edge edge1, Edge edge2)
    {
        return !(edge1 == edge2);
    }
}