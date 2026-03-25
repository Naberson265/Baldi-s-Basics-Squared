using UnityEngine;

public class BsodaSpray : MonoBehaviour
{
	private void Start()
	{
		rigidBody = GetComponent<Rigidbody>();
		rigidBody.linearVelocity = transform.forward * speed;
	}
	private void Update()
	{
		rigidBody.linearVelocity = transform.forward * speed;
		if (PlayerPrefs.GetInt("DietItemModifier") == 1) lifeSpan -= 3f * Time.deltaTime;
		else lifeSpan -= Time.deltaTime;
		if (lifeSpan < 0f)
		{
			Destroy(gameObject, 0f);
		}
	}
	public float speed;
	public float lifeSpan;
	private Rigidbody rigidBody;
}
