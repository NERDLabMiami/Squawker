using UnityEngine;
using System.Collections;

public class BrokenHeart : MonoBehaviour {
	private CupidStats cupid;
	// Use this for initialization
	void Start () {
		cupid = GameObject.FindGameObjectWithTag("Player").GetComponent<CupidStats>();
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
			cupid.broken();
			Debug.Log("BROKEN HEART LANDED");
		}
	}

	void healed() {
		cupid.heal();
		Destroy(this.gameObject);
	}
}
