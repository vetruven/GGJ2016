using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AngerBar : MonoBehaviour
{

    public Scrollbar hungerBar;
    public GameObject deamonHandGameObject;
    public Color flashColor;

    public Text youLastedTextObject;

    public float angryDrop;
    public float normalDrop;
    public float eatJump;

    public float timer = 0;

    public float angerCount = 100f;

    public bool gameOver = false;

    public float deamonAngryTimer = 2f;

    [Space]
    [Space]
    [Space]
    [Space]
    public DeamonHand deamonHand;
    public float hapiness;

    public float deamonHungerSum = 0;

    void Start()
    {
        deamonHand = deamonHandGameObject.GetComponent<DeamonHand>();
    }

    void FixedUpdate()
    {
        if (!IsGameOver())
        {
            timer += Time.deltaTime;
            //if (deamonHungerSum < deamonAngryTimer)
            //{
            //    deamonHungerSum += Time.deltaTime;
            //}
            //else
            //{
            //    deamonHungerSum = 0;
            //    deamonHand.Angry = true;

            //}
        }
        else
        {
            gameOver = true;
            deamonHand.SetGameOver();
            DisplaygameOverMessage();
        }

    }

    void DisplaygameOverMessage()
    {
        youLastedTextObject.text = timer + " Seconds!";
    }

    IEnumerable AngerManagement()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (deamonHungerSum < deamonAngryTimer)
            {
                hungerBar.size -= normalDrop;
                deamonHungerSum += Time.deltaTime;
            }
            else
            {
                deamonHungerSum = 0;
                // change this to work with even manager
                hungerBar.size -= angryDrop;
                deamonHand.Angry = true;
            }
        }
    }
    

    bool IsGameOver()
    {

        if (angerCount == 0)
        {
            if (deamonHand.NumberOfEatensVirgins() == 0)
            {
                return true;
            }
        }
        return false;
    }
}
