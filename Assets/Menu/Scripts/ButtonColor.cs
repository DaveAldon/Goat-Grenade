using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler {

	// Used for the highlighted start button state text.
	private Color darkGreen = new Color(0, 0.5F, 0);
	// Used for the highlighted quit button state text.
	private Color darkRed = new Color(0.5F, 0, 0);

	// This button object's text child.
	private Text myText;

	// Called on startup
	public void Start() {
		myText = gameObject.GetComponentInChildren<Text> ();
	}
	// Set the text color to green on hover.
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (myText.text.Equals ("Start")) {
			myText.color = darkGreen;
		} else {
			myText.color = darkRed;
		}
	}
	// Set the text color to black on hover exit.
	public void OnPointerExit(PointerEventData eventData)
	{
		myText.color = Color.black;
	}

	// Set the text color to light green on select.
	public void OnSelect(BaseEventData eventData)
	{
		if (myText.text.Equals ("Start")) {
			myText.color = Color.green;
		} else {
			myText.color = Color.red;
		}
	}
}
