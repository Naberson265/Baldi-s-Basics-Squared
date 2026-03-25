using UnityEngine;

public class NotebookScript : MonoBehaviour
{
    private void Start()
	{
		collected = false;
	}
    private void Update()
    {   
		Vector3 offset = gameObject.transform.position - player.position;
        distance = offset.magnitude;
        if (gameController.gameMode == "endless")
		{
			if (respawnTime > 0f)
			{
				if ((transform.position - player.position).magnitude > 60f) respawnTime -= Time.deltaTime;
			}
			else if (collected)
			{
				if (distance >= 140f)
				{
					transform.position = new Vector3(transform.position.x, 5f, transform.position.z);
					collected = false;
					notebookAudio.PlayOneShot(respawnSFX);
				}
			}
		}
        if (collected == true) transform.position = new Vector3(transform.position.x, -100f, transform.position.z);
        interact = Input.GetButtonDown("Interact");
    }
    public SpoopBaldi baldi;
    public bool collected;
    public AudioSource notebookAudio;
	public float respawnTime;
    public float distance;
    public bool interact;
    public AudioClip notebookCollect;
    public AudioClip respawnSFX;
    public Transform player;
    public float collectDistance;
	public GameController gameController;
    public GameObject notebookYCTP;
    public string notebookMinigame;
}