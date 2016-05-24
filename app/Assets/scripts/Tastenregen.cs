using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Tastenregen : MonoBehaviour {

	private GameObject[] currentKeys; // stores current GameObjects (=KEYS)
	private int maxKeys = 6; // maximum amount of simultaniously displayed keys
	//private bool fullSlotted = false; // determinates wether the amount of current keys is equal to maxkeys
	private int countKeys = 0; // absolute Count of dropped keys, debug only
	private int hitsTarget = 20; // count of keys the user has to trigger correctly to win the game
	private int hitsCurrent = 0; // count of keys the user has triggered correctly
	private int parentWidth;
	private int parentHeight;
	private float keyScale;
	private int keyWidth;
	private List<string> keyList;
	private bool[] isTriggered;
	private string[] currentOccupation;



	// Use this for initialization
	void Start () {
		currentKeys = new GameObject[maxKeys];
		RectTransform rt = gameObject.GetComponent<RectTransform> ();
		parentWidth = (int)rt.rect.width;
		parentHeight = (int)rt.rect.height;
		keyScale = (float)(parentWidth) / ((2 + (float)(maxKeys) + ((float)(maxKeys) - 1) * 2) * 100);
		keyWidth = (int)(keyScale * 100);

		keyList = new List<string> ();
		for (char letter = 'A'; letter <= 'Z'; letter++)
		{
			keyList.Add (letter.ToString());
		}
		isTriggered = new bool[maxKeys];

		currentOccupation = new string[maxKeys];
		for (int i = 0; i < maxKeys; i++) {
			currentOccupation [i] = "empty";
		}

	}
	
	// Update is called once per frame
	void Update () {

		// end demo and return to main menu if keys are pressed correctly
		if (hitsCurrent >= hitsTarget) {
			Game.LoadLevel ("scenes/main");
		}

		// add new Keys to the scene, as long as the maximum count is exceeded
		if (!isFullSlotted()) {
			
			// drop key on a certain chance
			float chance = Random.Range(0,120);
			if (chance <= 1) {

				countKeys++;

				// shuffle keys all over the width of the screen
				int offSetX = Random.Range (0, maxKeys); // exclusive right boundary
				int attempts = 0;
				while (attempts < maxKeys) {
					
					/*if (attempts == maxKeys-1)
						fullSlotted = true;*/
					
					if (currentKeys[offSetX] == null) {
						break;
					} else {
						attempts++;
						if (offSetX == maxKeys-1) {
							offSetX = 0;
						} else {
							offSetX++;
						}
					}
				}

				// create Key Object
				GameObject key = getNewKey(offSetX);
				key.name = "KEY at ROW: " + offSetX + " ID: " + countKeys;
				key.transform.Translate (keyWidth * (offSetX * 3f + 1.5f), parentHeight + keyWidth/2, 0);
				// save the reference at the position it belongs to
				currentKeys[offSetX] = key;
			}
		}

		// prepare touch / mouse Interaction
		RaycastHit2D ray;



		// take care of the existing keys
		for (int i = 0; i < maxKeys; i++) {

			if (currentKeys[i] != null) {
				
				GameObject go = currentKeys [i];

				// check for correct user input
				Text keyText = go.GetComponentInChildren<Text>();
				if (Input.GetKey ((KeyCode) System.Enum.Parse(typeof(KeyCode), keyText.text)) && !isTriggered[i]) { // really ugly workaround - works though
					isTriggered [i] = true;
					keyText.color = Color.red;
					hitsCurrent++;
					Text t = GameObject.Find ("Score").GetComponent<Text> ();
					t.text = hitsCurrent + " / " + hitsTarget;
				}
				if (Input.GetMouseButtonDown(0)) {
					ray = Physics2D.Raycast (Camera.main.WorldToViewportPoint (Input.mousePosition), Vector2.zero);
					if (ray.collider != null && ray.transform.gameObject.tag == "keyTarget") {
						Debug.Log("SUCCESS!");
					}
				}

				// move existing keys
				go.GetComponent<RectTransform> ().transform.Translate (0, -2, 0);

				// destroy objects when out of sight
				if (go.GetComponent<RectTransform> ().transform.position.y < -keyWidth/2) {
					currentKeys [i] = null;
					GameObject.Destroy (go);
					isTriggered [i] = false;
					//fullSlotted = false;
				}
			}
		}


	}

	private GameObject getNewKey(int offSetX) {
		
		GameObject key = new GameObject ();
		key.AddComponent<RectTransform> ();
		key.GetComponent<RectTransform> ().SetParent (gameObject.transform);
		key.transform.localScale = new Vector3 (keyScale, keyScale, 1f);
		key.AddComponent<BoxCollider2D> ();
		key.tag = "KeyTarget";

		GameObject keyImg = new GameObject ();
		keyImg.name = "Key Background";
		keyImg.AddComponent<RectTransform> ();
		keyImg.GetComponent<RectTransform> ().SetParent (key.transform);
		Image img = keyImg.AddComponent<Image> ();
		img.color = new Color (255, 170, 0, 255);

		GameObject keyText = new GameObject ();
		keyText.name = "Key Letter";
		keyText.AddComponent<RectTransform> ();
		keyText.GetComponent<RectTransform> ().SetParent (key.transform);

		Text t = keyText.AddComponent<Text> ();
		// ensure no letter is displayed more than once
		bool keyInUse = false;
		string text;
		do {
			keyInUse = false;
			text = keyList[Random.Range(0, keyList.Count)];
			for (int i = 0; i < maxKeys; i++) {
				if (currentOccupation[i].Equals(text))
				{
					keyInUse = true;
				}
			}
		} while(keyInUse);

		currentOccupation [offSetX] = text;
		t.text = text;
		t.alignment = TextAnchor.MiddleCenter;
		t.horizontalOverflow = HorizontalWrapMode.Overflow;
		t.verticalOverflow = VerticalWrapMode.Overflow;
		t.font = (Font)Resources.GetBuiltinResource (typeof(Font), "Arial.ttf");
		t.fontSize = 70;
		t.enabled = true;
		t.color = Color.blue;


		key.SetActive (true);
		return key;
	}

	private bool isFullSlotted() {
		for (int i = 0; i < maxKeys; i++) {
			if (currentKeys [i] == null)
				return false;
		}
		return true;
	}
}
