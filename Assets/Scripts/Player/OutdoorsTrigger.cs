using UnityEngine;
using System.Collections;

public class OutdoorsTrigger : MonoBehaviour
{
	private void Start()
	{
		hitBox = GetComponent<BoxCollider>();
		StartCoroutine(LateStart());
	}
	IEnumerator LateStart()
	{
		yield return null;
		player = FindFirstObjectByType<PlayerController>();
	}
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			player.ResetGuilt("outdoors", 0f);
			if (player.stamina <= player.staminaMax)
			{
				player.stamina += player.staminaDecreaseSpd * Time.deltaTime;
			}
		}
	}
	private void OnTriggerExit(Collider other)
	{
		player.guiltType = "";
	}
	public PlayerController player;
	private BoxCollider hitBox;
}