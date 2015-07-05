using UnityEngine;
using System.Collections;

public class StringArrayFunctions : MonoBehaviour {
	
	public static string[] getMessage(string message) {
		return message.Split (new string[] {"/"}, System.StringSplitOptions.None);
	}

}
