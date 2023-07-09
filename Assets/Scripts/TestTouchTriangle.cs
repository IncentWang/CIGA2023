using UnityEngine;

namespace DefaultNamespace
{
    public class TestTouchTriangle : MonoBehaviour
    {
        public FishFeelingManager Manager;
        Camera cam;
        private long[] normalPattern = new[] {200l, 200l};
        private long[] badPattern = new[] {50l, 100l};
        private int currentMode = 0;  // 0 is idle, 1 is good, 2 is normal, 3 is bad

        void Start()
        {
            cam = GetComponent<Camera>();
        }

        void Update()
        {
            RaycastHit hit = new RaycastHit();
#if UNITY_ANDROID && !UNITY_EDITOR           
            if (Input.touchCount > 0 && !Physics.Raycast(cam.ScreenPointToRay(Input.GetTouch(0).position), out hit))
            {
                Manager.Changing = false;
                Vibration.Cancel();
                currentMode = 0;
                return;
            }
#else
            if (Input.GetMouseButton(0) && !Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Manager.Changing = false;
                Vibration.Cancel();
                currentMode = 0;
                return;
            }
#endif

            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (meshCollider == null || meshCollider.sharedMesh == null)
            {
                Manager.Changing = false;
                Vibration.Cancel();
                currentMode = 0;
                return;
            }

            Manager.Changing = true;
            if (hit.collider.GetComponent<TouchObject>().GoodPart.Contains(hit.triangleIndex))
            {
                Debug.Log("Hit Good Part!~");
                Manager.Base = 2.0f;
                if (currentMode != 1)
                {
                    Vibration.Cancel();
                    Vibration.Vibrate(10000);
                    currentMode = 1;
                }
                return;
            }

            if (hit.collider.GetComponent<TouchObject>().BadPart.Contains(hit.triangleIndex))
            {
                Debug.Log("Hit Bad Part! Euh~");
                Manager.Base = -2.0f;
                if (currentMode != 3)
                {
                    Vibration.Cancel();
                    Vibration.Vibrate(badPattern, repeat: 0);
                    currentMode = 3;
                }
                //Vibration.VibratePredefined(Vibration.PredefinedEffect.EFFECT_HEAVY_CLICK);
                return;
            }
            
            // This is normal part
            Manager.Base = 1.0f;
            if (currentMode != 2)
            {
                Vibration.Cancel();
                Vibration.Vibrate(normalPattern, repeat: 0);
                currentMode = 2;
            }
        }
    }
}