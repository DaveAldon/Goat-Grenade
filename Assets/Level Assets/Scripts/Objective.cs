using UnityEngine;

public class Objective : MonoBehaviour {

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Sheep")
        {
            GameStats.GainPoint();
            c.gameObject.SetActive(false);
        }
    }
}
