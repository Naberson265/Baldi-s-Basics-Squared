using UnityEngine;

public class HallQuarter : MonoBehaviour
{
	private void Start()
	{
		wanderPointSelector.GetNewTargetQuarter();
		transform.position = psLocation.position + Vector3.up * 5f;
	}
	public AIWanderPointSelector wanderPointSelector;
	public Transform psLocation;
}