using UnityEngine;
using UnityEngine.AI;

public class Principal : MonoBehaviour
{
	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		audioQueue = GetComponent<AudioQueueScript>();
		audioDevice = GetComponent<AudioSource>();
	}
	private void Update()
	{
		if (seesRuleBreak)
		{
			timeSeenRuleBreak += 1f * Time.deltaTime;
			if ((double)timeSeenRuleBreak >= 0.5 & !angry)
			{
				angry = true;
				seesRuleBreak = false;
				timeSeenRuleBreak = 0f;
				TargetPlayer();
				CorrectPlayer();
			}
		}
		else
		{
			timeSeenRuleBreak = 0f;
		}
		if (coolDown > 0f)
		{
			coolDown -= 1f * Time.deltaTime;
		}
	}
	private void FixedUpdate()
	{
		if (!angry)
		{
			Vector3 aim = player.position - transform.position;
			RaycastHit raycastHit;
			if (Physics.Raycast(transform.position, aim, out raycastHit, Mathf.Infinity, rayLayerMask, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player" & playerScript.guilt > 0f & !inOffice & !angry && playerScript.facultyTime <= 0f)
			{
				seesRuleBreak = true;
			}
			else
			{
				seesRuleBreak = false;
				if (agent.velocity.magnitude <= 1f & coolDown <= 0f)
				{
					Wander();
					agent.speed = 20;
				}
			}
			if (Physics.Raycast(transform.position, aim, out raycastHit, Mathf.Infinity, rayLayerMask, QueryTriggerInteraction.Ignore) & raycastHit.transform.name == "ItsABully" & bullyScript.guilt > 0f & !inOffice & !angry)
			{
				TargetBully();
				agent.speed = 20;
			}
		}
		else
		{
			Vector3 aim = player.position - transform.position;
			RaycastHit raycastHit;
			if (Physics.Raycast(transform.position, aim, out raycastHit, Mathf.Infinity, rayLayerMask, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player")
			{
				TargetPlayer();
				agent.speed = 20;
			}
			else
			{
				if (agent.velocity.magnitude <= 1f & coolDown <= 0f)
				{
					Wander();
					agent.speed = 20;
				}
			}
		}
	}
	private void Wander()
	{
		playerScript.principalBugFixer = 1;
		wanderSelector.GetNewTarget();
		agent.SetDestination(wanderTarget.position);
		if (agent.isStopped)
		{
			agent.isStopped = false;
		}
		coolDown = 1f;
		if (Random.Range(0f, 10f) <= 1f && !angry)
		{
			quietAudioDevice.PlayOneShot(aud_Whistle);
			UIPopupTextManagerWithMovement.Show("PRI_Whistle", subtitleColor, transform, aud_Whistle.length, 0f);
		}
	}
	private void TargetPlayer()
	{
		agent.SetDestination(player.position);
		coolDown = 1f;
	}
	public void WhistleItemActivated()
	{
		agent.SetDestination(player.position);
		audioDevice.PlayOneShot(audDontWorry);
		UIPopupTextManagerWithMovement.Show("PRI_DontWorry", subtitleColor, transform, audDontWorry.length, 0f);
		if (PlayerPrefs.GetInt("DietItemModifier") == 1) agent.speed = agent.speed * 3f;
		else agent.speed = agent.speed * 5f;
		coolDown = 1f;
	}
	private void TargetBully()
	{
		if (!bullySeen)
		{
			agent.SetDestination(bully.position);
			audioQueue.QueueAudio(audNoBullying);
			bullySeen = true;
		}
	}
	private void CorrectPlayer()
	{
		audioQueue.ClearAudioQueue();
		if (playerScript.guiltType == "faculty")
		{
			audioQueue.QueueAudio(audNoFaculty);
			UIPopupTextManagerWithMovement.Show("PRI_NoFaculty", subtitleColor, transform, audNoFaculty.length, 0f);
		}
		else if (playerScript.guiltType == "running")
		{
			audioQueue.QueueAudio(audNoRunning);
			UIPopupTextManagerWithMovement.Show("PRI_NoRunning", subtitleColor, transform, audNoRunning.length, 0f);
		}
		else if (playerScript.guiltType == "drink")
		{
			audioQueue.QueueAudio(audNoDrinking);
			UIPopupTextManagerWithMovement.Show("PRI_NoDrink", subtitleColor, transform, audNoDrinking.length, 0f);
		}
		else if (playerScript.guiltType == "escape")
		{
			audioQueue.QueueAudio(audNoEscaping);
			UIPopupTextManagerWithMovement.Show("PRI_NoEscape", subtitleColor, transform, audNoEscaping.length, 0f);
		}
		else if (playerScript.guiltType == "afterhours")
		{
			audioQueue.QueueAudio(audNoAfterHours);
			UIPopupTextManagerWithMovement.Show("PRI_NoAfterHours", subtitleColor, transform, audNoAfterHours.length, 0f);
		}
	}
	private void OnTriggerStay(Collider other)
	{
		if (other.name == "OfficeTrigger")
		{
			inOffice = true;
		}
		if (other.tag == "Player" & angry)
		{
			inOffice = true;
			playerScript.principalBugFixer = 0;
			agent.Warp(principalDetentionPoint.position);
			agent.isStopped = true;
			cc.enabled = false;
			player.transform.position = new Vector3(playerDetentionPoint.position.x, 4f, playerDetentionPoint.position.z);
			cc.enabled = true;
			audioQueue.QueueAudio(aud_Delay);
			audioQueue.QueueAudio(audTimes[detentions]);
			audioQueue.QueueAudio(audDetention);
			int num = Mathf.RoundToInt(Random.Range(0f, 3f));
			audioQueue.QueueAudio(audScolds[num]);
			officeDoor.LockDoor(lockTime[detentions]);
			if (spoopBaldiScript.isActiveAndEnabled) spoopBaldiScript.Hear(transform.position, 95f);
			coolDown = 5f;
			angry = false;
			UIPopupTextManagerWithMovement.Show("PRI_" + lockTime[detentions].ToString(), subtitleColor, transform, audTimes[detentions].length, 0f);
			UIPopupTextManagerWithMovement.Show("PRI_Detention", subtitleColor, transform, audDetention.length, 0f);
			UIPopupTextManagerWithMovement.Show("PRI_Scold" + (num + 1).ToString(), subtitleColor, transform, audScolds[num].length, 0f);
			detentions++;
			if (detentions > 10)
			{
				detentions = 10;
			}
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.name == "OfficeTrigger") inOffice = false;
		if (other.name == "ItsABully") bullySeen = false;
		if (other.name == "OfficeDoorOut" && !officeDoor.DoorLocked)
		{
			officeDoor.openTime = 0.01f;
			officeDoor.LockDoor(officeDoor.lockTime);
		}
		if (other.tag == "LightSource" && playerScript.guiltType == "afterhours" && other.name != "StaticLight" && other.name != "NonVisibleLight")
		{
			other.gameObject.SetActive(false);
			lightManager.SetLighting();
		}
	}
	public bool seesRuleBreak;
	public Transform player;
	public Transform bully;
	public bool bullySeen;
	public PlayerScript playerScript;
	public ItsABully bullyScript;
	public SpoopBaldi spoopBaldiScript;
	public Transform wanderTarget;
	public AIWanderPointSelector wanderSelector;
	public StandardDoorScript officeDoor;
	public Transform principalDetentionPoint;
	public Transform playerDetentionPoint;
	public LightingManager lightManager;
	public float coolDown;
	public float timeSeenRuleBreak;
	public bool angry;
	public bool inOffice;
	private int detentions;
	private float[] lockTime = new float[]
	{
		12f,
		16f,
		20f,
		24f,
		28f,
		32f,
		36f,
		40f,
		44f,
		48f,
		79.2f
	};
	public AudioClip[] audTimes = new AudioClip[11];
	public AudioClip[] audScolds = new AudioClip[4];
	public AudioClip audDetention;
	public AudioClip audNoDrinking;
	public AudioClip audNoEating;
	public AudioClip audNoBullying;
	public AudioClip audNoFaculty;
	public AudioClip audNoLockers;
	public AudioClip audNoRunning;
	public AudioClip audNoStabbing;
	public AudioClip audNoEscaping;
	public AudioClip audNoAfterHours;
	public AudioClip audDontWorry;
	public AudioClip aud_Whistle;
	public AudioClip aud_Delay;
	private NavMeshAgent agent;
	private AudioQueueScript audioQueue;
	private AudioSource audioDevice;
	public AudioSource quietAudioDevice;
	public CharacterController cc;
	public LayerMask rayLayerMask;
	public Color subtitleColor = new Color(0f, 0f, 0.2f);
}
