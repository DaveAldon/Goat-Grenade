using System.Collections;
using UnityEngine;

public class BombBehavior : MonoBehaviour {
	
    // Called when another collider hits this one and one has a rigidbody
	void OnCollisionEnter(Collision c)
	{
        if (c.gameObject.gameObject.name == "Ground")
        {
            StartCoroutine(BeginDeath());
		}
	}

	IEnumerator BeginDeath()
	{
		yield return new WaitForSeconds(50f);
        Destroy(this.gameObject);
	}
}
