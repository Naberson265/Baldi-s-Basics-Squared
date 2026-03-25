using UnityEngine;

public class FarmItsABully : MonoBehaviour
{
    private void Start()
	{
		audioDevice = GetComponent<AudioSource>();
		waitTime = Random.Range(0f, 5f);
	}
	private void Update()
	{
        if (waitTime > 0f)
		{
			waitTime -= Time.deltaTime;
		}
		else if (!active)
		{
			Activate();
		}
        if (active)
		{
			activeTime += Time.deltaTime;
			if (activeTime >= 60f & (transform.position - player.position).magnitude >= 60f)
			{
                audioDevice.PlayOneShot(bored);
				Reset();
			}
		}
		if (guilt > 0f)
		{
			guilt -= Time.deltaTime;
		}
    }
    private void FixedUpdate()
	{
        Vector3 direction = player.position - transform.position;
		RaycastHit raycastHit;
		if (Physics.Raycast(transform.position + new Vector3(0f, 4f, 0f), direction, out raycastHit, Mathf.Infinity, rayLayerMask, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player" & (transform.position - player.position).magnitude <= 30f & active)
		{
			if (!spoken)
			{
				int num = Mathf.RoundToInt(Random.Range(0f, 1f));
				audioDevice.PlayOneShot(taunt[num]);
				spoken = true;
			}
			guilt = 10f;
		}
    }
    private void Activate()
	{
        wanderSelector.GetNewTargetHallway();
		transform.position = wanderTarget.position + new Vector3(0f, 5f, 0f);
		while ((transform.position - player.position).magnitude < 20f)
		{
			wanderSelector.GetNewTargetHallway();
			transform.position = wanderTarget.position + new Vector3(0f, 5f, 0f);
        }
		active = true;
    }
    private void OnTriggerEnter(Collider other)
	{
        if (other.transform.tag == "Player")
		{
			if (gameController.item[0] == 0 & gameController.item[1] == 0 & gameController.item[2] == 0)
			{
				audioDevice.PlayOneShot(noPass);
			}
			else
			{
				int num = Mathf.RoundToInt(Random.Range(0f, 2f));
				while (gameController.item[num] == 0)
				{
					num = Mathf.RoundToInt(Random.Range(0f, 2f));
				}
				gameController.DeleteSpecificItem(num);
				int num2 = Mathf.RoundToInt(Random.Range(0f, 1f));
				audioDevice.PlayOneShot(steal[num2]);
				Reset();
			}
		}
    }
    private void Reset()
	{
		transform.position = new Vector3(transform.position.x, -20f, transform.position.z);
		waitTime = Random.Range(5f, 10f);
        activeTime = 0f;
		active = false;
		spoken = false;
	}
    public Transform player;
	public BasicGameController gameController;
	public Renderer bullyRenderer;
	public Transform wanderTarget;
	public AIWanderPointSelector wanderSelector;
	public float waitTime;
	public float activeTime;
	public float guilt;
	public bool active;
	public bool spoken;
	private AudioSource audioDevice;
	public AudioClip[] taunt = new AudioClip[2];
	public AudioClip[] steal = new AudioClip[2];
    public AudioClip bored;
	public AudioClip noPass;
	public AudioClip achievementSFX;
	public LayerMask rayLayerMask;
}
