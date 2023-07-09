using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WallMove : MonoBehaviour
{
    public GameObject[] PrefabBuilding;
    public Transform createPoint;
    public Transform parentLeft;
    public Transform parentRight;

    private   List<WallMoveBase> listWall ;

    //墙壁生成时间间隔计数器
    public float time;
    public float timerLeft;
    public float timerRight;

    //道路脚本列表
    public RoadMoveBase roadMoveBase;

    public  float RunSpeed;

    //显示时间 路程
    public Text timeText;
    public Text sumText;

    //声音
    public AudioClip audioClip03;
    private AudioSource[] audioSources; 
    private void Awake()
    {
        listWall = new List<WallMoveBase>();
        
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
            GameObject leftWall = Instantiate(PrefabBuilding[Random.Range(0,7)], createPoint.position+new Vector3(-6,0,0),Quaternion.Euler(0,40,0));
            leftWall.transform.SetParent(parentLeft);
            WallMoveBase wallMoveBaseLeft = leftWall.AddComponent<WallMoveBase>();
            listWall.Add(wallMoveBaseLeft);


        }
        if(timerRight < 0)
        {
            timerRight = Random.Range(0.2f, 1);
            GameObject rightWall = Instantiate(PrefabBuilding[Random.Range(0, 7)], createPoint.position + new Vector3(6, 0, 0), Quaternion.Euler(0, -40, 0));
            rightWall.transform.SetParent(parentRight);
            WallMoveBase wallMoveBaseRight = rightWall.AddComponent<WallMoveBase>();
            listWall.Add(wallMoveBaseRight);
        }
        //更新道路与建筑速度
        UpdateSpeed();
        //更新UI
        UpdateUI();
        //结束判断
        EndCheck();
        //场景切换延迟
        TurnUpdate();
    }


    private bool isEnd;
    
    //结束判断
    private void EndCheck()
    {
        if (isEnd) return;
        if (allTime <= 0)
        {
            Handheld.Vibrate();
            GameObject obj= Instantiate(Resources.Load("Prefabs/Fail")) as GameObject;
            Button btn = obj.GetComponentInChildren<Button>();
            btn.onClick.AddListener(ShowLoading);
            isEnd = true;
            audioSources = FindObjectsOfType<AudioSource>();
            foreach (AudioSource item in audioSources)
            {
                item.Stop();
            }
            audioSources[0].PlayOneShot(audioClip03);
            return;
        }
        if(roadLength >= 300f)
        {
            Handheld.Vibrate();
            isEnd = true;
            ShowLoading();
        }
    }

    public  void ShowLoading()
    {
        Instantiate(Resources.Load("Prefabs/Loading")) ;

        StartCoroutine(loadScene(1));
    }
    AsyncOperation operation;

    IEnumerator loadScene(int i)
    {
        operation = SceneManager.LoadSceneAsync(i);
        //加载完场景不要自动跳转
        //operation.allowSceneActivation 默认为true,意味自动跳转
        operation.allowSceneActivation = false;
        isLoading = true;
        yield return operation;
    }

    float Turntimer = 0f;
    bool isLoading;
    // Update is called once per frame
    void TurnUpdate()
    {
        if (!isLoading) return;
        //输出加载进度 0-0.9 最大为0.9
        Turntimer += Time.deltaTime;
        //如果到达5秒，再跳转
        if (Turntimer > 2f)
        {
            operation.allowSceneActivation = true;
        }
    }




    //总倒计时
    private float allTime = 30;
    //总路程
    private float roadLength;

    private void UpdateUI()
    {
        allTime -= Time.deltaTime;
        roadLength = roadLength + RunSpeed/1000f; 
        timeText.text = "倒计时：" + (int)allTime;
        sumText.text = "路程：" + (int)roadLength;
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
        roadMoveBase.SetSpeed(RunSpeed);


    }

    public void DestroyObj(WallMoveBase wallMoveBase)
    {
        listWall.Remove(wallMoveBase);
        wallMoveBase.DestroySelf();
        //Debug.Log("数量：" +listWall.Count);
    }
}
