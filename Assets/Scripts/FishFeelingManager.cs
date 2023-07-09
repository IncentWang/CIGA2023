using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class FishFeelingManager : MonoBehaviour
{
    public static FishFeelingManager Instance;
    public float Feeling; //好感度
    public float Coefficient; //系数
    public float Base; // 基础值

    public float Timer;
    public float GoodTimer;
    public float CooldownTimer;
    public float BaseIncreaseCooldown;
    public bool Changing;
    public bool IsGood;
    public int CurrentLevel;
    public bool Bad;
    public ParticleSystem Love;
    public ParticleSystem Shit;

    private Animator _animator;
    private TouchObject _object;


    public void ChangeFeeling()
    {
        Feeling += Coefficient * Base;
        if (Feeling < UIManager.Instance.LowerLimit)
        {
            Feeling = UIManager.Instance.LowerLimit;
        }

        if (Feeling > UIManager.Instance.UpperLimit)
        {
            Feeling = UIManager.Instance.UpperLimit;
        }
    }

    public void IncreaseBase()
    {
        Base += 0.34f;
    }

    public bool IsPlaying()
    {
        AnimatorStateInfo info = _animator.GetCurrentAnimatorStateInfo(0);
        return !info.IsName("New State") && !info.IsName("Normal");
    }


    public void ResetBase()
    {
        Base = 1.0f;
    }

    public void ChangeTouching(bool touching)
    {
        _animator.SetBool("Touching", touching);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Bind Animator here
        if (scene.buildIndex == 1)
        {
            _animator = GameObject.Find("Fish").GetComponent<Animator>();
            _object = GameObject.Find("Fish").GetComponentInChildren<TouchObject>();
            Love = GameObject.Find("Heart").GetComponent<ParticleSystem>();
            Shit = GameObject.Find("Shit").GetComponent<ParticleSystem>();
            UIManager.Instance.LevelChange(CurrentLevel);
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        if (Changing)
        {
            Timer += Time.deltaTime;
            if (IsGood)
            {
                GoodTimer += Time.deltaTime;
            }
        }
        else
        {
            Timer = 0.0f;
        }

        if (Timer > CooldownTimer)
        {
            Timer = 0.0f;
            ChangeFeeling();
        }

        if (GoodTimer > BaseIncreaseCooldown)
        {
            GoodTimer = 0.0f;
            IncreaseBase();
            if (Base > 2.0f)
            {
                // 播放好评动画，翻身，重置
                ResetBase();
                if (Random.Range(0.0f, 1.0f) > 0.5f)
                {
                    _animator.Play("Base Layer.Haoping1");
                    ChangeTouching(false);
                    Debug.Log("Supposed to play animation");
                }
                else
                {
                    _animator.Play("Base Layer.Haoping2");
                    ChangeTouching(false);
                    Debug.Log("Supposed to play animation");
                }

                Love.Stop();
                _object.ReRandomParts();
            }
        }

        if (Bad)
        {
            Base = -10.0f;
            ChangeFeeling();
            _animator.Play("Chaping");
            ChangeTouching(false);
            _object.ReRandomParts();
            ResetBase();
            Bad = false;
        }

        if (Feeling >= UIManager.Instance.UpperLimit)
        {
            // Level up
            if (CurrentLevel != 3)
            {
                CurrentLevel++;
                UIManager.Instance.LevelChange(CurrentLevel);
            }
        }
    }
}