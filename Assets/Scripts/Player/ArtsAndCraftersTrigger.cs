using UnityEngine;

public class ArtsAndCraftersTrigger : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && crafters.isActiveAndEnabled)
		{
			crafters.GiveLocation(goTarget.position, false);
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player" && crafters.isActiveAndEnabled)
		{
			crafters.GiveLocation(fleeTarget.position, true);
		}
	}
	public Transform goTarget;
	public Transform fleeTarget;
	public ArtsAndCrafters crafters;
}
