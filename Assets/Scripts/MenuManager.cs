using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI; 

public class MenuManager : MonoBehaviour {
	public GameObject menuPanel;
	private GameObject snakeObject;
	private float difficultySnake;
	private int difficulty = 2;
	private float snakeSpeed = 0.12f;
	public Text buttonText;

	void Start() {
		snakeObject = GameObject.FindGameObjectWithTag ("Snake");
		snakeObject.GetComponent<Snake> ().SetSnakeSpeed (100000.0f);

		difficulty = PlayerPrefs.GetInt ("difficulty",2);
		switch(difficulty) {
			case 1:
				buttonText.text = difficulty.ToString ();
				snakeSpeed = 0.2f; 
				break;
			case 2:
				buttonText.text = difficulty.ToString ();
				snakeSpeed = 0.12f; 
				break;
			case 3:
				buttonText.text = difficulty.ToString ();
				snakeSpeed = 0.07f;
				break;
		}


		//Time.timeScale = 0; // pauznutie hry
		menuPanel.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) && !menuPanel.activeSelf) {
			snakeObject.GetComponent<Snake> ().SetSnakeSpeed (100000.0f);
			//Time.timeScale = 0; // pauznutie hry
			menuPanel.SetActive (true);
		} else {
			if (Input.GetKeyDown (KeyCode.Escape) && menuPanel.activeSelf) {
				print ("exit");
				Application.Quit ();
			}
		}
	}

	public void EndGame() {
		int currentScene = SceneManager.GetActiveScene ().buildIndex;
		SceneManager.LoadScene (currentScene, LoadSceneMode.Single);
	}

	public void StartGame() {
		print ("start");
		menuPanel.SetActive(false);
		snakeObject.GetComponent<Snake> ().SetSnakeSpeed (snakeSpeed);
		//Time.timeScale = 1; // start hry
	}

	public void ExitGame() {
		print ("exit");
		Application.Quit ();
	}

	public void RateGame() {
		// rating game with stars
	}

	public void ChangeDifficulty() {
		difficulty++;
		if(difficulty > 3) {
			difficulty = 1;
		}

		switch(difficulty) {
			case 1:
				buttonText.text = difficulty.ToString ();
				snakeSpeed = 0.2f; 
				PlayerPrefs.SetInt ("difficulty", difficulty);
				break;
			case 2:
				buttonText.text = difficulty.ToString ();
				snakeSpeed = 0.12f; 
				PlayerPrefs.SetInt ("difficulty", difficulty);
				break;
			case 3:
				buttonText.text = difficulty.ToString ();
				snakeSpeed = 0.07f;
				PlayerPrefs.SetInt ("difficulty", difficulty);
				break;
		}
	}
}