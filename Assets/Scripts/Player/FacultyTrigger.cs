using UnityEngine;
using System.Collections;

public class FacultyTrigger : MonoBehaviour
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
			player.ResetGuilt("faculty", 1f);
		}
	}
	public PlayerController player;
	private BoxCollider hitBox;
}