using System.Collections;
using UnityEngine;

public class PooBehavior : MonoBehaviour {

	void Start () {
        StartCoroutine(Clean());
	}
	
    public IEnumerator Clean()
    {
        yield return new WaitForSeconds(20);
        Destroy(gameObject);
    }
}
