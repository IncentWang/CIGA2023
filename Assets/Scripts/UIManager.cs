using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        public GameObject FeelingPanel;
        public Image FishImage;
        public Sprite[] PossibleFishImage;
        public ViewCollectionDetail CollectionDetail1;
        public ViewCollectionDetail CollectionDetail2;
        public ViewCollectionDetail CollectionDetail3;
        public TextMeshProUGUI Value;
        public TextMeshProUGUI Target;
        public TextMeshProUGUI Description;
        public Slider Slider;
        public float LowerLimit;
        public float UpperLimit;

        void Awake()
        {
            if(Instance != null)
                Destroy(gameObject);
            Instance = this;
        }

        void Update()
        {
            UpdateSliderAndText();
        }

        public void LevelChange(int newLevel)
        {
            ChangeFishImage(newLevel - 1);
            if (newLevel == 1)
            {
                LowerLimit = 0.0f;
                UpperLimit = 100.0f;
                CollectionDetail1.SetUnlockedStatus(false);
                CollectionDetail2.SetUnlockedStatus(false);
            }
            else if (newLevel == 2)
            {
                LowerLimit = 101.0f;
                UpperLimit = 300.0f;
                CollectionDetail1.SetUnlockedStatus(true);
                CollectionDetail2.SetUnlockedStatus(false);
            }
            else if (newLevel == 3)
            {
                LowerLimit = 301.0f;
                UpperLimit = 500.0f;
                CollectionDetail1.SetUnlockedStatus(true);
                CollectionDetail2.SetUnlockedStatus(true);
            }

            Target.text = "/ " + UpperLimit.ToString();
        }
        
        public void GoOut()
        {
            
        }

        public void OpenAndCloseFeeling()
        {
            FeelingPanel.SetActive(!FeelingPanel.activeSelf);
            Debug.Log("Open Or Close Panel!");
        }

        public void ChangeFishImage(int index)
        {
            Debug.Log(index);
            FishImage.sprite = PossibleFishImage[index];
        }

        public void UpdateSliderAndText()
        {
            float feeling = FishFeelingManager.Instance.Feeling;
            Slider.value = (feeling - LowerLimit) / (UpperLimit - LowerLimit);
            Value.text = feeling.ToString();
        }

        public void OpenCollectionDetail(int index)
        {
            
        }
    }
}