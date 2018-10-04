using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class GM : MonoBehaviour {
	
	public Sprite[] ImageSprites;
	public Card MainCrad;
	public Transform StarPos;

	//IDs of Sprites
	[HideInInspector]
	public int[] IDs =  {0,0,1,1,2,2};


	// info for Grids
	public const int gridRows = 2;
	public const int gridCols = 3;
	public const float offsetX = 170f;
	public const float offsetY = 120f;

	//that list will collect all Cards
	private List<Card> AllCards = new List<Card>();

	void Start(){
		AddSpritesToImages ();
	}

	void Update(){
		if (Score >= 3) {
			YouWin ();
		}
	}

	//this Function is the responsable for adding sprites to each card and shuufle cards and sort IDs of Cards as the same order as Cards
	void AddSpritesToImages(){
		
		//array created for suffling 
		int[] ShuffledArray = {0,1,2,3,4,5};
		ShuffledArray = ShuffleArray(ShuffledArray);

		Vector3 starPos = StarPos.position;

		//x is just varibale for incremental 
		int x = 0;
		Card card;

		//Nested loop for positioning the Cards 
		for (int i = 0; i < gridCols; i++) {
			for (int j = 0; j < gridRows; j++) {
				
				//create cards and move it under canvas
				card = Instantiate (MainCrad)as Card;
				card.transform.SetParent (GameObject.FindWithTag ("Canvas").transform, false);

				//add IDs and Sprites to Card as same order as ShuffledArray
				int Id = IDs [ShuffledArray [x]];
				card.ChangeSprite (Id, ImageSprites [ShuffledArray [x]]);

				//Move Images To it's Locations
				float posX = (offsetX * i) + starPos.x;
				float posY = (offsetY * j) + starPos.y;
				card.transform.position = new Vector3 (posX, posY, 0f);

				AllCards.Add (card);
				x++;
			}
		}
	}


	private int[] ShuffleArray(int[] array)
	{
		int p = array.Length;
		for (int n = p - 1; n > 0; n--)
		{
			int r = Random.Range(0, n);
			int t = array[r];
			array[r] = array[n];
			array[n] = t;
		}
		return array;
	}

	//--------------------------------------------------------------------s
	//instances of Card for first two cards will revel
	private Card FirstCard;
	private Card SecoundtCard;

	[HideInInspector]
	public int Score = 0 ;
	public AudioSource RightAnswer;
	public Timer Timer;

	//Checks if the Card Can Revel Or not
	public bool canRevel{
		get { return SecoundtCard == null;}
	}


	public void CardReveled(Card Card){
		if (FirstCard == null) {
			FirstCard = Card;
		} else {
			SecoundtCard = Card;
			StartCoroutine (checkMatch ());
		}
	}
		
	private IEnumerator checkMatch(){
		if (FirstCard.id == SecoundtCard.id) {
			Score++;
			RightAnswer.Play ();
		} else {
			yield return new WaitForSeconds (0.5f);
			FirstCard.UnRevel ();
			SecoundtCard.UnRevel ();
		}

		FirstCard = null;
		SecoundtCard = null;
	}

	public void GameOver(){
		for (int i = 0; i <= 5; i++) {
			AllCards [i].Revel ();
			AllCards [i].Button.enabled = false;
		}
	}

	public void YouWin(){
		Timer.TimeText.text = "00:00";
		for (int i = 0; i <= 5; i++) {
			AllCards [i].Button.enabled = false;
		}
	}

}
