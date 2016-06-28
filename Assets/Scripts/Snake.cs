using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Snake : MonoBehaviour {
	private string headDirection;
	private string lastHeadDirection;
	private float lastFall;
	public List<GameObject> snakeBody;
	public GameObject snakeHead;
	public GameObject tail;
	public GameObject particle;
	private GameObject menuManager;
	private bool oneParticle = true;
	private float snakeSpeed = 1000.0f;
	private bool openMenuPanel = false;

	// Use this for initialization
	void Start () {
		headDirection = "LEFT";
		lastHeadDirection = "LEFT";
		menuManager = GameObject.FindGameObjectWithTag ("MenuManager");
	}
	
	// Update is called once per frame
	void Update () {
		// Move Left
		if(!menuManager.GetComponent<MenuManager>().menuPanel.activeSelf) {
			if(Input.GetMouseButtonDown(0)) {
				float partScreen = Screen.height / 3;

				if ((Input.mousePosition.y > 0 && Input.mousePosition.y < partScreen) && lastHeadDirection != "UP") {
					headDirection = "DOWN";
				}

				if (((Input.mousePosition.y > (partScreen * 2)) && Input.mousePosition.y < Screen.height) && lastHeadDirection != "DOWN") {
					headDirection = "UP";
				}

				if ((Input.mousePosition.y >= partScreen) && (Input.mousePosition.y <= (partScreen * 2))) {
					if (((Input.mousePosition.x >= 0) && (Input.mousePosition.x < (Screen.width / 2))) && lastHeadDirection != "RIGHT") {
						headDirection = "LEFT";
					} else {
						headDirection = "RIGHT";
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
			else if (Time.time >= snakeSpeed + lastFall) {
				moveSnake ();
				lastHeadDirection = headDirection;
				lastFall = Time.time;
			}
		}

		if(openMenuPanel) {
			menuManager.GetComponent<MenuManager> ().EndGame ();
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
			if(oneParticle) {
				Instantiate (particle, new Vector3(snakeHead.transform.position.x, snakeHead.transform.position.y, -0.2f), Quaternion.identity);
				oneParticle = false;
			}

			FindObjectOfType<Score> ().ScoreText.text = "GAME OVER";
			StartCoroutine(Wait());
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

	public void SetSnakeSpeed(float snakeSpeed) {
		this.snakeSpeed = snakeSpeed;
	}

	IEnumerator Wait() { 
		yield return new WaitForSeconds(2);
		openMenuPanel = true;
	}
}
