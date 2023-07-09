using UnityEngine;
using UnityEngine.EventSystems;

public class ViewCollectionDetail : MonoBehaviour, IPointerClickHandler
{
    public GameObject Detail;
    public GameObject Mask;
    public bool Unlocked = false;
    public AudioSource SFXSource;
    public AudioClip ButtonSFX;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        SFXSource.PlayOneShot(ButtonSFX);
        if (Unlocked)
        {
            Detail.SetActive(true);
        }
    }

    public void SetUnlockedStatus(bool status)
    {
        Mask.SetActive(!status);
        Unlocked = status;
    }
}