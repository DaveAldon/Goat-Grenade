using UnityEngine;
using UnityEngine.UI;

public class TextBehavior : MonoBehaviour {

    public Text HUD_SheepCount;

    void Start () {
        HUD_SheepCount.text = "Sheep!";
	}
	
	void Update () {
        HUD_SheepCount.text = "Sheep Remaining: " + GameStats.NumSheep ().ToString();
	}
}
