using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AngerBar : MonoBehaviour
{

    public Image angerBar;

    public float angryDrop;
    public float normalDrop;

    public float flashYellowThreshold = 0.33f;
    public float flashRedThreshold = 0.66f;

    public float currentAngerlevel = 1f;
    public float demonAngryEverySeconds = 2f;

    public float virginEatingCost = 0.005f;

    private bool _gameOver = false;
    private float _deamonAngerSum = 0;
    private Color _flashColor;
    private Color _normalColor;

    void Start()
    {
        _normalColor = angerBar.color;
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
        angerBar.fillAmount = currentAngerlevel;
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
        angerBar.color = _normalColor;
    }

    IEnumerator BarFlashing(float timeToFlash)
    {
        Color oldColor = angerBar.color;
        while (true)
        {
            if (angerBar.color == _flashColor)
            {
                angerBar.color = oldColor;
            }
            else
            {
                angerBar.color = _flashColor;
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
        currentAngerlevel += virginEatingCost;
    }
}
