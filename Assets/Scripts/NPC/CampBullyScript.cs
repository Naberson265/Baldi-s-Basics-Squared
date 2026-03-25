using UnityEngine;
using UnityEngine.AI;

public class CampBullyScript : MonoBehaviour
{
	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		audioDevice = GetComponent<AudioSource>();
		Reset();
		waitTime = 20f;
	}
	private void Update()
	{
        if (waitTime > 0f)
		{
			waitTime -= Time.deltaTime;
		}
		else
		{
			TargetPlayer();
			transform.position = new Vector3(transform.position.x, 2f, transform.position.z);
		}
	}
	private void FixedUpdate()
	{
		if (waitTime <= 0f)
		{
			Vector3 direction = player.position - transform.position;
			RaycastHit raycastHit;
			if (Physics.Raycast(transform.position, direction, out raycastHit, Mathf.Infinity, rayLayerMask, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player" & bullyRenderer.isVisible & sprite.activeSelf)
			{
				agent.speed = 0f;
			}
			else
			{
				agent.speed = 13f;
			}
		}
		else
		{
			agent.speed = 15f;
		}
	}
	private void TargetPlayer()
	{
		agent.SetDestination(player.position);
	}
    private void OnTriggerEnter(Collider other)
	{
        if (other.transform.tag == "Player")
		{
			if (!(gc.item[0] == 0 & gc.item[1] == 0 & gc.item[2] == 0))
			{
				gc.DeleteSpecificItem(0);
				gc.DeleteSpecificItem(1);
				gc.DeleteSpecificItem(2);
				audioDevice.PlayOneShot(audioSticks);
				Reset();
			}
		}
    }
    private void Reset()
	{
		agent.SetDestination(corner.position);
		waitTime = Random.Range(5f, 45f);
	}
	public float waitTime;
	public bool db;
	public float anger;
	private float forceShowTime;
	public Transform player;
	public Transform corner;
	public CharacterController cc;
	public Transform playerCamera;
	public GameObject sprite;
	public BasicGameController gc;
	private NavMeshAgent agent;
	public Renderer bullyRenderer;
	public AudioSource audioDevice;
	public AudioClip audioSticks;
	public LayerMask rayLayerMask;
}
