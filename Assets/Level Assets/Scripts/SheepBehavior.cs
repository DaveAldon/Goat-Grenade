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
    // 4 - dead

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
        // For state observation during dev
        // NOTE that this works fine if there's no textures.
        ColorDebug();

        if (gameObject.GetComponent<BaseSheepAttribute>().health <= 0) {
            // TODO make this less complicated
            state = 4;
        }
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
            case 4:
                Dead();
                break;
		}
	}

    // For debugging the sheep behaviors
    // TODO observations show that fear state has issues and conflicts with graze/wander decisions.
    // Behavior structure needs to change so that they're not all fighting with each other.
    void ColorDebug() {
        Color red = new Color(1,0,0,1);
        Color green = new Color(0, 1, 0, 1);
        Color white = new Color(1, 1, 1, 1);
        Color yellow = new Color(1, 0.92f, 0.016f, 1);
        var obj = gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material;
        //var obj = gameObject.GetComponent<Renderer>().material.color;
        switch (state)
        {
            case 1:
                obj.color = green;
                break;
            case 2:
                obj.color = white;
                break;
            case 3:
                obj.color = yellow;
                break;
            case 4:
                obj.color = red; //obj.SetColor("_Color", c);
                break;
        }
    }

    void OnTriggerEnter(Collider c)
	{
        if (c.tag == "Bomb")
		{
            nearestBomb = c.gameObject;
            state = 3;
		}
	}

    void OnTriggerExit(Collider c)
	{
		if (c.tag == "Bomb")
		{
			state = 1;
		}
	}

    private void Fear() {
        GetComponent<Animator>().Play("Run");
		timer += Time.deltaTime;
		
        if ((timer >= 2) && (nearestBomb != null))
        {
            transform.rotation = Quaternion.LookRotation(transform.position - nearestBomb.transform.position);
            Vector3 newPos = transform.position + transform.forward;
			agent.SetDestination(newPos);
			agent.speed = FleeSpeed;
            timer = 0;
        }
        else state = wantToWalk ? 1 : 2;
    }

    private void Wander() {
        GetComponent<Animator>().Play("Walk");
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
        GetComponent<Animator>().Play("IdleGraze");
		timer += Time.deltaTime;
        if ((timer >= waitTimer) && (!wantToWalk))
        {
            waitTimer = GetRanRange();
        }
		else state = !wantToWalk ? 2 : 1;
    }

    private void Dead() {
        GetComponent<Animator>().Play("Downed");
        //gameObject.GetComponent<SheepBehavior>().enabled = false;
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