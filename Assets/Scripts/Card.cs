using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

	[HideInInspector]
	public Button Button;	
	public GameObject ImageBG;
	public GameObject ImageSource;

	private Animator Anim;
	private GM GameManager;


	void Start () {
		Anim = GetComponent<Animator> ();
		Button = GetComponent<Button> ();
		GameManager = GameObject.Find ("GM").GetComponent<GM>();
	}

	//when Click On Card 
	public void OnCardClick(){
		//check from GM if it can be revele or not 
		if (GameManager.canRevel) {
			GameManager.CardReveled (this);
			Revel ();
		}
	}

	public void Revel(){
		Anim.enabled = true;
		if (Button.transform.rotation.y <= 90f) {
			ImageBG.SetActive (true);
		}
	}
		
	public void UnRevel(){
		ImageBG.SetActive (false);
		Anim.enabled = false;
		//Rest Rotation for Animation
		Button.transform.rotation = new Quaternion (0, 0, 0,0);

	}

	private int _id;
	public int id{
		get { return _id; }		
	}

	//Change Sprites To Images from GM 
	public void ChangeSprite(int id, Sprite image){
		_id = id;
		ImageSource.GetComponent<Image> ().sprite = image;
	}
}


