using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour {

    public float x;
    public float z;

    public GameObject terrain;
    public GameObject sheep;
    public GameObject wolf;
    private TextAsset textFile;
    private string config;
    private string sheepCountRegex = "sheepCount=(.*);";
    private string wolfCountRegex = "wolfCount=(.*);";


    private void Awake()
    {
        textFile = Resources.Load(SceneManager.GetActiveScene().name) as TextAsset;
        config = textFile.text;  //this is the content as a string
    }

    void OnEnable()
    {
        SpawnSheep(config);
        // TODO: Wolf spawning will be handled by other timers depending on level
        SpawnWolf(config);
    }

    int GetIntFromRegex(string text, string regex) 
    {
        MatchCollection coll = Regex.Matches(text, regex);
        return int.Parse(coll[0].Groups[1].ToString());
    }

    void SpawnSheep(string text) 
    {
        GameStats.SetMaxSheep(GetIntFromRegex(text, sheepCountRegex));
        for (int i = GameStats.MaxSheep(); i > 0; i--) {
            SetPos();
            Instantiate(sheep, GetPos(), new Quaternion());
        }
    }

    void SpawnWolf(string text)
    {
        for (int i = GetIntFromRegex(text, wolfCountRegex); i > 0; i--)
        {
            SetPos();
            Instantiate(wolf, GetPos(), new Quaternion());
            GameStats.WolfSpawned();
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