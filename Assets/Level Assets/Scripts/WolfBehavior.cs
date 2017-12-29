using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class WolfBehavior : Attributes {
	// The state machine states
	private const int WANDERING = 1;
	private const int CHASING   = 2;
	private const int DEAD      = 3;
	// How long to wait before updating wantToWalk.
	private const float WAIT_TIME = 5.0f;
	// How far away the wolf can attack from.
	private const float ATTACK_RANGE = 1.0f;
	// How much damage an attack inflicts.
	private const int ATTACK_DAMAGE = 10;
	// How fast attacks occur. 1/ATTACK_SPEED is the time between attacks in seconds.
	private const int ATTACK_SPEED = 1;

	public float wanderRadius;
	public float waitTimer;
	public float walkSpeed = 0.6f;
	public bool wantToWalk;

	private Transform target;
	private NavMeshAgent agent;
	private float timer;
	private WaitForSeconds waitForSeconds = new WaitForSeconds(WAIT_TIME);
	private const float chaseSpeed = 0.9f;

	public GameObject nearestBomb;
	public GameObject nearestSheep;

	public int state = WANDERING;

	// Use this for initialization
	void OnEnable()
	{
		StartCoroutine(DoCheck());
		agent = GetComponent<NavMeshAgent>();
		agent.speed = walkSpeed;
		health = 150;
		state = WANDERING;
		waitTimer = GetRanRange();
		timer = waitTimer;

	}

	// Get a random boolean value every WAIT_TIME number of seconds.
	IEnumerator DoCheck()
	{
		for (;;)
		{
			// Not returning a new var reduces garbage
			yield return waitForSeconds;
			wantToWalk = GetRanBool();
		}
	}

	// Update is called once per frame
	void Update()
	{
		// For state observation during dev
		// NOTE that this works fine if there's no textures.
		ColorDebug();

		if (health <= 0) {
			// TODO make this less complicated
			state = DEAD;
		}

		switch (state)
		{
		case WANDERING:
			Wander();
			break;
		case CHASING:
			Chase();
			break;
		case DEAD:
			Downed();
			break;
		default:
			Debug.Assert (true); // This should never be hit.
			break;
		}
	}

	// For debugging the sheep behaviors
	// TODO observations show that fear state has issues and conflicts with graze/wander decisions.
	// Behavior structure needs to change so that they're not all fighting with each other.
	void ColorDebug() {
		switch (state)
		{
		case WANDERING:
			gameObject.GetComponent<Renderer>().material.color = Color.green;
			break;
		case CHASING:
			gameObject.GetComponent<Renderer>().material.color = Color.white;
			break;
		case DEAD:
			gameObject.GetComponent<Renderer>().material.color = Color.yellow;
			break;
		}
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.tag == "Bomb") {
			nearestBomb = c.gameObject;
			state = WANDERING;
		} 
		// Only chase a sheep that is alive. Targets are only updated after the previously chased sheep is dead.
		else if ((c.tag == "Sheep") && (c.GetComponent<Attributes>().health > 0) && (nearestSheep == null)) { 
			nearestSheep = c.gameObject;
			state = CHASING;
		}
	}

	void OnTriggerExit(Collider c)
	{
		if (c.tag == "Bomb") {
			state = WANDERING;
		}
	}
		
	private void Wander() {
		// TODO: Add wolf wander animation here
		// While wandering, we count up
		timer += Time.deltaTime;
		// Once we go past predefined time, get new locations and head there
		if ((timer >= waitTimer) && (wantToWalk)) {
			Vector3 newPos = RandomNavSphere (transform.position, wanderRadius, -1);
			agent.SetDestination (newPos);
			agent.transform.rotation = SmoothLook (newPos);
			agent.speed = GetRanSpeed();
			timer = 0;
			// TODO: Change state after wandering?
			// currently we just get a new duration to graze
			waitTimer = GetRanRange ();
		}
	}

	/// <summary>
	/// Chases the nearest sheep and attacks it if possible.
	/// </summary>
	private void Chase() {
		// TODO: Add wolf chase animation here
		if ((nearestSheep != null) && (nearestSheep.GetComponent<Attributes>().health > 0)) {
			transform.rotation = Quaternion.LookRotation(nearestSheep.transform.position - transform.position);
			Vector3 newPos = transform.position + transform.forward;
			agent.SetDestination (newPos);
			agent.speed = chaseSpeed;

			Attack(); // Attack the sheep if possible.
		} else {
			nearestSheep = null;
			state = WANDERING;
		}
	}

	private void Downed() {
		// TODO: Add wolf downed animation.
		//GetComponent<Animator>().Play("Downed");
		//gameObject.GetComponent<WolfBehavior>().enabled = false;
	}

	/// <summary>
	/// Applies damage to the nearest sheep if within range and the attack timeout has elapsed.
	/// </summary>
	private void Attack() {
		timer += Time.deltaTime;
		if ((timer >= (1/ATTACK_SPEED)) && (Vector3.Distance(nearestSheep.transform.position, transform.position) < ATTACK_RANGE)) {
			// TODO: Add wolf attack animation here
			nearestSheep.gameObject.GetComponent<Attributes>().Damage(ATTACK_DAMAGE);
			timer = 0;
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