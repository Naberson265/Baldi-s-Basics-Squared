using UnityEngine;
using UnityEngine.AI;

public class GreenBaldloon : MonoBehaviour
{
	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		Wander();
	}
	private void Update()
	{
		if (coolDown > 0f)
		{
			coolDown -= 1f * Time.deltaTime;
		}
	}
	private void FixedUpdate()
	{
		Vector3 direction = player.position - transform.position;
		RaycastHit raycastHit;
		if (Physics.Raycast(transform.position, direction, out raycastHit, Mathf.Infinity, rayLayerMask, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player" & (transform.position - player.position).magnitude <= 50f)
		{
			playerSeen = true;
			TargetPlayer();
		}
		else if (playerSeen && coolDown <= 0f)
		{
			playerSeen = false;
			Wander();
		}
		else if (agent.velocity.magnitude <= 1f & coolDown <= 0f)
		{
			Wander();
		}
	}
	private void Wander()
	{
		wanderer.GetNewTargetHallway();
		agent.SetDestination(wanderTarget.position);
		agent.speed = 10f;
		playerSpotted = false;
		coolDown = 1f;
	}
	private void TargetPlayer()
	{
		agent.SetDestination(player.position);
		agent.speed = 15f;
		coolDown = 0.2f;
		if (!playerSpotted)
		{
			playerSpotted = true;
		}
	}
	public LayerMask rayLayerMask;
	public bool db;
	public bool playerSeen;
	public Transform player;
	public PlayerScript playerScript;
	public Transform wanderTarget;
	public AIWanderPointSelector wanderer;
	public float coolDown;
	public bool playerSpotted;
	private NavMeshAgent agent;
    
}
