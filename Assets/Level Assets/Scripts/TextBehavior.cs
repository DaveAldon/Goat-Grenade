using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<Text> ().text = "Sheep";
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Text> ().text = "Sheep: " + GameStats.NumSheep ().ToString();
	}
}
