using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AngerBarManager : MonoBehaviour
{

    public Image angerBarImage;

    public float angryDrop;
    public float normalDrop;

    public float flashYellowThreshold = 0.33f;
    public float flashRedThreshold = 0.66f;

    public float currentAngerlevel = 1f;
    public float demonAngryEverySeconds = 2f;

    public float virginWeight = 0.005f;

    private bool _gameOver = false;
    private float _deamonAngerSum = 0;
    private Color _flashColor;
    private Color _normalColor;

    void Start()
    {
        _normalColor = angerBarImage.color;
        RegisterHandlers();
        //StartCoroutine("AngerManagement");
    }

    void RegisterHandlers()
    {
        EventBus.StartLevel.AddListener(() =>
        {
        });

        EventBus.FinishLevel.AddListener(() =>
        {
            _gameOver = true;
        });

        EventBus.UpdateBar.AddListener(() =>
        {
            AngerBarUpdate();
        });

        EventBus.VirginDied.AddListener(() => {
            VirginEaten();
        });
    }

    void AngerBarUpdate()
    {
        float drop;

        if (_deamonAngerSum < demonAngryEverySeconds)
        {
            drop = normalDrop;
            _deamonAngerSum += Time.deltaTime;
            if (_deamonAngerSum > _deamonAngerSum * flashYellowThreshold)
            {
                if (_deamonAngerSum > _deamonAngerSum * flashRedThreshold)
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
        }
        else
        {
            _deamonAngerSum = 0;
            EventBus.DemonAngry.Dispatch();
            drop = angryDrop;
        }
        currentAngerlevel -= drop;
        angerBarImage.fillAmount = currentAngerlevel;
    }

    void FlashYellow()
    {
        StopCoroutine("BarFlashing");
        _flashColor = Color.yellow;
        StartCoroutine("BarFlashing", 0.5);
    }

    void FlashRed()
    {
        StopCoroutine("BarFlashing");
        _flashColor = Color.red;
        StartCoroutine("BarFlashing", 0.2);
    }

    void ClearFlash()
    {
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

    public bool IsAngerBarEnded()
    {
        return currentAngerlevel == 0;
    }

    void VirginEaten()
    {
        currentAngerlevel += virginWeight;
    }
}
