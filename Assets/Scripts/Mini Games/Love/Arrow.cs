using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {
	// Use this for initialization
	private float startTime;
	private float endTime;
	public float speed = 10f;
	void Start () {
		startTime = Time.realtimeSinceStartup;
		endTime = startTime + 3f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += Vector3.up * speed * Time.deltaTime;
		if (endTime < Time.realtimeSinceStartup) {
			Destroy (this.gameObject);
		}
	}

}
