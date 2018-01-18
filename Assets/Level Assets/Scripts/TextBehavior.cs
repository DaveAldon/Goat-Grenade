using UnityEngine;
using UnityEngine.UI;

public class TextBehavior : MonoBehaviour {

    public Text HUD_SheepCount;
    public Text HUD_Score;

    void Start () {
        HUD_SheepCount.text = "nif";
        HUD_Score.text = "Score: 0";
	}
	
	void Update () {
        HUD_SheepCount.text = "Ssheep Remaining: " + GameStats.NumSheep().ToString();
        HUD_Score.text = "Score: " + GameStats.GetPoints().ToString();
	}
}
