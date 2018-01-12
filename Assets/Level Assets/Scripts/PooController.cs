using System.Collections;
using UnityEngine;

public class PooController : MonoBehaviour {

    public GameObject pooObj;
    public GameObject pooPosition;
    private WaitForSeconds waitForSeconds;

	void Start () {
        StartCoroutine(ThePoo());
	}

    IEnumerator ThePoo()
    {
        while (Application.isPlaying)
        {
            Instantiate(pooObj, pooPosition.transform.position, new Quaternion());
            yield return new WaitForSeconds(Random.Range(10, 30));
        }
    }
}
