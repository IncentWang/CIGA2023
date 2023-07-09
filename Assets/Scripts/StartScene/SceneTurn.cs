using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//有报错用不了
public class SceneTurn : MonoBehaviour
{


    AsyncOperation operation;
 
    public static SceneTurn instance = new SceneTurn();

    private SceneTurn()
    {

    }
    private int i;
    public void TurnScene(int i)
    {
        this.i = i;
        Debug.Log(i);
        StartCoroutine(loadScene());
    }

    //协程方法用来异步加载场景
    IEnumerator loadScene()
    {
        operation = SceneManager.LoadSceneAsync(i);
        //加载完场景不要自动跳转
        //operation.allowSceneActivation 默认为true,意味自动跳转
        operation.allowSceneActivation = false;
        yield return operation;
    }

    float timer = 0;

    // Update is called once per frame
    void Update()
    {
        //输出加载进度 0-0.9 最大为0.9
        Debug.Log(operation.progress);
        timer += Time.deltaTime;
        //如果到达5秒，再跳转
        if (timer > 2f)
        {
            operation.allowSceneActivation = true;
        }
    }

}
