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

    public float currentAngerlevel = 1f;
    public float demonAngryEverySeconds = 2f;

    public float virginWeight = 0.005f;

    private bool _gameOver = false;

    public  float _demonAngryTickSum = 0;
    public  float _demonUpdateTickSum = 0;


    private Color _flashColor;
    private Color _normalColor;

    private bool _gameStarted = false;

    void Start()
    {
        _normalColor = angerBarImage.color;
        RegisterHandlers();
    }

    void RegisterHandlers()
    {
        EventBus.StartGame.AddListener(() =>
        {
            _gameOver = false;
            _gameStarted = true;
        });

        EventBus.EndGame.AddListener(() =>
        {
            _gameStarted = false;
            _gameOver = true;
        });

        //EventBus.UpdateBar.AddListener(() =>
        //{
        //    AngerBarUpdate();
        //});

        EventBus.VirginDied.AddListener(() =>
        {
            VirginEaten();
        });
    }


    void Update()
    {
        
        if (!_gameOver && _gameStarted)
        {
            if (currentAngerlevel == 0)
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
                    Debug.Log("Dispatching demon angry event");
                    _demonAngryTickSum = 0;
                    EventBus.DemonAngry.Dispatch();
                    drop = angryDrop;
                }
                currentAngerlevel -= drop;
                angerBarImage.fillAmount = currentAngerlevel;
            }
        }

    }

    void FlashYellow()
    {
        Debug.Log("Flashing yellow");
        ClearFlash();
        _flashColor = Color.yellow;
        StartCoroutine("BarFlashing", 0.5);
    }

    void FlashRed()
    {
        Debug.Log("Flashing red");
        ClearFlash();
        _flashColor = Color.red;
        StartCoroutine("BarFlashing", 0.2);
    }

    void ClearFlash()
    {
        Debug.Log("Clearing flashing");
        StopCoroutine("BarFlashing");
        angerBarImage.color = _normalColor;
    }

    IEnumerator BarFlashing(float timeToFlash)
    {
        Color oldColor = angerBarImage.color;
        while (true)
        {
            if (angerBarImage.color == _flashColor)
            {
                angerBarImage.color = oldColor;
            }
            else
            {
                angerBarImage.color = _flashColor;
            }
            yield return new WaitForSeconds(timeToFlash);
        }
    }

    public bool IsAngerBarEmpty()
    {
        return currentAngerlevel == 0;
    }

    void VirginEaten()
    {
        currentAngerlevel += virginWeight;
    }
}
