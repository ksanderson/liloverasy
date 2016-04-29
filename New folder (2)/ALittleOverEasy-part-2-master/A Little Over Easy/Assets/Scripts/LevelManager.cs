using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	void Update() {
		if (Input.GetButtonDown ("Cancel")) {
			Application.LoadLevel("menu");
		}
	}

	public void LoadScene(string name) { 
		Application.LoadLevel(name);
	} 

	public void QuitGame() { 
		Application.Quit();
	} 
}
