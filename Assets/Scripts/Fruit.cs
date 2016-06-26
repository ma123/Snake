using UnityEngine;
using System.Collections;

public class Fruit : MonoBehaviour {
	public GameObject particle;
	public GameObject fruit;

	// Use this for initialization
	void Start () {
		seed ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void seed()
	{
		int x = 0;
		int y;
		do{
			x = Random.Range (-9, 9);
			y = Random.Range (-16, 16);
		}while(FindObjectOfType<Snake> ().isTouchingTheSnake(x, y));

		fruit.transform.position = new Vector3 (x, y, 0);

		Instantiate (fruit,
		             new Vector3 (x, y, 0),
		             Quaternion.identity);
	}

	public void remove()
	{
		GameObject[] currentFruit = GameObject.FindGameObjectsWithTag ("Fruit");
		Instantiate (particle, new Vector3(currentFruit[0].transform.position.x, currentFruit[0].transform.position.y, -0.2f), Quaternion.identity);
		Destroy(currentFruit[0]);
	}

}
