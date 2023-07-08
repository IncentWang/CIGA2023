using UnityEngine;

namespace DefaultNamespace
{
    public class TestTouchTriangle : MonoBehaviour
    {
        Camera cam;

        void Start()
        {
            cam = GetComponent<Camera>();
        }

        void Update()
        {
            RaycastHit hit;
            if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
                return;

            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (meshCollider == null || meshCollider.sharedMesh == null)
                return;

            Mesh mesh = meshCollider.sharedMesh;
            Vector3[] vertices = mesh.vertices;
            int[] triangles = mesh.triangles;
            if (hit.collider.GetComponent<TouchObject>().GoodPart.Contains(hit.triangleIndex))
            {
                Debug.Log("Hit Good Part!~");
            }

            if (hit.collider.GetComponent<TouchObject>().BadPart.Contains(hit.triangleIndex))
            {
                Debug.Log("Hit Bad Part! Euh~");
            }
        }
    }
}