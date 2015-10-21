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
		player.setAttractiveness(Mathf.Min(player.attractiveness + healCount, 100));
		Time.timeScale = 0f;
		endButton.SetActive(true);
	}

	public void returnHome() {
		//TODO: Proper transition
		Time.timeScale = 1f;
		Application.LoadLevel(1);
	}

	// Update is called once per frame
	void Update () {
		if (playing) {
			if (Input.GetButton("Vertical")) {
				if (Input.GetAxis("Vertical") > 0) {
					GetComponent<Animator>().SetTrigger("flap");

					transform.position += Vector3.up* movementIntensity * Time.deltaTime;
				}
				else {
					transform.position += Vector3.down * movementIntensity * Time.deltaTime;
				}
			}
			if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Fire1")) {
				GetComponent<Animator>().SetTrigger("shoot");
				Instantiate(arrow, transform.position, transform.rotation);		
				Debug.Log("Fire!");
			}
		
		}
	}
}
