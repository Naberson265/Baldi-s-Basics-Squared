using UnityEngine;
using UnityEngine.AI;

public class WindowBreaker : MonoBehaviour
{
	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.name == "WindowOut" && !other.GetComponent<WindowScript>().windowBroken)
		{
			if (agent.velocity.magnitude > 5f) other.GetComponent<WindowScript>().BreakWindow(false);
		}
	}
	private NavMeshAgent agent;
}
