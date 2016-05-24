using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour {

	private static BackgroundMusic instance = null;

	public static BackgroundMusic getInstance() {
		return instance;
	}

	void Start() {
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad (this.gameObject);
	}

	void Update() {
		AudioSource music = GetComponent<AudioSource> ();
		if (!SceneManager.GetActiveScene().name.Equals("scenes/main")) {
			music.volume = 0.25f;	
		}

		if (Game.muted) {
			music.mute = true;
		} else {
			music.mute = false;
		}
	}
}

