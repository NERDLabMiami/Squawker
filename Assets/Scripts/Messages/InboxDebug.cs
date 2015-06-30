using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;

public class InboxDebug : MonoBehaviour {
/*
	public TextAsset characters;
	public GameObject messageTemplate;
	private JSONNode json;
	private List<string> inbox;
*/
	public Inbox inbox;
	
	public void advanceDay() {
		for(int i = 0; i < inbox.getCount(); i++) {
			//iterate through inbox, reduce wait time for each message
		//	string[] message = inbox[i].Split (new string[] {"/"}, System.StringSplitOptions.None);
				inbox.reduceDuration(i, 1);
			}

		inbox.save();
		inbox.show();
	}
	


}
