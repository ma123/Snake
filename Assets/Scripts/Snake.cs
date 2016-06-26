using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Snake : MonoBehaviour {
	private string headDirection;
	private string lastHeadDirection;
	private float lastFall;
	private Transform lastHeadTransform;
	public List<GameObject> snakeBody;
	public GameObject snakeHead;
	public GameObject tail;

	// Use this for initialization
	void Start () {
		headDirection = "LEFT";
		lastHeadDirection = "LEFT";
	}
	
	// Update is called once per frame
	void Update () {
		// Move Left
		if(Input.GetMouseButtonDown(0)) {
			float partScreen = Screen.width / 3;

			if ((Input.mousePosition.x > 0 && Input.mousePosition.x < partScreen) && lastHeadDirection != "RIGHT") {
				headDirection = "LEFT";
			}

			if (((Input.mousePosition.x > (partScreen * 2)) && Input.mousePosition.x < Screen.width) && lastHeadDirection != "LEFT") {
				headDirection = "RIGHT";
			}

			if ((Input.mousePosition.x >= partScreen) && (Input.mousePosition.x <= (partScreen * 2))) {
				if (((Input.mousePosition.y >= 0) && (Input.mousePosition.y < (Screen.height / 2))) && lastHeadDirection != "UP") {
					headDirection = "DOWN";
				} else {
					headDirection = "UP";
				}
			}
		}


		if (Input.GetKeyDown (KeyCode.LeftArrow) && lastHeadDirection != "RIGHT") 
		{
			headDirection = "LEFT";
		}
		// Move Right
		else if (Input.GetKeyDown (KeyCode.RightArrow) && lastHeadDirection != "LEFT")
		{
			headDirection = "RIGHT";
		}
		// Move Up
		else if (Input.GetKeyDown (KeyCode.UpArrow) && lastHeadDirection != "DOWN")
		{
			headDirection = "UP";
		}
		// Move Down
		else if (Input.GetKeyDown (KeyCode.DownArrow) && lastHeadDirection != "UP")
		{
			headDirection = "DOWN";
		}
		// Move Snake
		else if (Time.time - lastFall >= 0.10f) {
			moveSnake ();
			lastHeadDirection = headDirection;
			lastHeadTransform = this.transform;
			lastFall = Time.time;
		}
	}

	void moveSnake()
	{
		if ( isInsideArea () 
		    && !isTouchingHisTail() ) {
			if( thereIsFood() ){
				eat ();
				tailFollowHead (true);
			}else{
				tailFollowHead ();
			}
			moveHead ();
		} else {
			FindObjectOfType<Score> ().ScoreText.text = "GAME OVER!";
		}
	}

	void moveHead ()
	{
		switch (headDirection){
		case "UP":
			snakeHead.transform.position += new Vector3(0, 1, 0);
			break;
		case "RIGHT":
			snakeHead.transform.position += new Vector3(1, 0, 0);
			break;
		case "DOWN":
			snakeHead.transform.position += new Vector3(0, -1, 0);
			break;
		case "LEFT":
			snakeHead.transform.position += new Vector3(-1, 0, 0);
			break;
		}
	}

	void tailFollowHead (bool excludeLastOne = false)
	{
		Vector3 head = snakeHead.transform.position;

		for(int i = snakeBody.Count - 1; i > 0; i--){
			if(excludeLastOne && i == snakeBody.Count - 1){
				continue;
			}
			snakeBody[i].transform.position = snakeBody[i-1].transform.position;
		}

		snakeBody[0].transform.position = head;

	}

	bool isInsideArea ()
	{
		Vector3 head = snakeHead.transform.position;
		if( head.x >= -9 && head.x <= 9
		   && head.y >= -16 && head.y <= 16 ){
			return true;
		}
		else{
			return false;
		}
	}

	bool isTouchingHisTail ()
	{
		Vector3 head = snakeHead.transform.position;

		for(int i = 0; i < snakeBody.Count; i++){
			Vector3 tail = snakeBody[i].transform.position;
			if( head.x == tail.x
			   && head.y == tail.y ){
				return true;
			}
		}

		return false;
	}

	public bool isTouchingTheSnake(int x, int y)
	{
		Vector3 head = snakeHead.transform.position;

		if(head.x == x && head.y == y){
			return true;
		}
		
		for(int i = 0; i < snakeBody.Count; i++){
			Vector3 tail = snakeBody[i].transform.position;
			if( x == tail.x
			   && y == tail.y ){
				return true;
			}
		}
		
		return false;
	}

	void eat ()
	{
		snakeBody.Add ((GameObject)Instantiate (snakeBody[snakeBody.Count-1],
		                                        snakeHead.transform.position,
		                                        Quaternion.identity));

		FindObjectOfType<Fruit> ().remove();
		FindObjectOfType<Fruit> ().seed ();
		int gameScore = ++FindObjectOfType<Score> ().score;
		FindObjectOfType<Score> ().ScoreText.text = gameScore.ToString();
	}

	bool thereIsFood ()
	{
		GameObject fruit = FindObjectOfType<Fruit> ().fruit;
		if( fruit.transform.position.x == snakeHead.transform.position.x
		   && fruit.transform.position.y == snakeHead.transform.position.y ){
			return true;
		}
		else{
			return false;
		}
	}
}
