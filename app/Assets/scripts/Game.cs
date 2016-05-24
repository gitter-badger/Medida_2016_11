using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

	public static bool muted = true;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setMuted() {
		Game.muted = true;
	}

	public void setUnMuted() {
		Game.muted = false; 
	}

	public void toggleMute() {
		if (Game.muted) {
			Game.muted = false;
		} else {
			Game.muted = true;
		}
	}

	public void quit() {
		Application.Quit();
	}

	public void loadLevel(string level) {
		Game.LoadLevel (level);
	}

	public static void LoadLevel(string level) {
		SceneManager.LoadScene (level);
	}
}
