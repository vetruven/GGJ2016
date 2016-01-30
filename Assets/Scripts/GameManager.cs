using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    void Awake()
    {
        
    }

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            EventBus.StartGame.Dispatch();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            EventBus.EndGame.Dispatch();
        }
    }
	
}
