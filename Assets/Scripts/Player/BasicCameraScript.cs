using UnityEngine;

public class BasicCameraScript : MonoBehaviour
{
	// This is basically a template, used for new modes and cutscene/ending based areas.
	// If you see any empty or generally short functions, it is because other scripts rely on them.
	// They are only kept to make sure the game will function properly in the rare occasion they are needed.
    void Start()
    {
		offset = transform.position - player.transform.position;
    }
    public void KillCam()
    {
		killedPlayer = true;
        transform.position = baldi.transform.position + baldi.transform.forward * baldiOffset.z + new Vector3(0f, baldiOffset.y, 0f);
		transform.LookAt(new Vector3(baldi.position.x, baldi.position.y + baldiOffset.y, baldi.position.z));
    }
    void Update()
    {
        lookBehind = Input.GetAxis("Look Behind") * 180;
    }
    private void LateUpdate()
	{
		transform.position = player.transform.position + offset;
		transform.position = player.transform.position + offset;
		transform.rotation = player.transform.rotation * Quaternion.Euler(0f, (float)lookBehind, 0f);
	}
    public float lookBehind;
	public bool killedPlayer;
    public Vector3 baldiOffset;
    public Transform baldi;
	public BasicPlayerScript bps;
    public GameObject player;
	public Vector3 offset;
}