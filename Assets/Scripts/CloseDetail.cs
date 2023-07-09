using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseDetail : MonoBehaviour, IPointerClickHandler
{
    public AudioSource SFXSource;
    public AudioClip ButtonSFX;
    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.SetActive(false);
        SFXSource.PlayOneShot(ButtonSFX);
    }
}
