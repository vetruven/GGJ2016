using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AngerBar : MonoBehaviour
{

    public Scrollbar angerBar;
    public Color flashColor;

    public float angryDrop;
    public float normalDrop;

    public float angerTickSeconds;
    public float currentAnger = 1f;
    public float demonAngryEverySeconds = 2f;

    private bool _gameOver = false;
    private float _deamonAngerSum = 0;

    void Start()
    {
        RegisterHandlers();
        StartCoroutine("AngerManagement");
    }

    void RegisterHandlers()
    {
        EventBus.StartLevel.AddListener(() =>
        {
            StartCoroutine("AngerManagement");
        });

        EventBus.FinishLevel.AddListener(() =>
        {
            _gameOver = true;
        });
    }

    IEnumerator AngerManagement()
    {
        float angerDrop;
        while (!_gameOver)
        {
            yield return new WaitForSeconds(angerTickSeconds);
            if (_deamonAngerSum < demonAngryEverySeconds)
            {
                angerDrop = normalDrop;
                _deamonAngerSum += angerTickSeconds;
            }
            else
            {
                _deamonAngerSum = 0;
                Debug.Log("sending demon angry");
                EventBus.DemonAngry.Dispatch();
                angerDrop= angryDrop;
            }
            currentAnger -= angerDrop;
            angerBar.size = currentAnger;
        }
    }


    public bool IsAngerBarEnded()
    {
        return currentAnger == 0;
    }

    public void VirginEaten(float virginWeight)
    {
        currentAnger += virginWeight;
        if (currentAnger > 1)
        {
            currentAnger = 1;
        }
    }
}
