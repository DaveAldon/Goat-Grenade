using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    GameObject playerCamera;
    string[] killList = { "Sheep", "Wolf", "Temporary"};

    public void StartGame() {
        GetComponent<SpawnManager>().enabled = true;
    }

    public void RestartGame() {
        GetComponent<SpawnManager>().enabled = false;
        foreach (string killTag in killList)
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag(killTag);
            foreach (GameObject go in gos)
                Destroy(go);
        }
        StartGame();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
