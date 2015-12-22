using UnityEngine;
using System.Collections;

public class BrokenHeart : MonoBehaviour {
//	public CupidStats cupid;
	// Use this for initialization
	void Start () {
		//cupid = GameObject.FindGameObjectWithTag("Player").GetComponent<CupidStats>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.tag == "Projectile") {
			gameObject.GetComponent<Animator>().SetTrigger("healed");
			Destroy (coll.gameObject);
		}

		if (coll.gameObject.tag == "Vulnerable") {
			gameObject.GetComponentInParent<BrokenHeartContainer>().cupid.broken();
			Debug.Log("BROKEN HEART LANDED");
		}
	}

	void healed() {
		gameObject.GetComponentInParent<BrokenHeartContainer>().cupid.heal();

		GetComponent<AudioSource> ().Play ();
		Destroy(this.gameObject);
	}

	void broke() {
		gameObject.GetComponentInParent<BrokenHeartContainer>().cupid.broken();
	}
}
