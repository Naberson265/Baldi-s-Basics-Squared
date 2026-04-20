using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ArtsAndCrafters : MonoBehaviour
{
	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		audioDevice = GetComponent<AudioSource>();
		sprite.SetActive(false);
		StartCoroutine(LateStart());
	}
	IEnumerator LateStart()
	{
		yield return null;
		player = FindFirstObjectByType<PlayerController>().transform;
	}
	private void Update()
	{
		if (forceShowTime > 0f)
		{
			forceShowTime -= Time.deltaTime;
		}
		if (gettingAngry)
		{
			anger += Time.deltaTime;
			if (anger >= 1f & !angry)
			{
				angry = true;
				audioDevice.PlayOneShot(aud_Intro);
				UIPopupTextManagerWithMovement.Show("ANC_Static", Color.white, transform, aud_Intro.length, 0f);
				spriteImage.sprite = angrySprite;
			}
		}
		else if (anger > 0f)
		{
			anger -= Time.deltaTime;
		}
		if (!angry)
		{
			if (((transform.position - agent.destination).magnitude <= 20f & (transform.position - player.position).magnitude >= 60f) || forceShowTime > 0f) //If close to the player and force showtime is less then 0
			{
				sprite.SetActive(true);
			}
			else
			{
				sprite.SetActive(false);
			}
		}
		else
		{
			agent.speed = agent.speed + 60f * Time.deltaTime;
			TargetPlayer();
			UIPopupTextManagerWithMovement.Show("ANC_Static", Color.white, transform, Time.deltaTime, 0f);
			if (!audioDevice.isPlaying)
			{
				audioDevice.PlayOneShot(aud_Loop);
			}
		}
	}
	private void FixedUpdate()
	{
		if (gc.notebooks >= gc.maxNotebooks)
		{
			Vector3 direction = player.position - transform.position;
			RaycastHit raycastHit;
			if (Physics.Raycast(transform.position, direction, out raycastHit, Mathf.Infinity, rayLayerMask, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player" & craftersRenderer.isVisible & sprite.activeSelf && player.GetComponent<PlayerController>().facultyTime <= 0f)
			{
				gettingAngry = true;
			}
			else
			{
				gettingAngry = false;
			}
		}
	}
	public void GiveLocation(Vector3 location, bool flee)
	{
		if (!angry && agent.isActiveAndEnabled)
		{
			agent.SetDestination(location);
			if (flee)
			{
				forceShowTime = 3f;
			}
		}
	}
	private void TargetPlayer()
	{
		agent.SetDestination(player.position);
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" & angry)
		{
			cc.enabled = false;
			player.position = new Vector3(playerTeleportPoint.position.x, 4f, playerTeleportPoint.position.z);
			baldiAgent.Warp(baldiTeleportPoint.position);
			cc.enabled = true;
			gc.DespawnCrafters();
		}
	}
	public bool db;
	public bool angry;
	public bool gettingAngry;
	public float anger;
	private float forceShowTime;
	public Transform player;
	public CharacterController cc;
	public Transform playerCamera;
	public Transform baldiTeleportPoint;
	public Transform playerTeleportPoint;
	public NavMeshAgent baldiAgent;
	public GameObject sprite;
	public GameController gc;
	private NavMeshAgent agent;
	public Renderer craftersRenderer;
	public SpriteRenderer spriteImage;
	public Sprite angrySprite;
	private AudioSource audioDevice;
	public AudioClip aud_Intro;
	public AudioClip aud_Loop;
	public LayerMask rayLayerMask;
}
