using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundButtonText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Text text = GetComponent<Text> ();
		if (Game.muted) {
			text.text = "Ton an";
		} else {
			text.text = "Ton aus";
		}
	}
	
	// Update is called once per frame
	void Update () {
		Start ();
	}
}
