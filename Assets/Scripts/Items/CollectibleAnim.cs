using UnityEngine;

public class PickupAnim : MonoBehaviour
{
	private void Start()
	{
		itemPosition = GetComponent<Transform>();
	}
	private void Update()
	{
		itemPosition.localPosition = new Vector3(0f, Mathf.Sin((float)Time.frameCount * 0.035f) / 2f, 0f);
	}
	private Transform itemPosition;
}