using UnityEngine;
using UnityEngine.AI;

public class BsodaNPCEffect : MonoBehaviour
{
	private void Start()
	{
		spawnPos = transform.position;
		agent = GetComponent<NavMeshAgent>();
	}
	private void Update()
	{
		if (inBsoda)
		{
			agent.velocity = otherVelocity;
		}
		if (failSave > 0f)
		{
			failSave -= Time.deltaTime;
		}
		else
		{
			inBsoda = false;
		}
	}
	private void FixedUpdate()
	{
		if (ventTarget != new Vector3(0f, 0f, 0f))
		{
			GetComponent<NavMeshAgent>().enabled = false;
			inVent = true;
			transform.position = Vector3.MoveTowards(transform.position, ventTarget, ventSpeed * Time.deltaTime);
		}
		else 
		{
			GetComponent<NavMeshAgent>().enabled = true;
			inVent = false;
		}
	}
	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "BSODA")
		{
			inBsoda = true;
			otherVelocity = other.GetComponent<Rigidbody>().linearVelocity;
			failSave = 1f;
		}
		else if (other.transform.name == "GottaSweep")
		{
			inBsoda = true;
			otherVelocity = transform.forward * agent.speed * 0.1f + other.GetComponent<NavMeshAgent>().velocity;
			failSave = 1f;
		}
	}
	private void OnTriggerExit()
	{
		inBsoda = false;
	}
	private NavMeshAgent agent;
	private Vector3 otherVelocity;
	private bool inBsoda;
	private float failSave;
	public bool inVent;
	public Vector3 spawnPos;
	public float ventSpeed = 35f;
	public Vector3 ventTarget;
}