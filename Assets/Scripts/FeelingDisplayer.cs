using System;
using TMPro;
using UnityEngine;

public class FeelingDisplayer : MonoBehaviour
{
    public TextMeshProUGUI FeelingText;
    public FishFeelingManager Manager;

    private void Update()
    {
        FeelingText.text = Manager.Feeling.ToString();
    }
}