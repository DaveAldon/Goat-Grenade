using System.Collections;
using UnityEngine;

public class BombBehavior : MonoBehaviour {

    public Collider fearCollider;
    public GameObject explosionParticle;

    private void OnEnable()
    {
        fearCollider.enabled = false;
    }

    // Called when another collider hits this one and one has a rigidbody
    void OnCollisionEnter(Collision c)
	{
        if (c.gameObject.name == "Ground")
        {
            fearCollider.enabled = true;
            StartCoroutine(BeginDeath());
		}
	}

	IEnumerator BeginDeath()
	{
		yield return new WaitForSeconds(1f);
        GetComponent<MeshRenderer>().enabled = false;
        explosionParticle.SetActive(true);
        yield return new WaitForSeconds(4f);
        Destroy(this.gameObject);
	}
}
