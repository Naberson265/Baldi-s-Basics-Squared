using UnityEngine;
using UnityEngine.AI;

public class FirstPrize : MonoBehaviour
{
	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		coolDown = 1f;
		Wander();
	}
	private void Update()
	{
		if (coolDown > 0f)
		{
			coolDown -= 1f * Time.deltaTime;
		}
		if (autoBrakeCool > 0f)
		{
			autoBrakeCool -= 1f * Time.deltaTime;
		}
		else
		{
			agent.autoBraking = true;
		}
		angleDiff = Mathf.DeltaAngle(transform.eulerAngles.y, Mathf.Atan2(agent.steeringTarget.x - transform.position.x, agent.steeringTarget.z - transform.position.z) * 57.29578f);
		if (movementDisabled <= 0f)
		{
			if (Mathf.Abs(angleDiff) < 5f)
			{
				transform.LookAt(new Vector3(agent.steeringTarget.x, transform.position.y, agent.steeringTarget.z));
				agent.speed = currentSpeed;
			}
			else
			{
				transform.Rotate(new Vector3(0f, turnSpeed * Mathf.Sign(angleDiff) * Time.deltaTime, 0f));
				agent.speed = 0f;
			}
		}
		else
		{
			agent.speed = 0f;
			transform.Rotate(new Vector3(0f, 180f * Time.deltaTime, 0f));
			movementDisabled -= Time.deltaTime;
		}
		motorAudio.pitch = (agent.velocity.magnitude / 50f + 1f) * Time.timeScale;
		UIPopupTextManagerWithMovement.Show("15PR_Motor", Color.white, transform, 0.5f, 0f);
		if (prevSpeed - agent.velocity.magnitude > 40f)
		{
			audioDevice.PlayOneShot(bangSFX);
			UIPopupTextManagerWithMovement.Show("Struct_Vent_Bang", Color.white, transform, 0.5f, 0f);
			if (baldi.isActiveAndEnabled)
			{
				baldi.Hear(transform.position, 64f);
			}
		}
		prevSpeed = agent.velocity.magnitude;
	}
	private void FixedUpdate()
	{
		Vector3 direction = player.position - transform.position;
		RaycastHit raycastHit;
		if (Physics.Raycast(transform.position, direction, out raycastHit, Mathf.Infinity, rayLayerMask, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player")
		{
			if (!isFifthPr || Vector3.Distance(transform.position, raycastHit.transform.position) <= 60f)
			if (!playerSeen && !audioDevice.isPlaying)
			{
				int num = Mathf.RoundToInt(Random.Range(0f, 1f));
				audioDevice.PlayOneShot(foundSFX[num]);
				if (isFifthPr) UIPopupTextManagerWithMovement.Show("5PR_Found" + (num + 1).ToString(), Color.magenta, transform, foundSFX[num].length, 0f);
				else UIPopupTextManagerWithMovement.Show("1PR_Found" + (num + 1).ToString(), Color.cyan, transform, foundSFX[num].length, 0f);
			}
			playerSeen = true;
			TargetPlayer();
			currentSpeed = runSpeed;
		}
		else
		{
			currentSpeed = baseSpeed;
			if (playerSeen & coolDown <= 0f)
			{
				if (!audioDevice.isPlaying)
				{
					int num2 = Mathf.RoundToInt(Random.Range(0f, 1f));
					audioDevice.PlayOneShot(lostSFX[num2]);
					if (isFifthPr) UIPopupTextManagerWithMovement.Show("5PR_Lost" + (num2 + 1).ToString(), Color.magenta, transform, lostSFX[num2].length, 0f);
					else UIPopupTextManagerWithMovement.Show("1PR_Lost" + (num2 + 1).ToString(), Color.cyan, transform, lostSFX[num2].length, 0f);
				}
				playerSeen = false;
				Wander();
			}
			else if (agent.velocity.magnitude <= 1f & coolDown <= 0f & (transform.position - agent.destination).magnitude < 5f)
			{
				Wander();
			}
		}
	}
	private void Wander()
	{
		wanderSelector.GetNewTargetHallway();
		agent.SetDestination(wanderTarget.position);
		hugAnnounced = false;
		int num = Mathf.RoundToInt(Random.Range(0f, 9f));
		if (!audioDevice.isPlaying & num == 0 & coolDown <= 0f)
		{
			int num2 = Mathf.RoundToInt(Random.Range(0f, 1f));
			audioDevice.PlayOneShot(randomSFX[num2]);
			if (isFifthPr) UIPopupTextManagerWithMovement.Show("5PR_Random" + (num + 1).ToString(), Color.magenta, transform, randomSFX[num2].length, 0f);
			else UIPopupTextManagerWithMovement.Show("1PR_Random" + (num + 1).ToString(), Color.cyan, transform, randomSFX[num2].length, 0f);
		}
		coolDown = 1f;
	}
	private void TargetPlayer()
	{
		agent.SetDestination(player.position);
		coolDown = 0.5f;
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			if (!audioDevice.isPlaying & !hugAnnounced)
			{
				int num = Mathf.RoundToInt(Random.Range(0f, 1f));
				audioDevice.PlayOneShot(hugSFX[num]);
				hugAnnounced = true;
				if (isFifthPr) UIPopupTextManagerWithMovement.Show("5PR_Hug" + (num + 1).ToString(), Color.magenta, transform, hugSFX[num].length, 0f);
				else UIPopupTextManagerWithMovement.Show("1PR_Hug" + (num + 1).ToString(), Color.cyan, transform, hugSFX[num].length, 0f);
			}
			agent.autoBraking = false;
		}
		else if (other.name == "WindowOut" && !other.GetComponent<WindowScript>().windowBroken)
		{
			if (agent.velocity.magnitude > 50f)
			{
				other.GetComponent<WindowScript>().BreakWindow(true);
			}
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			autoBrakeCool = 1f;
		}
	}
	public void CutWires()
	{
		movementDisabled = 15f;
	}
	public float turnSpeed;
	public float str;
	public float angleDiff;
	public float baseSpeed;
	public float runSpeed;
	public float currentSpeed;
	public float speed;
	public bool playerSeen;
	public bool isFifthPr;
	public bool follow;
	public Transform player;
	public float autoBrakeCool;
	public Transform wanderTarget;
	public AIWanderPointSelector wanderSelector;
	public AudioClip[] foundSFX = new AudioClip[2];
	public AudioClip[] lostSFX = new AudioClip[2];
	public AudioClip[] hugSFX = new AudioClip[2];
	public AudioClip[] randomSFX = new AudioClip[2];
	public AudioClip bangSFX;
	public AudioSource audioDevice;
	public AudioSource motorAudio;
	public SpoopBaldi baldi;
	public float movementDisabled;
	public bool hugAnnounced;
	public float coolDown;
	private NavMeshAgent agent;
	private float prevSpeed;
	public LayerMask rayLayerMask;
}
