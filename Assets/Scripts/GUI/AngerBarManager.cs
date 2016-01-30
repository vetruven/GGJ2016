using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AngerBarManager : MonoBehaviour
{

    public Image angerBarImage;

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

    void RegisterHandlers()
    {
        EventBus.StartGame.AddListener(() =>
        {
            _currentAngerlevel = 1f;
            _gameOver = false;
            _gameStarted = true;
            _demonAngryTickSum = 0;
            _demonUpdateTickSum = 0;
            ClearFlash();
        });

        EventBus.EndGame.AddListener(() =>
        {
            _gameStarted = false;
            _gameOver = true;
        });

        EventBus.VirginDied.AddListener(() =>
        {
            VirginEaten();
            _demonAngryTickSum = 0;

        });
    }


    void Update()
    {

        if (!_gameOver && _gameStarted)
        {
            if (_currentAngerlevel <= 0)
            {
                EventBus.BarEmpty.Dispatch();
                EventBus.DemonAngry.Dispatch();
            }
            if (_demonUpdateTickSum < updateTick)
            {
                _demonUpdateTickSum += Time.deltaTime;
            }
            else
            {
                float drop;
                if (_demonAngryTickSum < demonAngryEverySeconds)
                {
                    drop = normalDrop;
                    _demonAngryTickSum += _demonUpdateTickSum + Time.deltaTime;
                    if (_demonAngryTickSum > demonAngryEverySeconds * flashYellowThreshold)
                    {
                        if (_demonAngryTickSum > demonAngryEverySeconds * flashRedThreshold)
                        {
                            FlashRed();
                        }
                        else
                        {
                            FlashYellow();
                        }
                    }
                    else
                    {
                        ClearFlash();
                    }
                    _demonUpdateTickSum = 0;
                }
                else
                {
                    _demonAngryTickSum = 0;
                    EventBus.DemonAngry.Dispatch();
                    drop = angryDrop;
                }
                _currentAngerlevel -= drop;
                angerBarImage.fillAmount = _currentAngerlevel;
            }
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
        return _currentAngerlevel == 0;
    }

    void VirginEaten()
    {
        _currentAngerlevel += virginWeight;
    }
}
