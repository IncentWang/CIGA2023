using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextAnimation : MonoBehaviour
{
    private Text text;
    private string textValue;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        textValue = text.text;
        text.text = "";
        text.DOText(textValue, 4);
        text.DOPlay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
