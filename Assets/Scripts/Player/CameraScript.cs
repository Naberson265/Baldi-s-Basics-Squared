using System.Collections;
using UnityEngine;


public class CameraScript : MonoBehaviour
{
    void Start()
    {
		killedPlayer = false;
		offset = transform.position - player.transform.position;
        gameObject.GetComponent<Camera>().useOcclusionCulling = true;
		StartCoroutine(LateStart());
	}
	IEnumerator LateStart()
	{
		yield return null;
		ps = FindFirstObjectByType<PlayerController>();
		player = ps.gameObject;
	}
    public void KillCam()
    {
		killedPlayer = true;
        transform.position = baldi.transform.position + baldi.transform.forward * baldiOffset.z + new Vector3(0f, baldiOffset.y, 0f);
		transform.LookAt(new Vector3(baldi.position.x, baldi.position.y + baldiOffset.y, baldi.position.z));
    }
    void Update()
    {
        if (ps.jumpRope)
		{
			velocity -= gravity * Time.deltaTime;
			jumpHeight += velocity * Time.deltaTime;
			if (jumpHeight <= 0f)
			{
				jumpHeight = 0f;
				if (Input.GetButtonDown("Interact"))
				{
					velocity = initVelocity;
				}
			}
			jumpHeightVector = new Vector3(0f, jumpHeight, 0f);
		}
		if (PlayerPrefs.GetInt("LookBackRestriction") != 1) lookBehind = Input.GetAxis("Look Behind") * 180;
		else lookBehind = 0;
    }
    private void LateUpdate()
	{
		if (!killedPlayer)
		{
			transform.position = player.transform.position + offset;
			if (!ps.jumpRope)
			{
				transform.position = player.transform.position + offset;
				transform.rotation = player.transform.rotation * Quaternion.Euler(0f, (float)lookBehind, 0f);
			}
			else if (ps.jumpRope)
			{
				transform.position = player.transform.position + offset + jumpHeightVector;
				transform.rotation = player.transform.rotation * Quaternion.Euler(0f, (float)lookBehind, 0f);
			}
		}
	}
    public float lookBehind;
	public PlayerController ps;
    public Vector3 baldiOffset;
	public bool killedPlayer;
    public Transform baldi;
    public GameObject player;
	public float jumpHeight;
	public Vector3 jumpHeightVector;
	public float initVelocity;
	public float velocity;
	public float gravity;
	public Vector3 offset;
}