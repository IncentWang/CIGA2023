using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMoveBase : MonoBehaviour
{
    private float speed;
    private  float destroyTime;

    private WallMove wallMove;
    // Start is called before the first frame update
    void Start()
    {
        speed = 20f;
        destroyTime = 30f;
        wallMove = FindObjectOfType<WallMove>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(speed);
        transform.Translate(new Vector3(0,0,-speed*Time.deltaTime),Space.World);
        destroyTime -= Time.deltaTime;
        if (destroyTime < 0)
        {
            wallMove.DestroyObj(this);
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

}
