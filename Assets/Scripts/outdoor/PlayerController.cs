using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private WallMove wallMove;
    private bool isGound,inRoad;
    private Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        wallMove = FindObjectOfType<WallMove>();
        body = GetComponent<Rigidbody>();
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
    private void GroundCheck()
    {
        if(transform.position.y <= 0)
        {
            wallMove.SetRunSpeed(10f);
            inRoad = true;
        }
        else if (transform.position.y > 0.5f)
        {
            wallMove.SetRunSpeed(30f);
            inRoad = false;
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    //if (collision.gameObject.CompareTag("Road"))
    //    //{
    //        Debug.Log(collision.gameObject.name + " ***");
    //        wallMove.SetRunSpeed(10f);
    //        inRoad = true;
    //    //}
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    //if (collision.gameObject.CompareTag("Road"))
    //    //{
    //        inRoad = false;
    //    //}
    //}
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
