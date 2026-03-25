using UnityEngine;

public class OutdoorsTrigger : MonoBehaviour
{
	private void Start()
	{
		hitBox = GetComponent<BoxCollider>();
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
	public PlayerScript player;
	private BoxCollider hitBox;
}