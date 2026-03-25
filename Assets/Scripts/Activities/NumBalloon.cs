using UnityEngine;

public class NumBalloon : MonoBehaviour
{
    public void Start()
    {
        currentlyHeld = false;
        origin = transform.position;
        poppingTime = UnityEngine.Random.Range(0.5f, 3f);
    }
    public void Update()
    {
        interact = Input.GetButtonDown("Interact");
        if (currentlyHeld)
        {
            transform.position = player.position + (player.forward * 3f) + new Vector3(0f, -2f, 0f);
            gameObject.layer = 2;
        }
        else
        {
            transform.position = new Vector3(transform.position.x, origin.y, transform.position.z);
            gameObject.layer = 7;
        }
		if (poppingTime > 0f && mathMachine.answered)
		{
			poppingTime -= Time.deltaTime;
            currentlyHeld = false;
		}
        if (poppingTime <= 0f && mathMachine.answered && GetComponent<SpriteRenderer>().enabled)
        {
            mathMachine.audioDevice.PlayOneShot(popSound);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            playerScript.heldBalloon = -1f;
            currentlyHeld = false;
		    UIPopupTextManagerWithMovement.Show("Misc_Pop", Color.white, transform, 0.5f, 0f);
        }
        if (mathMachine.answered && balloonNumber == mathMachine.qAnswer && GetComponent<SpriteRenderer>().enabled)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            playerScript.heldBalloon = -1f;
            currentlyHeld = false;
        }
        if (gameController.escapeSequence && !bonus && !GetComponent<SpriteRenderer>().enabled)
        {
            bonus = true;
            poppingTime = UnityEngine.Random.Range(0.5f, 3f);
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
            transform.position = origin;
        }
        if (playerScript.heldBalloon != balloonNumber && currentlyHeld) currentlyHeld = false;
    }
    private void OnTriggerExit(Collider other)
	{
        if (other.transform.name == "BalloonBoundary")
        {
            if (currentlyHeld)
            {
                playerScript.heldBalloon = -1f;
                currentlyHeld = false;
            }
            transform.position = origin;
        }
    }
    public float balloonNumber;
    public float poppingTime;
    public bool interact;
    public bool currentlyHeld;
    public bool bonus = false;
    public Transform player;
    public PlayerScript playerScript;
	public GameController gameController;
    public MathMachine mathMachine;
    public AudioClip popSound;
	public Vector3 origin;
}
