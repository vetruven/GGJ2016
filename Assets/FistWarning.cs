using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FistWarning : MonoBehaviour {

	public static FistWarning Instance;

	public Image notifier;
	public GameObject container;
	float currentSeconds;
	float nextInterval;


	// Use this for initialization
	void Start () {
		Instance = this;
	}

	public void ResetTimer(float seconds) {
		nextInterval = seconds;
		container.SetActive(true);
		//Debug.LogWarning("Received " + seconds + " seconds");
	}
	
	// Update is called once per frame
	void Update () {
		if(nextInterval == 0f)
			return;

		currentSeconds += Time.deltaTime;
		notifier.fillAmount = currentSeconds / nextInterval;
		//Debug.Log("fill amount: " + notifier.fillAmount);

		if(currentSeconds > nextInterval)
		{
			nextInterval = 0;
			currentSeconds = 0;
			notifier.fillAmount = 0;
			container.SetActive(false);
		}
	}
}
