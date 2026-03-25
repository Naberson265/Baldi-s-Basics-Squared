using UnityEngine;

public class FacultyTrigger : MonoBehaviour
{
	private void Start()
	{
		hitBox = GetComponent<BoxCollider>();
	}
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			player.ResetGuilt("faculty", 1f);
		}
	}
	public PlayerScript player;
	private BoxCollider hitBox;
}