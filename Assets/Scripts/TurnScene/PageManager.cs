using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PageManager : MonoBehaviour
{
    public Transform[] images;
    public float time = 1f;
    private float timer;

    //当下数组下标，记录要移进入画面的图片
    private int currentIndex;
    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;
        timer = time;
        foreach (Transform t in images)
        {
            t.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Tween tween;
        if (currentIndex == 6)
        {
            Debug.Log("***" + Turntimer);
            TurnUpdate();
        }
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            if (currentIndex == 2)
            {
                images[0].gameObject.SetActive(false);
                images[1].gameObject.SetActive(false);
            }
            images[currentIndex].gameObject.SetActive(true);
            if(currentIndex == 5 || currentIndex == 3)
            {
                tween= images[currentIndex].DOMove(new Vector3(0f, 0f, 24f), 0.2f).OnComplete(() =>
                {
                    shakeCamare();
                    if(currentIndex == 6)
                    {
                        Handheld.Vibrate();
                        Debug.Log("到最后了");
                        Invoke("ShowLoading", 0.5f);
                    }
                        
                });

            }
            else
            {
                tween= images[currentIndex].DOMove(new Vector3(0f, 0f, 24f), 0.5f).OnComplete(() =>
                {
                    shakeLowCamare();
                });
            }

            currentIndex++;
            if(currentIndex == images.Length)
            {
                time = 100000f;
            }
            timer = time;
            if (currentIndex == 2)
            {
                timer = 2.5f;
            }
            if (tween.IsComplete() /*&& tween != null*/)
            {
                Debug.Log("完成");
                shakeCamare();
            }
        }
    }

    private void shakeCamare()
    {
        Camera.main.transform.DOShakePosition(0.5f, 0.5f, 10, 90, false);
    }

    private void shakeLowCamare()
    {
        Camera.main.transform.DOShakePosition(0.1f, 0.1f, 2, 90, false);
    }







    private void ShowLoading()
    {
        Instantiate(Resources.Load("Prefabs/Loading"));
        StartCoroutine(loadScene(3));
    }
    AsyncOperation operation;

    IEnumerator loadScene(int i)
    {
        operation = SceneManager.LoadSceneAsync(i);
        //加载完场景不要自动跳转
        //operation.allowSceneActivation 默认为true,意味自动跳转
        operation.allowSceneActivation = false;
        yield return operation;
    }

    float Turntimer = 0;

    // Update is called once per frame
    void TurnUpdate()
    {
        //输出加载进度 0-0.9 最大为0.9
        Turntimer += Time.deltaTime;
        //如果到达5秒，再跳转
        if (Turntimer > 2f)
        {
            operation.allowSceneActivation = true;
        }
    }
}
