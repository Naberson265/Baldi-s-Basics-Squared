using UnityEngine;

public class UnlockedSwingingDoorScript : MonoBehaviour
{
	private void Start()
	{
		barrier.enabled = false;
		swingDoorAudio = GetComponent<AudioSource>();
	}
	private void Update()
	{
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
	private void OnTriggerStay(Collider other)
	{
		if (!DoorLocked)
		{
			DoorOpened = true;
			inRenderer.material = open;
			outRenderer.material = open;
			if (openTime <= 0)
			{
				swingDoorAudio.PlayOneShot(swingingDoorOpen, 1f);
			}
			openTime = 2.5f;
		}
	}
	private void LockDoor(float time)
	{
		DoorLocked = true;
		lockTime = time;
		barrier.enabled = true;
		inRenderer.material = locked;
		outRenderer.material = locked;
		swingDoorAudio.PlayOneShot(swingingDoorLock, 1f);
	}
	private void UnlockDoor()
	{
		DoorLocked = false;
		barrier.enabled = false;
		inRenderer.material = closed;
		outRenderer.material = closed;
	}
	public Transform player;
	public MeshCollider barrier;
	public MeshCollider trigger;
	public MeshRenderer inRenderer;
	public MeshRenderer outRenderer;
	public AudioClip swingingDoorOpen;
	public AudioClip swingingDoorLock;
	public Material closed;
	public Material open;
	public Material locked;
	public bool DoorOpened;
	public bool DoorLocked;
	public float openTime;
	public float lockTime;
	private AudioSource swingDoorAudio;
}