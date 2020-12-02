using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Tracker : MonoBehaviour
{

    public int player_id;
    public string belief_id;
    public string path;
	public string character;

	// Use this for initialization

    public string gameplayUrl = "http://squawktrack.nerdlab.miami/event.php";

    // Start is called before the first frame update
    void Start()
    {
		DontDestroyOnLoad(this);
		player_id = Random.Range(1, 999999999);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public void Track()
	{
		StartCoroutine(SendGameplay());

	}
	IEnumerator SendGameplay()
	{

		// Create a Web Form
		WWWForm form = new WWWForm();
		form.AddField("player_id", player_id);
		form.AddField("belief_id", belief_id);
		form.AddField("character", character);
		form.AddField("path", path);
		form.AddField("time_elapsed", (int)Time.time);

		// Upload to a cgi script
		using (var w = UnityWebRequest.Post(gameplayUrl, form))
		{
			yield return w.SendWebRequest();
			if (w.result != UnityWebRequest.Result.Success)
			{
				print(w.error);
			}
			else
			{
				print("Recorded Game Play Action");
			}
		}
	}

}
