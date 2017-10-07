using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SheepBehavior : MonoBehaviour {

	public float wanderRadius;
	public float waitTimer;
    public float walkSpeed = 0.3f;
    public bool wantToWalk;

	private Transform target;
	private NavMeshAgent agent;
    private float timer;
    private WaitForSeconds waitForSeconds = new WaitForSeconds(5.0f);
    private const float FleeSpeed = 0.9f;

    public GameObject nearestBomb;

    public int state = 0;
    // state definition:
    // 1 - wander
    // 2 - graze
    // 3 - afraid

	// Use this for initialization
	void OnEnable()
	{
        StartCoroutine(DoCheck());
		agent = GetComponent<NavMeshAgent>();
        agent.speed = walkSpeed;
        state = 1;
        waitTimer = GetRanRange();
        timer = waitTimer;

	}

	IEnumerator DoCheck()
	{
		for (;;)
		{
			wantToWalk = GetRanBool();
            // Not returning a new var reduces garbage
            yield return waitForSeconds;
		}
	}

	// Update is called once per frame
	void Update()
	{
		switch (state)
		{
			case 1:
                Wander();
				break;
			case 2:
                Graze();
				break;
			case 3:
				Fear();
				break;
		}
	}

	void OnCollisionEnter(Collision c)
	{
        if (c.collider.tag == "Bomb")
		{
            nearestBomb = c.gameObject;
            state = 3;
		}
	}

    /*
	void OnCollisionExit(Collision c)
	{
		if (c.gameObject.gameObject.name == "Bomb")
		{
			state = 2;
		}
	}
	*/

    private void Fear() {
		timer += Time.deltaTime;
		
        if (timer >= 1)
        {
			Vector3 newPos = transform.position - nearestBomb.transform.position;
			agent.SetDestination(newPos);
			agent.transform.rotation = SmoothLook(newPos);
			agent.speed = FleeSpeed;
            timer = 0;
            state = 2;
        }
    }

    private void Wander() {
		// While wandering, we count up
		timer += Time.deltaTime;
        // Once we go past predefined time, get new locations and head there
        if ((timer >= waitTimer) && (wantToWalk))
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            agent.transform.rotation = SmoothLook(newPos);
            agent.speed = GetRanSpeed();
            timer = 0;
            // TODO: Change state after wandering?
            // currently we just get a new duration to graze
            waitTimer = GetRanRange();
        }
        else state = wantToWalk ? 1 : 2;
    }

    private void Graze() {
		timer += Time.deltaTime;
        if ((timer >= waitTimer) && (!wantToWalk))
        {
            waitTimer = GetRanRange();
        }
		else state = !wantToWalk ? 2 : 1;
    }

	public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
	{
		Vector3 randDirection = Random.insideUnitSphere * dist;
		randDirection += origin;
		NavMeshHit navHit;
		NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

		return navHit.position;
	}

    bool GetRanBool()
    {
        return Random.value > 0.5f;
    }

	float GetRanRange()
	{
		return Random.Range(0.00f, 6.00f);
	}

	float GetRanSpeed()
	{
		return Random.Range(0.1f, 0.9f);
	}

	// Smooth rotation towards a supplied direction
	Quaternion SmoothLook(Vector3 newDirection)
	{
		return Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(newDirection), Time.deltaTime);
	}
}