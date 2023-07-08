using UnityEngine;

namespace DefaultNamespace
{
    public class TestTouchTriangle : MonoBehaviour
    {
        public FishFeelingManager Manager;
        Camera cam;

        void Start()
        {
            cam = GetComponent<Camera>();
        }

        void Update()
        {
            RaycastHit hit;
            if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Manager.Changing = false;
                return;
            }

            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (meshCollider == null || meshCollider.sharedMesh == null)
            {
                Manager.Changing = false;
                return;
            }

            Manager.Changing = true;
            if (hit.collider.GetComponent<TouchObject>().GoodPart.Contains(hit.triangleIndex))
            {
                Debug.Log("Hit Good Part!~");
                Manager.Base = 2.0f;
                return;
            }

            if (hit.collider.GetComponent<TouchObject>().BadPart.Contains(hit.triangleIndex))
            {
                Debug.Log("Hit Bad Part! Euh~");
                Manager.Base = -2.0f;
                return;
            }
            
            // This is normal part
            Manager.Base = 1.0f;
        }
    }
}