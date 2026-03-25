using UnityEngine;

public class GeneralBalloon : MonoBehaviour
{
	private void Start()
	{
        rb = gameObject.GetComponent<Rigidbody>();
		ChangeDirection();
	}
	private void Update()
	{
		directionTime -= Time.deltaTime;
		if (directionTime <= 0f)
		{
			ChangeDirection();
			directionTime = Random.Range(5f, 10f);
		}
		rb.linearVelocity = direction * speed;
	}
	private void ChangeDirection()
	{
		direction.x = Random.Range(-1f, 1f);
		direction.z = Random.Range(-1f, 1f);
		direction = direction.normalized;
        speed = Random.Range(0.5f, 2f);
	}
	private Rigidbody rb;
	public float directionTime = 10f;
	public float speed;
	public Vector3 direction;
}
