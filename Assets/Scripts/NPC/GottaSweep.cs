using UnityEngine;
using UnityEngine.AI;

public class GottaSweepScript : MonoBehaviour
{
    private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		gottaSweepAudSource = GetComponent<AudioSource>();
		origin = transform.position;
		waitTime = Random.Range(120f, 180f);
	}
    private void Update()
	{
		if (coolDown > 0f)
		{
			coolDown -= 1f * Time.deltaTime;
		}
		if (waitTime > 0f)
		{
			waitTime -= Time.deltaTime;
		}
		else if (!active)
		{
			active = true;
			wandersReached = 0;
			Wander();
			gottaSweepAudSource.PlayOneShot(sweepingTimeSFX);
		}
	}
    private void FixedUpdate()
	{
        if ((double)agent.velocity.magnitude <= 0.1 & coolDown <= 0f & wandersReached < 6 & active)
		{
			Wander();
		}
		else if (wandersReached >= 6)
		{
			EndSweepCycle();
		}
    }
    private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "NPC" || other.tag == "Player")
		{
			gottaSweepAudSource.PlayOneShot(gottaSweepSFX);
		}
	}
    private void EndSweepCycle()
	{
		waitTime = Random.Range(120f, 180f);
        agent.SetDestination(origin);
		wandersReached = 0;
		active = false;
	}
    private void Wander()
	{
		wanderSelector.GetNewTarget();
		agent.SetDestination(wanderTarget.position);
		coolDown = 1f;
        wandersReached++;
	}
    public AIWanderPointSelector wanderSelector;
    public Transform wanderTarget;
	public float coolDown;
	public float waitTime;
	public int wandersReached;
	public bool active;
	private Vector3 origin;
	public AudioClip gottaSweepSFX;
	public AudioClip sweepingTimeSFX;
	private NavMeshAgent agent;
	private AudioSource gottaSweepAudSource;
}
