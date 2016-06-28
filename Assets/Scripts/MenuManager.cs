using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI; 

public class MenuManager : MonoBehaviour {
	public GameObject menuPanel;
	private GameObject snakeObject;
	private float difficultySnake;

	void Start() {
		snakeObject = GameObject.FindGameObjectWithTag ("Snake");
		snakeObject.GetComponent<Snake> ().SetSnakeSpeed (10000.0f);
		//Time.timeScale = 0; // pauznutie hry
		menuPanel.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) && !menuPanel.activeSelf) {
			snakeObject.GetComponent<Snake> ().SetSnakeSpeed (10000.0f);
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
		snakeObject.GetComponent<Snake> ().SetSnakeSpeed (0.10f);
		//Time.timeScale = 1; // start hry
	}

	public void ExitGame() {
		print ("exit");
		Application.Quit ();
	}

	public void RateGame() {
		// rating game with stars
	}

	public void DifficultyGame() {
		// set difficulty of game
	}
}