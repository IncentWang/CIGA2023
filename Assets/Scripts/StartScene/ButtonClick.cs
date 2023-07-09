using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    private  Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        animator.SetTrigger("click");
        Invoke("ShowLoading", 1f);
    }

    private void ShowLoading()
    {
        Instantiate(Resources.Load("Prefabs/Loading"));
        StartCoroutine(loadScene(1));
    }
    AsyncOperation operation;

    IEnumerator loadScene(int i)
    {
        operation = SceneManager.LoadSceneAsync(i);
        //加载完场景不要自动跳转
        //operation.allowSceneActivation 默认为true,意味自动跳转
        operation.allowSceneActivation = false;
        isEnd = true;
        yield return operation;
    }

    float timer = 0;
    bool isEnd;
    // Update is called once per frame
    void Update()
    {
        if (!isEnd) return;
        //输出加载进度 0-0.9 最大为0.9
        timer += Time.deltaTime;
        //如果到达5秒，再跳转
        if (timer > 2f)
        {
            operation.allowSceneActivation = true;
        }
    }

}
