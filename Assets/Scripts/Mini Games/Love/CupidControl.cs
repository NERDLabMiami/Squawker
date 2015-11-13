using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CupidControl : MonoBehaviour {
	public float movementIntensity = 5.0f;
	public float force = 300f;
	public GameObject arrow;
	public GameObject startButton;
	public GameObject endButton;
	public GameObject gameContainer;
	public Player player;
	public CupidTalk cupid;
	public AudioSource wings;
	private bool playing = false;


	// Use this for initialization

	void Start () {
		gameContainer.SetActive (true);
		startButton.SetActive(false);
		playing = true;
	}
	
	public void endGame(int healCount) {
		player.setAttractiveness(Mathf.Min(player.attractiveness + healCount, 100));
		playing = false;
		cupid.changeDialog ("end");
		cupid.gameObject.SetActive (true);
		startButton.SetActive (false);
		gameContainer.SetActive (false);
		endButton.SetActive(true);
	}

	public void returnHome() {
		//TODO: Proper transition
		Time.timeScale = 1f;
		Application.LoadLevel(1);
	}
	public void shoot() {
		Vector3 pos = transform.position;
		pos.x += 1f;
		Instantiate(arrow, pos, transform.rotation);		
	}

	// Update is called once per frame
	void Update () {
		if (playing) {
			if (Input.GetButton("Vertical")) {
				if (Input.GetAxis("Vertical") > 0) {
					wings.Play();
					GetComponent<Animator>().SetTrigger("flap");
					transform.GetComponent<Rigidbody2D>().AddForce(Vector2.up * force);
//					transform.position += Vector3.up* movementIntensity * Time.deltaTime;
				}
				else {
					transform.GetComponent<Rigidbody2D>().AddForce(Vector2.down * force);

					//					transform.position += Vector3.down * movementIntensity * Time.deltaTime;
				}
			}
			if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Fire1")) {
				GetComponent<Animator>().SetTrigger("shoot");
				Debug.Log("Fire!");
			}
		
		}
	}
}
