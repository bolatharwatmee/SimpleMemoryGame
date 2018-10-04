using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public Text TimeText;
	public float timeLeft = 60.0f;
	public AudioSource BellSound;

	private GM GameManager;

	void Start(){
		GameManager = GameObject.Find ("GM").GetComponent<GM>();
	}

	void Update()
	{
		timeLeft -= Time.deltaTime;
		int time = (int)timeLeft;
		TimeText.text = "00:"+time.ToString ();

		if(timeLeft < 0)
		{
			BellSound.Play ();
			TimeText.text = "00:00";
			GameManager.GameOver ();
		}
	}
}
