using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMove : MonoBehaviour
{
    public GameObject leftWallPrefab;
    public GameObject rightWallPrefab;
    public Transform createPoint;
    public Transform parentLeft;
    public Transform parentRight;

    private   List<WallMoveBase> listWall ;

    //墙壁生成时间间隔计数器
    public float time;
    public float timerLeft;
    public float timerRight;

    //道路脚本列表
    public Queue<RoadMoveBase> queueRoadMoveBase;

    public  float RunSpeed;
    private void Awake()
    {
        listWall = new List<WallMoveBase>();
        queueRoadMoveBase = new Queue<RoadMoveBase>();
    }
    // Start is called before the first frame update
    void Start()
    {

        time = 1f;
        timerLeft = time;
        timerRight = time;
    }

    // Update is called once per frame
    void Update()
    {
        timerLeft -= Time.deltaTime;
        timerRight -= Time.deltaTime;
        if(timerLeft < 0)
        {
            timerLeft = Random.Range(0.5f,1);
            GameObject leftWall = Instantiate(leftWallPrefab, createPoint.position+new Vector3(-6,0,0),Quaternion.Euler(0,60,0));
            leftWall.transform.SetParent(parentLeft);
            WallMoveBase wallMoveBaseLeft = leftWall.AddComponent<WallMoveBase>();
            listWall.Add(wallMoveBaseLeft);


        }
        if(timerRight < 0)
        {
            timerRight = Random.Range(0.2f, 1);
            GameObject rightWall = Instantiate(rightWallPrefab, createPoint.position + new Vector3(6, 0, 0), Quaternion.Euler(0, -60, 0));
            rightWall.transform.SetParent(parentRight);
            WallMoveBase wallMoveBaseRight = rightWall.AddComponent<WallMoveBase>();
            listWall.Add(wallMoveBaseRight);
        }
        //更新道路与建筑速度
        UpdateSpeed();
    }

    public void SetRunSpeed(float sp)
    {
        RunSpeed = sp;
    }


    private void UpdateSpeed()
    {
        foreach (WallMoveBase item in listWall)
        {
            item.SetSpeed(RunSpeed);
        }
        foreach (RoadMoveBase item in queueRoadMoveBase)
        {
            item.SetSpeed(RunSpeed);
        }
    }

    public void DestroyObj(WallMoveBase wallMoveBase)
    {
        listWall.Remove(wallMoveBase);
        wallMoveBase.DestroySelf();
        //Debug.Log("数量：" +listWall.Count);
    }
}
