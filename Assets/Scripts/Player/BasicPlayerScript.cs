using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BasicPlayerScript : MonoBehaviour
{
	// This is basically a template, used for new modes and cutscene/ending based areas.
	// If you see any empty or generally short functions, it is because other scripts rely on them.
	// They are only kept to make sure the game will function properly in the rare occasion they are needed.
	private void Awake()
    {
		if (PlayerPrefs.GetFloat("MouseSensitivity") != 0)
		{
			mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
		}
		else
		{
			mouseSensitivity = 1f;
		}
		frozen = false;
    }
	private void Start()
    {
		stamina = staminaMax;
		playerRotation = transform.rotation;
        height = transform.position.y;
    }
	private void Update()
	{
		transform.position = new Vector3(transform.position.x, height, transform.position.z);
		if (!frozen)
		{
			if (!gameController.dangerouslyTeleporting)
			{
				Movement();
			}
			Mouse();
			Stamina();
		}
		if (charControl.velocity.magnitude > 0f)
		{
			gameController.LockMouse();
		}
		if (bootsTimer > 0f)
		{
			bootsTimer -= Time.deltaTime;
		}
		else
		{
			bootsActive = false;
		}
		if (invisTime > 0f)
		{
			invisTime -= Time.deltaTime;
			gameObject.layer = 7;
		}
		else
		{
			gameObject.layer = 3;
		}
	}
	private void Movement()
	{
		Vector3 movement = Vector3.zero;
		Vector3 sideMovement = Vector3.zero;
		running = Input.GetButton("Run");
		if (stamina > 0f & running & !frozen)
		{
			playerSpeed = runSpeed;
		}
		else if (!frozen)
		{
			playerSpeed = walkSpeed;
		}
		if (Input.GetAxis("Up/Down") == 1) movement = transform.forward;
		if (Input.GetAxis("Up/Down") == -1) movement = -transform.forward;
		if (Input.GetAxis("Left/Right") == 1) sideMovement = transform.right;
		if (Input.GetAxis("Left/Right") == -1) sideMovement = -transform.right;
		playerSpeed *= Time.deltaTime;
		moveDirection = (movement + sideMovement).normalized * playerSpeed;
		charControl.Move(moveDirection);
	}
	private void Mouse()
    {
		playerRotation.eulerAngles = new Vector3(playerRotation.eulerAngles.x, playerRotation.eulerAngles.y, 0f);
		playerRotation.eulerAngles = playerRotation.eulerAngles + Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivity * Time.timeScale;
		transform.rotation = playerRotation;
		Cursor.lockState = CursorLockMode.Locked;
    }
    private void Stamina()
    {
        if (charControl.velocity.magnitude > charControl.minMoveDistance)
		{
			if (running)
			{
				stamina = Mathf.Max(stamina - staminaDecreaseSpd * Time.deltaTime, 0f);
			}
		}
		else if (stamina < staminaMax)
		{
			stamina += staminaIncreaseSpd * Time.deltaTime;
		}
		staminaRound = (Mathf.FloorToInt(stamina));
		StaminaText.text = staminaRound.ToString() + "%";
		staminaBar.value = staminaRound / staminaMax * 100f;
    }
	private void OnTriggerEnter(Collider other)
	{
		// Empty since it was used for character-based interactions.
	}
	private void OnTriggerStay(Collider other)
	{
		// Same here.
	}
	private void OnTriggerExit(Collider other)
	{
		// Same here.
	}
	public void ActivateBoots()
	{
		bootsActive = true;
		bootsTimer = 60f;
	}
    public CharacterController charControl;
	public BasicCameraScript cameraScript;
	public float playerSpeed;
	public float walkSpeed;
	public float runSpeed;
	public float stamina;
	public bool bootsActive;
	public bool frozen;
	public float staminaDecreaseSpd;
	public float staminaIncreaseSpd;
	public float staminaMax;
	public float mouseSensitivity;
	public float staminaRound;
	public float height;
	public Slider staminaBar;
	public Quaternion playerRotation;
	public TMP_Text StaminaText;
	public BasicGameController gameController;
	private Vector3 moveDirection;
	private bool running;
	private float bootsTimer;
	public float invisTime;
}