using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

    public float x;
    public float z;

    public GameObject terrain;
    public GameObject sheep;
    public TextAsset textFile;

    void Start()
    {
        string text = textFile.text;  //this is the content as string
        string regex = "sheepCount=(.*);";
        MatchCollection coll = Regex.Matches(text, regex);
        int max = int.Parse(coll[0].Groups[1].ToString());
        GameStats.SetMaxSheep(max);
        SpawnSheep();
    }

    void SpawnSheep() {
        for (int i = GameStats.MaxSheep(); i > 0; i--) {
            SetPos();
            Instantiate(sheep, GetPos(), new Quaternion());
        }
    }

    void SetPos()
    {
        var parent = terrain.GetComponent<MeshFilter>().mesh.bounds;
        x = Random.Range(parent.min.x, parent.max.x);
        z = Random.Range(parent.min.z, parent.max.z);
    }

    public Vector3 GetPos()
    {
        Vector3 pos = new Vector3();
        pos.x = x;
        pos.z = z;
        return pos;
    }
}