using UnityEngine;
using System.Collections;

public class RandomColor : MonoBehaviour {
	// paleta po 4 forbach
	// 4 palety farieb
	public GameObject snakeHead;
	public GameObject tail;
	public GameObject fruit;
	public Camera camera;
	private int randomNumber = 0;

	// Use this for initialization
	void Start () {
		randomNumber = Random.Range (1,5);
		print (randomNumber);
		switch(randomNumber) { 
			case 1: 
			//hlava FFC107FF  telo 18904AFF ovocie D92965FF pozadie 0EC488FF
				snakeHead.GetComponent<SpriteRenderer> ().color = ConvertColor(255, 193, 7, 255);
				tail.GetComponent<SpriteRenderer> ().color = ConvertColor(24, 144, 74, 255);
				fruit.GetComponent<SpriteRenderer> ().color = ConvertColor(217, 41, 101, 255);
				camera.backgroundColor = ConvertColor(14, 196, 136, 255);
			break;
			//hlava 69C6ECFF  telo 2D51BBFF ovocie BF1087FF pozadie FFCD38FF
			case 2: 
				snakeHead.GetComponent<SpriteRenderer> ().color = ConvertColor(105, 198, 236, 255);
				tail.GetComponent<SpriteRenderer> ().color = ConvertColor(45, 81, 187, 255);
				fruit.GetComponent<SpriteRenderer> ().color = ConvertColor(191, 16, 135, 255);
				camera.backgroundColor = ConvertColor(255, 205, 56, 255);
			break;
			//hlava 00FFC3FF  telo 00A37CFF ovocie F5ED79FF pozadie B22284FF
			case 3: 
				snakeHead.GetComponent<SpriteRenderer> ().color = ConvertColor(0, 255, 195, 255);
				tail.GetComponent<SpriteRenderer> ().color = ConvertColor(0, 163, 124, 255);
				fruit.GetComponent<SpriteRenderer> ().color = ConvertColor(245, 237, 121, 255);
				camera.backgroundColor = ConvertColor(178, 34, 132, 255);
			break;
			//hlava F39BEDFF  telo EA44DEFF ovocie F7BD7FFF pozadie 22A0B2FF
			case 4: 
				snakeHead.GetComponent<SpriteRenderer> ().color = ConvertColor(243, 155, 237, 255);
				tail.GetComponent<SpriteRenderer> ().color = ConvertColor(234, 68, 222, 255);
				fruit.GetComponent<SpriteRenderer> ().color = ConvertColor(247, 189, 127, 255);
				camera.backgroundColor = ConvertColor(34, 160, 178, 255);
			break;
		}
	}

    private Color ConvertColor (int r, int g, int b, int a) {
		return new Color(r/255.0f, g/255.0f, b/255.0f, a/255.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
