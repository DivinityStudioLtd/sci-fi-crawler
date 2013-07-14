using UnityEngine;
using System.Collections;

public class AccountUtility : Utility {
	/*
	public string databaseURL;
	
	void Start () {
        //StartCoroutine(SignUp("3","4","4"));	
	}
	
    IEnumerator SignUp (string emailAddress, string userName, string password) {
		WWWForm form = new WWWForm ();
		form.AddField ("account", "signup");
		
		form.AddField ("gameName", gameManager.gameName);
		form.AddField ("emailAddress", emailAddress);
		form.AddField ("userName", userName);
		form.AddField ("password", password);
		
    	WWW download = new WWW (databaseURL, form);
		print ("sending");
		yield return download;
		print ("yielded");
		
   		if (!string.IsNullOrEmpty (download.error)) {
      		print ("Error downloading: " + download.error);
        	return false;
    	} else {
        	Debug.Log (download.text);
        	return true;
   		}
    }		
    IEnumerator Login (string emailAddress, string password) {
		WWWForm form = new WWWForm ();
		form.AddField ("account", "login");
		
		form.AddField ("gameName", gameManager.gameName);
		form.AddField ("emailAddress", emailAddress);
		form.AddField ("password", password);
		
    	WWW download = new WWW (databaseURL, form);
		print ("sending");
		yield return download;
		print ("yielded");
   		if (!string.IsNullOrEmpty (download.error)) {
      		print ("Error downloading: " + download.error);
        	return false;
    	} else {
        	preferanceUtility.name = download.text;
        	return true;
   		}
    }	
    */
}
