using System;
using TMPro;
using UnityEngine;

public class FeelingDisplayer : MonoBehaviour
{
    public TextMeshProUGUI FeelingText;

    private void Update()
    {
        FeelingText.text = FishFeelingManager.Instance.Feeling.ToString();
    }
}