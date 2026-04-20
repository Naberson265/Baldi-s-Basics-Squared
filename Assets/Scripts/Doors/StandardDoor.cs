using UnityEngine;
using System.Collections;

public class StandardDoorScript : MonoBehaviour
{
	private void Start()
	{
		doorAudio = GetComponent<AudioSource>();
		StartCoroutine(LateStart());
	}
	IEnumerator LateStart()
	{
		yield return null;
		player = FindFirstObjectByType<PlayerController>().transform;
	}
	private void Update()
	{
		interact = Input.GetButtonDown("Interact");
		if (lockTime > 0f)
		{
			lockTime -= 1f * Time.deltaTime;
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
			barrier.enabled = true;
			DoorOpened = false;
			inRenderer.material = closed;
			outRenderer.material = closed;
            if (silentOpens <= 0)
			{
				doorAudio.PlayOneShot(doorClose, 1f);
				UIPopupTextManagerWithMovement.Show("Door_Slam", Color.white, transform, 0.75f, 0f);
			}
			gameObject.layer = 6;
		}
	}
	public void OpenDoor(bool playerOpened)
	{
		if (silentOpens <= 0 && !DoorOpened)
		{
			if (spoopBaldiScript.isActiveAndEnabled && playerOpened && openTime <= 0f)
			{
				spoopBaldiScript.Hear(transform.position, 10f);
			}
			doorAudio.PlayOneShot(doorOpen, 1f);
		}
		barrier.enabled = false;
		inRenderer.material = open;
		outRenderer.material = open;
        openTime = 2.5f;
		DoorOpened = true;
		gameObject.layer = 7;
		UIPopupTextManagerWithMovement.Show("Door_Open", Color.white, transform, 0.75f, 0f);
	}
	public void LockDoor(float time)
	{
		DoorLocked = true;
		lockTime = time;
		gameObject.layer = 6;
	}
	public void UnlockDoor()
	{
		DoorLocked = false;
	}
	public void SilenceDoor()
	{
		silentOpens = 4;
	}
	private void OnTriggerStay(Collider other)
	{
        if (other.tag == "NPC" && !DoorLocked)
        {
			OpenDoor(false);
		}
	}
	public float openingDistance;
	public Transform player;
	public bool interact;
	public SpoopBaldi spoopBaldiScript;
	public MeshCollider barrier;
	public MeshCollider trigger;
	public MeshRenderer inRenderer;
	public MeshRenderer outRenderer;
	public AudioClip doorOpen;
	public AudioClip doorClose;
	public Material closed;
	public Material open;
	public bool DoorOpened;
	public bool DoorLocked;
	public int silentOpens;
	public float openTime;
	public float lockTime;
	private AudioSource doorAudio;
}