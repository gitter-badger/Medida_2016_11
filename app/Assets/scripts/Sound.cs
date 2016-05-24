using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Sound : MonoBehaviour {

	public static bool muted = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void toggleSound(AudioSource audioSource) {
		if (Game.muted) {
			audioSource.mute = false;
		} else {
			audioSource.mute = true;
		}
	}

	public void toggleSoundString(Text text) {
		if (Game.muted) {
			text.text = "Ton an";
		} else {
			text.text = "Ton aus";
		}
	}
}