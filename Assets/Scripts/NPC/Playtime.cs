using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Playtime : MonoBehaviour
{
	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		audioDevice = GetComponent<AudioSource>();
		Wander();
		StartCoroutine(LateStart());
	}
	IEnumerator LateStart()
	{
		yield return null;
		playerScript = FindFirstObjectByType<PlayerController>();
		player = playerScript.transform;
	}
	private void Update()
	{
		if (coolDown > 0f)
		{
			coolDown -= 1f * Time.deltaTime;
		}
		if (playCool >= 0f)
		{
			playCool -= Time.deltaTime;
		}
		else if (animator.GetBool("sad"))
		{
			playCool = 0f;
			animator.SetBool("sad", false);
		}
	}
	private void FixedUpdate()
	{
		if (!playerScript.jumpRope)
		{
			Vector3 direction = player.position - transform.position;
			RaycastHit raycastHit;
			if (Physics.Raycast(transform.position, direction, out raycastHit, Mathf.Infinity, rayLayerMask, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player" & (transform.position - player.position).magnitude <= 40f & playCool <= 0f && playerScript.facultyTime <= 0f)
		    {
				playerSeen = true;
				TargetPlayer();
			}
			else if (playerSeen & coolDown <= 0f)
			{
				playerSeen = false;
				Wander();
			}
			else if (agent.velocity.magnitude <= 1f & coolDown <= 0f)
			{
				Wander();
			}
			jumpRopeStarted = false;
		}
		else
		{
			if (!jumpRopeStarted)
			{
				agent.Warp(transform.position - transform.forward * 10f);
			}
			jumpRopeStarted = true;
			agent.speed = 0f;
			playCool = 20f;
		}
	}
	private void Wander()
	{
		wanderer.GetNewTargetHallway();
		agent.SetDestination(wanderTarget.position);
		agent.speed = 15f;
		playerSpotted = false;
		audVal = Mathf.RoundToInt(Random.Range(0f, 1f));
		if (!audioDevice.isPlaying)
		{
			audioDevice.PlayOneShot(aud_Random[audVal]);
		}
		coolDown = 1f;
	}
	private void TargetPlayer()
	{
		animator.SetBool("sad", false);
		agent.SetDestination(player.position);
		agent.speed = 20f;
		coolDown = 0.2f;
		if (!playerSpotted)
		{
			playerSpotted = true;
			audioDevice.PlayOneShot(aud_LetsPlay);
		}
	}
	public void Disappoint()
	{
		animator.SetBool("sad", true);
		audioDevice.Stop();
		audioDevice.PlayOneShot(aud_Sad);
	}
	public LayerMask rayLayerMask;
	public bool db;
	public bool playerSeen;
	public bool disappointed;
	public int audVal;
	public Animator animator;
	public Transform player;
	public PlayerController playerScript;
	public Transform wanderTarget;
	public AIWanderPointSelector wanderer;
	public float coolDown;
	public float playCool;
	public bool playerSpotted;
	public bool jumpRopeStarted;
	private NavMeshAgent agent;
	public AudioClip[] aud_Numbers = new AudioClip[10];
	public AudioClip[] aud_Random = new AudioClip[2];
	public AudioClip aud_Oops;
	public AudioClip aud_LetsPlay;
	public AudioClip aud_Congrats;
	public AudioClip aud_ReadyGo;
	public AudioClip aud_Sad;
	public AudioSource audioDevice;
}
