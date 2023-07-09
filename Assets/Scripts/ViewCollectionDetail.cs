using UnityEngine;
using UnityEngine.EventSystems;

public class ViewCollectionDetail : MonoBehaviour, IPointerClickHandler
{
    public GameObject Detail;
    public GameObject Mask;
    public bool Unlocked = false;
    
    public void OnPointerClick(PointerEventData eventData)
    {
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