using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.Escape)) {
			print ("Quit game");
			Application.Quit ();
		}
	}

	public void StartGame() {
		SceneManager.LoadSceneAsync ("GameScene");
	}

	public void RateGame() {
		// rating game with stars
	}
}
