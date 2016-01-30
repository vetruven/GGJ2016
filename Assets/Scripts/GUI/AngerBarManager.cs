
﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AngerBarManager : MonoBehaviour
{

    public Image angerBarImage;

    //Minimum time frame to check update
    public float updateTick;

    public float angryDrop;
    public float normalDrop;

    public float flashYellowThreshold = 0.33f;
    public float flashRedThreshold = 0.66f;


    public float demonAngryEverySeconds = 2f;

    public float virginWeight = 0.005f;

    private bool _gameOver = false;

    public float _demonAngryTickSum;
    public float _demonUpdateTickSum;

    private float _currentAngerlevel = 1f;

    private Color _flashColor;
    private Color _normalColor;

    private bool _gameStarted = false;

    [Header("Ignore all above me")]
    public float startLifetime;
    public AnimationCurve peopleToTime;

    private float currentLifetime;

    struct Flash
    {
        public Color flashColor;
        public Color prevColor;
        public float timeToFlash;
    }

    void Start()
    {
        _normalColor = angerBarImage.color;
        RegisterHandlers();
    }
    void OnEnable()
    {
        currentLifetime = startLifetime;
        _gameOver = false;
        _gameStarted = true;
    }

    void OnDisable()
    {
        _gameOver = true;
        _gameStarted = false;
    }
    void RegisterHandlers()
    {
        EventBus.StartGame.AddListener(() =>
        {
            _currentAngerlevel = 1f;
            _gameOver = false;
            _gameStarted = true;
            _demonAngryTickSum = 0;
            _demonUpdateTickSum = 0;

            currentLifetime = startLifetime;
            ClearFlash();
        });

        EventBus.EndGame.AddListener(() =>
        {
            _gameStarted = false;
            _gameOver = true;
        });

        EventBus.TotalVirginsDied.AddListener((deaths) =>
        {
            VirginEaten(deaths);
        });
    }


    void Update()
    {

        if (!_gameOver && _gameStarted)
        {
            if (currentLifetime <= 0)
            {
                EventBus.BarEmpty.Dispatch();
                //EventBus.DemonAngry.Dispatch();
            }

            currentLifetime = Mathf.Clamp(currentLifetime, 0, startLifetime);
            currentLifetime -= Time.deltaTime;
            currentLifetime = Mathf.Max(currentLifetime, 0);
            angerBarImage.fillAmount = Mathf.Clamp(currentLifetime / startLifetime, 0, 1);
        }
    }

    void FlashYellow()
    {
        ClearFlash();
        _flashColor = Color.yellow;
        Flash fl;
        fl.flashColor = Color.yellow;
        fl.prevColor = Color.green;
        fl.timeToFlash = 0.5f;

        StartCoroutine("BarFlashing", fl);
    }

    void FlashRed()
    {
        ClearFlash();
        Flash fl;
        fl.flashColor = Color.red;
        fl.prevColor = Color.yellow;
        fl.timeToFlash = 0.2f;
        _flashColor = Color.red;
        StartCoroutine("BarFlashing", fl);
    }

    void ClearFlash()
    {
        StopCoroutine("BarFlashing");
        angerBarImage.color = _normalColor;
    }

    IEnumerator BarFlashing(Flash fl)
    {
        //Color oldColor = fl.prevColor;

        while (true)
        {
            if (angerBarImage.color == fl.flashColor)
            {
                angerBarImage.color = fl.prevColor;
            }
            else
            {
                angerBarImage.color = fl.flashColor;
            }
            yield return new WaitForSeconds(fl.timeToFlash);
        }
    }

    public bool IsAngerBarEmpty()
    {
        return currentLifetime == 0;
    }

    void VirginEaten(int deaths)
    {
        float adjustedWeight = deaths;
        if (peopleToTime != null)
        {
            adjustedWeight = peopleToTime.Evaluate(adjustedWeight);
        }
        currentLifetime += adjustedWeight;
    }
}
