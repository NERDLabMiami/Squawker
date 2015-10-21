using UnityEngine;
using System.Collections;

public class ObjectEmitter : MonoBehaviour {
	public GameObject objectToEmit;
	public float randomInterval = 0;
	public float randomInterval2 = 0;
	public GameObject container;
	private float nextSpawnTime;
	// Use this for initialization
	void Start () {
		newSpawnTime();
	}

	void newSpawnTime() {
		nextSpawnTime = Time.time + Random.Range (randomInterval, randomInterval2);

	}
	// Update is called once per frame
	void Update () {
		if (nextSpawnTime < Time.time) {
			GameObject go = (GameObject) Instantiate(objectToEmit, transform.position, transform.rotation);
			go.transform.SetParent(container.transform);
			newSpawnTime();
		}
	}

}
