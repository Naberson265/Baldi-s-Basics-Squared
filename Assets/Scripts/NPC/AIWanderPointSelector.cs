using UnityEngine;

public class AIWanderPointSelector : MonoBehaviour
{
	public void GetNewTarget()
	{
		id = Mathf.RoundToInt(Random.Range(0f, upperRange));
		transform.position = newLocation[id].position;
	}
	public void GetNewTargetHallway()
	{
		id = Mathf.RoundToInt(Random.Range(0f, upperHallRange));
		transform.position = newLocation[id].position;
	}
	public void GetNewTargetQuarter()
	{
		id = Mathf.RoundToInt(Random.Range(1f, upperHallRange));
		transform.position = newLocation[id].position;
	}
	
	private int id;
	public float upperRange;
	public float upperHallRange;
    public Transform[] newLocation = new Transform[100]; // Most often, unless the map is absolutely HUGE, only a small portion of the locations will be used.
}