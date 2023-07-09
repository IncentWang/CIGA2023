using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private WallMove wallMove;
    private bool isGound,inRoad;
    private Rigidbody body;
    private FishFeelingManager fishFeelingManager;
    private float LoveLevel;
    private float LoveAddSpeed;
    private AudioSource audioSource;
    public AudioClip audioClip01;
    public AudioClip audioClip02;



    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        wallMove = FindObjectOfType<WallMove>();
        body = GetComponent<Rigidbody>();
        fishFeelingManager = FindObjectOfType<FishFeelingManager>();
        LoveLevel = fishFeelingManager.Feeling;
        SetLoveSpeed();
        //LoveAddSpeed = 1.2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        //TouchControl();
        MouseControl();
        GroundCheck();
    }

    public void PlaySmallClip()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(audioClip01);
    }
    public void PlayBigClip()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(audioClip02);
    }


    private void SetLoveSpeed()
    {
        if(LoveLevel >0 && LoveLevel <= 100)
        {
            LoveAddSpeed = 1.2f;
        }
        else if (LoveLevel >100 && LoveLevel <= 300)
        {
            LoveAddSpeed = 1.5f;
        }
        else if (LoveLevel >300 && LoveLevel <= 500)
        {
            LoveAddSpeed = 2f;
        }
    }

    private void GroundCheck()
    {
        if(transform.position.y <= 0)
        {
            wallMove.SetRunSpeed(10f * LoveAddSpeed);
            inRoad = true;
        }
        else if (transform.position.y > 0.5f)
        {
            wallMove.SetRunSpeed(30f * LoveAddSpeed);
            inRoad = false;
        }
    }

    public void SetIsGround()
    {
        isGound = true;
    }

    public void SetIsNotGround()
    {
        
        isGound = false;
    }
    private void MouseControl()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    if (Input.mousePosition.x < Screen.width / 2)
        //    {
        //        //向左移动
        //        transform.Translate(Vector3.left * Time.deltaTime);
        //    }
        //    else
        //    {
        //        // 向右移动
        //        transform.Translate(Vector3.right * Time.deltaTime);
        //    }
        //}
        if (Input.GetMouseButtonDown(0) && isGound && inRoad)
        {
            Handheld.Vibrate();
            body.AddForce(Vector3.up *  350f);
            PlayBigClip();
            Debug.Log("我跳了");
        }
    }



    private void TouchControl()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                body.AddForce(Vector3.up * 100f);
            }
            //if (touch.position.x < Screen.width / 2)
            //{
            //    // 向左移动
            //    transform.Translate(Vector3.left * Time.deltaTime);
            //}
            //else
            //{
            //    // 向右移动
            //    transform.Translate(Vector3.right * Time.deltaTime);
            //}
        }
    }
}
