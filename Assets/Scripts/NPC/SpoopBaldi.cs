using UnityEngine;
using UnityEngine.AI;

public class SpoopBaldi : MonoBehaviour
{
	private void Start()
	{
		praising = false;
		agent = GetComponent<NavMeshAgent>();
		TargetPlayer();
		baldiAudio = GetComponent<AudioSource>();
		timeToMove = baseTime;
		agent = GetComponent<NavMeshAgent>();
		killScript = GetComponent<KillerAgent>();
	}
	private void Update()
	{
		if (timeToMove > 0f) timeToMove -= 1f * Time.deltaTime;
		else Move();
		if (coolDown > 0f) coolDown -= 1f * Time.deltaTime;
		if (baldiTempAnger > 0f) baldiTempAnger -= 0.02f * Time.deltaTime;
		if (antiHearingTime > 0f) antiHearingTime -= Time.deltaTime;
		else antiHearing = false;
		if (endless)
		{
			if (timeToAnger > 0f) timeToAnger -= 1f * Time.deltaTime;
			else
			{
				timeToAnger = angerFrequency;
				GetAngry(angerRate);
				angerRate += angerRateRate;
			}
		}
		if (timeOut)
		{
			if (timeToAnger > 0f) timeToAnger -= 2.5f * Time.deltaTime;
			else
			{
				timeToAnger = angerFrequency;
				GetAngry(angerRate);
				angerRate += angerRateRate;
			}
		}
		if (praising) killScript.killerActive = false;
	}
	private void Move()
	{
		if (transform.position == previous && coolDown < 0f) Wander(); 
		moveFrames = 10f;
		timeToMove = baldiWait - baldiTempAnger;
 		previous = transform.position;
		if (praising == true)
		{
			praising = false;
			killScript.killerActive = true;
		}
		if (killScript.killerActive)
		{
			baldiAnimator.SetTrigger("Slap");
			baldiAudio.PlayOneShot(slap);
			UIPopupTextManagerWithMovement.Show("BAL_Slap", Color.green, transform, 0.5f, 0f);
		}
		else baldiAnimator.SetTrigger("SlapRulerBroken");
	}
	public void GetAngry(float value)
	{
		baldiAnger += value;
		if (baldiAnger < 0.5f) baldiAnger = 0.5f;
		baldiWait = -3f * baldiAnger / (baldiAnger + 2f / baldiSpeedScale) + 3f;
		if (timeToMove < 0.1f) timeToMove = 0.1f;
	}
	public void GetTempAngry(float value)
	{
		baldiTempAnger += value;
	}
	private void FixedUpdate()
	{
		if (moveFrames > 0f)
		{
			moveFrames -= 1f;
			agent.speed = speed;
		}
		else agent.speed = 0f;
		Vector3 direction = player.position - transform.position; 
		RaycastHit raycastHit;
		if (Physics.Raycast(transform.position, direction, out raycastHit, Mathf.Infinity, rayLayerMask, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player")
		{
			db = true;
			TargetPlayer();
		}
		else db = false;
	}
	public void ActivityPraise(float praiseTime)
	{
		praising = true;
		killScript.killerActive = false;
		int praiseID = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 5f));
		timeToMove = timeToMove + praiseTime;
		baldiAnimator.SetTrigger("Praise");
		baldiAudio.PlayOneShot(activityPraises[praiseID]);
		praising = true;
		UIPopupTextManagerWithMovement.Show("BAL_Praise" + (praiseID + 1).ToString(), Color.green, transform, activityPraises[praiseID].length, 0f);
	}
	public void GiveApple(float appleTime)
	{
		praising = true;
		killScript.killerActive = false;
		timeToMove = timeToMove + appleTime;
		baldiAnimator.SetTrigger("Apple");
		baldiAudio.PlayOneShot(appleVoice);
		praising = true;
	}
	private void Wander()
	{
		wanderSelector.GetNewTarget();
		agent.SetDestination(wanderTarget.position);
		coolDown = 1f;
		currentPriority = 0f;
	}
	public void TargetPlayer()
	{
		agent.SetDestination(player.position);
		coolDown = 1f;
		currentPriority = 0f;
	}
	public void Hear(Vector3 soundLocation, float priority)
	{
		if (!antiHearing)
		{
			if (priority >= currentPriority)
			{
				agent.SetDestination(soundLocation);
				currentPriority = priority;
				baldicatorAnimator.SetTrigger("soundHeard");
			}
			else baldicatorAnimator.SetTrigger("soundIgnored");
		}
	}
	public void ActivateAntiHearing(float antiht)
	{
		Wander();
		antiHearing = true;
		antiHearingTime = antiht;
	}
	public void RulerSnap()
	{
		killScript.killerActive = false;
		baldiAudio.PlayOneShot(snap);
		UIPopupTextManagerWithMovement.Show("BAL_RulerSnap", Color.green, transform, 1f, 0f);
	}
	public bool db;
	public bool praising;
	public bool antiHearing;
	public float antiHearingTime;
	public float baseTime;
	public float speed;
	public Transform player;
	public Transform wanderTarget;
	public float currentPriority;
	private AudioSource baldiAudio;
	private float moveFrames;
	public float angerRate;
	public float angerRateRate;
	public float angerFrequency;
	public float timeToAnger;
	public bool endless;
	public bool timeOut;
	public LayerMask rayLayerMask;
	private Vector3 previous;
	public float timeToMove;
	public float baldiAnger;
	public float baldiTempAnger;
	public float baldiWait;
	public AudioClip slap;
	public AudioClip snap;
	public AudioClip appleVoice;
	public Animator baldiAnimator;
	public KillerAgent killScript;
	public Animator baldicatorAnimator;
	public AIWanderPointSelector wanderSelector;
	public float baldiSpeedScale;
	public float coolDown;
	private NavMeshAgent agent;
	public AudioClip[] activityPraises = new AudioClip[6];
}