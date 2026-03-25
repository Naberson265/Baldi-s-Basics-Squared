using UnityEngine;
using UnityEngine.AI;

public class TestWanderAgent : MonoBehaviour
{
	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
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
		if (agent.velocity.magnitude <= 1f & coolDown <= 0f)
		{
			Wander();
		}
	}
	private void Wander()
	{
		wanderSelector.GetNewTarget();
		agent.SetDestination(wanderTarget.position);
		coolDown = 0.5f;
	}
	private void TargetPlayer()
	{
		agent.SetDestination(player.position);
		coolDown = 0.5f;
	}
	public bool follow;
	public Transform player;
	public Transform wanderTarget;
	public AIWanderPointSelector wanderSelector;
	public float coolDown;
	private NavMeshAgent agent;
}
