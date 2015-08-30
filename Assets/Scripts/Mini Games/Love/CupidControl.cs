using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CupidControl : MonoBehaviour {
	public float movementIntensity = 5.0f;
	public GameObject arrow;
	public GameObject startButton;
	public GameObject endButton;
	public Player player;
	private bool playing = false;

	// Use this for initialization

	void Start () {
		Time.timeScale = 0f;
	}

	public void startGame() {
		startButton.SetActive(false);
		playing = true;
		Time.timeScale = 1.0f;
	}

	public void endGame(int healCount) {
		player.setAttractiveness(healCount);
		Time.timeScale = 0f;
		endButton.SetActive(true);
//		startButton.SetActive(true);
	}

	public void returnHome() {
		//TODO: Proper transition
		Time.timeScale = 1f;
		Application.LoadLevel(0);
	}

	// Update is called once per frame
	void Update () {
		if (playing) {
			if (Input.GetButton("Horizontal")) {
				if (Input.GetAxis("Horizontal") < 0) {
					transform.position += Vector3.left * movementIntensity * Time.deltaTime;
				}
				else {
					transform.position += Vector3.right * movementIntensity * Time.deltaTime;
				}
			}
			if (Input.GetButtonDown("Vertical") || Input.GetButtonDown("Fire1")) {
				Instantiate(arrow, transform.position, transform.rotation);		
				Debug.Log("Fire!");
			}
		
		}
	}
}
