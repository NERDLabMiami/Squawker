using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour {
	public bool deleteKeyStorage;

	// Use this for initialization
	void Start () {
		if(deleteKeyStorage)
		{
			PlayerPrefs.DeleteAll();
		}

	}
	private void ResetChats()
	{
		//			PlayerPrefs.DeleteKey("chats");
		PlayerPrefs.DeleteAll();
	}

	// Update is called once per frame
	void Update () {
	
	}


}
