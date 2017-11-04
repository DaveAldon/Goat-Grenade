using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour, ISelectHandler {

	// This button object's text child.
	private Text myText;

	// Called on startup
	public void Start() {
		myText = gameObject.GetComponentInChildren<Text> ();
	}

	// Set the text color to light green on select.
	public void OnSelect(BaseEventData eventData)
	{
		if (myText.text.Equals ("Start")) {
			SceneManager.LoadScene ("Scene1");
		} else {
			Application.Quit ();
		}
	}
}
