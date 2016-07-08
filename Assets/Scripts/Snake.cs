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

	private Vector2 firstPressPos;
	private Vector2 secondPressPos;
	private Vector2 currentSwipe;
	public AudioClip eatClips;

	// Use this for initialization
	void Start () {
		headDirection = "UP";
		lastHeadDirection = "UP";
		menuManager = GameObject.FindGameObjectWithTag ("MenuManager");
	}
	
	// Update is called once per frame
	void Update () {
		if(!menuManager.GetComponent<MenuManager>().menuPanel.activeSelf) {
			if(Input.GetMouseButtonDown(0)) {
				//save began touch 2d point
				firstPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
			}

			if(Input.GetMouseButtonUp(0)) {
				//save ended touch 2d point
				secondPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);

				//create vector from the two points
				currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

				//normalize the 2d vector
				currentSwipe.Normalize();

				//swipe upwards
				if(currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f && lastHeadDirection != "DOWN")
				{
					headDirection = "UP";
				}
				//swipe down
				if(currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f && lastHeadDirection != "UP")
				{
					headDirection = "DOWN";
				}
				//swipe left
				if(currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f && lastHeadDirection != "RIGHT")
				{
					headDirection = "LEFT";
				}
				//swipe right
				if(currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f && lastHeadDirection != "LEFT")
				{
					headDirection = "RIGHT";
				}
			} else if (Time.time >= snakeSpeed + lastFall) {
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
				snakeHead.GetComponent<SpriteRenderer> ().enabled = false;

				foreach (GameObject body in snakeBody) {
					Instantiate (particle, new Vector3(body.transform.position.x, body.transform.position.y, -0.2f), Quaternion.identity);
					body.GetComponent<SpriteRenderer> ().enabled = false;
				}
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
		AudioSource.PlayClipAtPoint (eatClips, transform.position);
		int gameScore = ++FindObjectOfType<Score> ().score;
		FindObjectOfType<Score> ().ScoreText.text = gameScore.ToString();
		PlayerPrefs.SetInt ("gamescore", gameScore);
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
