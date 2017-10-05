using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class wander : MonoBehaviour {

	public float wanderRadius;
	public float wanderTimer;

	private Transform target;
	private NavMeshAgent agent;
    private float timer;

    public int state = 0;
    // state definition:
    // 1 - wander
    // 2 - graze
    // 3 - afraid

	// Use this for initialization
	void OnEnable()
	{
		agent = GetComponent<NavMeshAgent>();
        state = 1;
        wanderTimer = GetRandomRange();
        timer = wanderTimer;
	}

    float GetRandomRange() {
        return Random.Range(0.00f, 6.00f);
    }

	// Update is called once per frame
	void Update()
	{
        if (state == 1)
        {
            timer += Time.deltaTime;

            if (timer >= wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                timer = 0;
                // TODO: Change state after wandering?
                // currently we just get a new duration to graze
                wanderTimer = GetRandomRange();
            }
        }
	}

	public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
	{
		Vector3 randDirection = Random.insideUnitSphere * dist;
		randDirection += origin;
		NavMeshHit navHit;
		NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

		return navHit.position;
	}
}