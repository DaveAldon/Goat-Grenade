using System;
using System.Collections;
using UnityEngine;

public class BombBehavior : MonoBehaviour {
	private const float EXPLOSION_RADIUS = .75f;
    public Collider fearCollider;
    public GameObject explosionParticle;
    public int dmg = 100;

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
        ExplosionDamage(this.transform.position, EXPLOSION_RADIUS);
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
	}

    void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach(Collider c in hitColliders)
        {
            try
            {
                // We can ask for the sheep attr class specifically if we want too, to specify different dmg levels
                // depending on the critter
				// TODO: Make damage a function of distance from the bomb
                c.gameObject.GetComponent<Attributes>().Damage(dmg);
            }
            #pragma warning disable RECS0022 // A catch clause that catches System.Exception and has an empty body
            #pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception e) { }
            #pragma warning restore CS0168
            #pragma warning restore RECS0022
        }
    }
}
