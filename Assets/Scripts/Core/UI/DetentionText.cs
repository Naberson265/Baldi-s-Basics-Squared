using TMPro;
using UnityEngine;

public class DetentionText : MonoBehaviour
{
	private void Start()
	{
		text = GetComponent<TMP_Text>();
	}
	private void Update()
	{
		if (door.lockTime > 0f)
		{
			text.text = "You have detention! \n" + Mathf.CeilToInt(door.lockTime * 1.25f) + " seconds remain!";
		}
		else
		{
			text.text = string.Empty;
		}
	}
	public StandardDoorScript door;
	private TMP_Text text;
}
