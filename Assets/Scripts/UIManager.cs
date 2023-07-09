using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        public AudioSource SFXSource;
        public AudioClip ButtonSFX;

        void Awake()
        {
            if(Instance != null)
                Destroy(gameObject);
            Instance = this;
        }

        void Update()
        {
            UpdateSliderAndText();

            //场景跳转延迟
            if (isTurning)
            {
                TurnUpdate();
            }
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

        //场景跳转部分
        public void GoOut()
        {
            ShowLoading();
            SFXSource.PlayOneShot(ButtonSFX);
            isTurning = true;
        }

        
        private void ShowLoading()
        {
            Instantiate(Resources.Load("Prefabs/Loading"));
            StartCoroutine(loadScene(2));
        }
        AsyncOperation operation;
        bool isTurning;
        IEnumerator loadScene(int i)
        {
            operation = SceneManager.LoadSceneAsync(i);
            //加载完场景不要自动跳转
            //operation.allowSceneActivation 默认为true,意味自动跳转
            operation.allowSceneActivation = false;
            yield return operation;
        }

        float Turntimer = 0;
        private void TurnUpdate()
        {
            //输出加载进度 0-0.9 最大为0.9
            Turntimer += Time.deltaTime;
            //如果到达5秒，再跳转
            if (Turntimer > 2f)
            {
                operation.allowSceneActivation = true;
            }
        }
        //场景跳转部分结束


        public void OpenAndCloseFeeling()
        {
            FeelingPanel.SetActive(!FeelingPanel.activeSelf);
            SFXSource.PlayOneShot(ButtonSFX);
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