using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour {

	public GameObject[] StarsImages;
	private GM GameManager;

	void Start(){
		GameManager = GameObject.Find ("GM").GetComponent<GM>();
	}

	void Update(){
		StarsImages [GameManager.Score].SetActive (true);
	}
}
