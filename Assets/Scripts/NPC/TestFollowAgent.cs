using UnityEngine;
using UnityEngine.AI;

public class TestFollowAgent : MonoBehaviour
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
		TargetPlayer();
	}
	private void Wander()
	{
		wanderSelector.GetNewTarget();
		agent.SetDestination(wanderTarget.position);
		coolDown = 1f;
	}
	private void TargetPlayer()
	{
		agent.SetDestination(player.position);
		coolDown = 1f;
	}
	public bool follow;
	public Transform player;
	public Transform wanderTarget;
	public AIWanderPointSelector wanderSelector;
	public float coolDown;
	private NavMeshAgent agent;
}
