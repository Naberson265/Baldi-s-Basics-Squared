using UnityEngine;

public class SwingingDoorScript : MonoBehaviour
{
	private void Start()
	{
		barrier.enabled = false;
		swingDoorAudio = GetComponent<AudioSource>();
	}
	private void Update()
	{
		if (gameController.notebooks < 2 && gameController.twoNotebooksRequired)
		{
			LockDoor(99);
		}
		if (lockTime > 0f)
		{
			lockTime -= 1f * Time.deltaTime;
			inRenderer.material = locked;
			outRenderer.material = locked;
		}
		else if (DoorLocked)
		{
			UnlockDoor();
		}
		if (openTime > 0f)
		{
			openTime -= 1f * Time.deltaTime;
		}
		if (openTime <= 0f & DoorOpened)
		{
			DoorOpened = false;
			inRenderer.material = closed;
			outRenderer.material = closed;
		}
		barrier.enabled = DoorLocked;
	}
	private void OnTriggerEnter(Collider other)
	{
		if (!DoorLocked && openTime <= 0)
		{
			swingDoorAudio.PlayOneShot(swingingDoorOpen, 1f);
			UIPopupTextManagerWithMovement.Show("Door_SwingOpen", Color.white, transform, 0.75f, 0f);
		}
		if (!DoorLocked && (gameController.notebooks >= 2))
		{
			if (spoopBaldiScript.isActiveAndEnabled && openTime <= 0f && other.tag == "Player")
			{
				spoopBaldiScript.Hear(transform.position, 10f);
			}
			DoorOpened = true;
			inRenderer.material = open;
			outRenderer.material = open;
			openTime = 2.5f;
		}
		else if (gameController.notebooks < 2)
		{
			if (gameController.twoNotebooksRequired)
			{
				swingDoorAudio.Stop();
				swingDoorAudio.PlayOneShot(baldiInfo, 1f);
			}
			else
			{
				if (spoopBaldiScript.isActiveAndEnabled && openTime <= 0f && other.tag == "Player")
				{
					spoopBaldiScript.Hear(transform.position, 10f);
				}
				DoorOpened = true;
				inRenderer.material = open;
				outRenderer.material = open;
				openTime = 2.5f;
			}
		}
	}
	public void LockDoor(float time)
	{
		DoorLocked = true;
		barrier.enabled = true;
		obstacle.SetActive(true);
		if (!(gameController.notebooks < 2))
		{
			lockTime = time;
			inRenderer.material = locked;
			outRenderer.material = locked;
			swingDoorAudio.PlayOneShot(swingingDoorLock, 1f);
			UIPopupTextManagerWithMovement.Show("Door_Slam", Color.white, transform, 1f, 0f);
		}
	}
	public void UnlockDoor()
	{
		if (!(gameController.notebooks < 2))
		{
			DoorLocked = false;
			barrier.enabled = false;
			obstacle.SetActive(false);
			inRenderer.material = closed;
			outRenderer.material = closed;
		}
	}
	public Transform player;
	public MeshCollider barrier;
	public MeshCollider trigger;
	public MeshRenderer inRenderer;
	public MeshRenderer outRenderer;
	public GameObject obstacle;
	public SpoopBaldi spoopBaldiScript;
	public AudioClip swingingDoorOpen;
	public AudioClip baldiInfo;
	public AudioClip swingingDoorLock;
	public Material closed;
	public Material open;
	public Material locked;
	public bool DoorOpened;
	public bool DoorLocked;
	public float openTime;
	public float lockTime;
	private AudioSource swingDoorAudio;
	public GameController gameController;
}