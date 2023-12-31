﻿using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class TestTouchTriangle : MonoBehaviour
    {
        public FishFeelingManager Manager;
        public ParticleSystem Love;
        public ParticleSystem Shit;
        public AudioSource SFXSource;
        public AudioClip TouchSFX;
        public GameObject FeelingPanel;
        
        Camera cam;
        private long[] normalPattern = new[] {200l, 200l};
        private long[] badPattern = new[] {50l, 100l};
        private int currentMode = 0;  // 0 is idle, 1 is good, 2 is normal, 3 is bad
        private Coroutine _shitPlay;

        void Start()
        {
            cam = GetComponent<Camera>();
            Love.Stop();
            Shit.Stop();
            Manager = FishFeelingManager.Instance;
        }

        IEnumerator StopShit()
        {
            yield return new WaitForSeconds(3f);
            Shit.Stop();
        }

        void Update()
        {
            RaycastHit hit = new RaycastHit();
            if (FishFeelingManager.Instance.IsPlaying())
                return;
            if (FeelingPanel.activeSelf)
                return;
#if UNITY_ANDROID && !UNITY_EDITOR           
            if (Input.touchCount > 0 && !Physics.Raycast(cam.ScreenPointToRay(Input.GetTouch(0).position), out hit))
            {
                Manager.Changing = false;
                Vibration.Cancel();
                currentMode = 0;
                Manager.ChangeTouching(false);
                SFXSource.Stop();
                return;
            }
#else
            if (Input.GetMouseButton(0) && !Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Manager.Changing = false;
                Vibration.Cancel();
                currentMode = 0;
                Manager.ChangeTouching(false);
                SFXSource.Stop();
                return;
            }
#endif

            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (meshCollider == null || meshCollider.sharedMesh == null)
            {
                Manager.Changing = false;
                Vibration.Cancel();
                currentMode = 0;
                SFXSource.Stop();
                Manager.ChangeTouching(false);
                return;
            }

            Manager.Changing = true;
            if (!SFXSource.isPlaying)
            {
                SFXSource.Play();
            }
            if (hit.collider.GetComponent<TouchObject>().GoodPart.Contains(hit.triangleIndex))
            {
                Debug.Log("Hit Good Part!~");
                Manager.IsGood = true;
                if (currentMode != 1)
                {
                    Vibration.Cancel();
                    Vibration.Vibrate(10000);
                    Love.Play();
                    Shit.Stop();
                    Manager.ChangeTouching(true);
                    currentMode = 1;
                }
                return;
            }

            if (hit.collider.GetComponent<TouchObject>().BadPart.Contains(hit.triangleIndex))
            {
                Debug.Log("Hit Bad Part! Euh~");
                Manager.Bad = true;
                if (currentMode != 3)
                {
                    Vibration.Cancel();
                    Vibration.Vibrate(badPattern, repeat: 0);
                    Shit.Play();
                    _shitPlay = StartCoroutine(StopShit()); 
                    Love.Stop();
                    Manager.ChangeTouching(true);
                    currentMode = 3;
                }
                return;
            }
            
            // This is normal part
            Manager.Base = 1.0f;
            Manager.IsGood = false;
            if (currentMode != 2)
            {
                Vibration.Cancel();
                Vibration.Vibrate(normalPattern, repeat: 0);
                Manager.ChangeTouching(true);
                currentMode = 2;
                Love.Stop();
            }
        }
    }
}