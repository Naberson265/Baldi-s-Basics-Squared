using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    void Start()
    {
		killedPlayer = false;
		dangerouslyTeleporting = false;
        schoolTheme.Play();
		LockMouse();
		gameMode = PlayerPrefs.GetString("GameMode");
		mapMode = PlayerPrefs.GetString("MapMode");
		spoopMode = false;
		quarterGiven = false;
		midGameYTPs = 0;
		if (gameMode == "endless")
		{
			maxNotebooks = 999999;
			spoopBaldiScript.endless = true;
			mapTimerEnabled = false;
		}
		if (modifiersEnabled) ModifierCheck();
		StartCoroutine(LateStart());
	}
	IEnumerator LateStart()
	{
		yield return null;
		player = FindFirstObjectByType<PlayerController>();
	}
	public void ModifierCheck()
	{
		if (PlayerPrefs.GetInt("TimeOutModifier") == 1) mapTimer = 60f;
	}
    void Update()
    {
		if (!mathPadActive)
		{
			if (Input.GetButtonDown("Pause") == true)
			{
				if (!gamePaused) PauseGame();
				else UnpauseGame();
			}
			if (!gamePaused && !finalExitReached && Time.timeScale != 1f && !killedPlayer) Time.timeScale = 1f;
			if (finalExitReached) Time.timeScale = 0f;
		}
		else if (Time.timeScale != 0f) Time.timeScale = 0f;
		if (gameMode == "classic" || gameMode == "challenge") notebookCount.text = notebooks.ToString() + "/" + maxNotebooks.ToString();
		else if (gameMode == "endless") notebookCount.text = notebooks.ToString() + " Notebooks";
		else if (gameMode == "campingABase" || gameMode == "campingAEndless") {}
		else notebookCount.text = notebooks.ToString() + "/0";
		if (finalExitReached || gamePaused || mathPadActive || player.frozen) UnlockMouse();
        int timeMinutes;
        int timeSeconds;
		string formattedTime;
		transform.position = playerTransform.position - playerTransform.forward;
		if (mapTimer > 0f && mapTimerEnabled && spoopMode)
		{
			timeIcon.SetActive(true);
			timeText.gameObject.SetActive(true);
			mapTimer -= 1f * Time.deltaTime;
        	if (mapTimer > 0f) timeMinutes = Mathf.FloorToInt(mapTimer / 60);
			else timeMinutes = 0;
        	if (mapTimer > 0f) timeSeconds = Mathf.FloorToInt(mapTimer % 60);
			else timeSeconds = 0;
			formattedTime = string.Format("{0:00}:{1:00}", timeMinutes, timeSeconds);
			timeText.text = formattedTime.ToString();
			if (mapTimer < 60f) timeText.color = new Color (255f, 0f, 0f);
			else timeText.color = new Color (0f, 0f, 0f);
		}
		else if (!mapTimerEnabled || !spoopMode)
		{
			timeIcon.SetActive(false);
			timeText.gameObject.SetActive(false);
		}
		if (mapTimer <= 0f && mapTimerEnabled && spoopMode )
		{
			if (!timeOut) StartTimeOut();
			player.ResetGuilt("afterhours", 99f);
		}
    }
	public void StartTimeOut()
	{
		timeOut = true;
		gameAudioDevice.clip = timeOutTheme;
		RenderSettings.skybox = escapeSequenceSky;
		gameAudioDevice.Play();
		gameAudioDevice.PlayOneShot(timeOutVoice);
		spoopBaldiScript.timeOut = true;
		UIPopupTextManagerWithMovement.Show("BALTV_TimeOut1", Color.green, transform, 1f, 0f);
		UIPopupTextManagerWithMovement.Show("BALTV_TimeOut2", Color.green, transform, 1f, 0f);
	}
	public void DespawnCrafters()
	{
		artsAndCrafters.SetActive(false);
	}
	private IEnumerator DangerousTeleporter()
	{
		dangerouslyTeleporting = true;
		playerCharacter.enabled = false;
		playerCollider.enabled = false;
		int teleports = Random.Range(12, 16);
		int teleportCount = 0;
		float baseTime = 0.2f;
		float currentTime = baseTime;
		float increaseFactor = 1.1f;
		while (teleportCount < teleports)
		{
			currentTime -= Time.deltaTime;
			if (currentTime < 0f)
			{
				Teleport();
				teleportCount++;
				baseTime *= increaseFactor;
				currentTime = baseTime;
			}
			else
			{
				player.height = 4f;
			}
			yield return null;
		}
		dangerouslyTeleporting = false;
		playerCharacter.enabled = true;
		playerCollider.enabled = true;
		yield break;
	}
	private void Teleport()
	{
		wanderSelector.GetNewTarget();
		player.transform.position = wanderSelector.transform.position + Vector3.up * player.height;
		gameAudioDevice.PlayOneShot(teleportSFX);
	}
	public void BeginSpoopMode()
	{
		spoopMode = true;
		tutorBaldi.SetActive(false);
		foreach (GameObject character in spoopCharacters) if (character != null) character.SetActive(true);
		schoolTheme.Stop();
		if (lightManager.sets <= 0) lightManager.SetLighting();
	}
	public void LockMouse()
	{
		cursorController.LockCursor();
		mouseLocked = true;
		reticle.SetActive(true);
	}
	public void UnlockMouse()
	{
		cursorController.UnlockCursor();
		mouseLocked = false;
		reticle.SetActive(false);
	}
	public void SchoolhouseEscapeStart()
	{
		schoolTheme.Stop();
		closedExits = 0;
		escapeSequence = true;
		SchoolhouseEscapeUpdate();
		gameAudioDevice.Play();
		if (maxNotebooks == 4) UIPopupTextManagerWithMovement.Show("BALTV_All4Books", Color.green, transform, 5.5f, 0f);
		else if (maxNotebooks == 7) UIPopupTextManagerWithMovement.Show("BALTV_All7Books", Color.green, transform, 5.5f, 0f);
		else if (maxNotebooks == 8) UIPopupTextManagerWithMovement.Show("BALTV_All8Books", Color.green, transform, 4.8f, 1f);
		else UIPopupTextManagerWithMovement.Show("BALTV_All9Books", Color.green, transform, 5.5f, 0f);
		UIPopupTextManagerWithMovement.Show("BALTV_NeedToDo", Color.green, transform, 2f, 0f);
		if (mapMode == "fgpd" || mapMode == "trueclassic" || maxNotebooks == 8)
		{
			UIPopupTextManagerWithMovement.Show("BALTV_GetOut", Color.green, transform, 3f, 0f);
			UIPopupTextManagerWithMovement.Show("Misc_Wait", Color.green, transform, 2.5f, 0f);
		}
		else
		{
			UIPopupTextManagerWithMovement.Show("BALTV_FindWayOut", Color.green, transform, 3f, 0f);
			UIPopupTextManagerWithMovement.Show("Misc_Wait", Color.green, transform, 3.5f, 0f);
		}
		UIPopupTextManagerWithMovement.Show("BALTV_EscapeLaugh", Color.green, transform, 1.5f, 0f);
	}
	public void SchoolhouseEscapeUpdate()
	{
		if (closedExits == 0)
		{
			if (!timeOut) gameAudioDevice.clip = escapeTheme0;
			if (changeSkyDuringEscape)
			{
				if (timeOut) RenderSettings.ambientLight = new Color(0.6f,0.5f,0.5f);
				else RenderSettings.ambientLight = new Color(1f,0.9f,0.9f);
				if (timeOut) RenderSettings.fogColor = new Color(0.6f,0.5f,0.5f);
				else RenderSettings.fogColor = new Color(1f,0.9f,0.9f);
			}
			if (changeSkyDuringEscape) RenderSettings.skybox = escapeSequenceSky;
			gameAudioDevice.Play();
			gameAudioDevice.PlayOneShot(allNotebookVoice);
		}
		else if (closedExits == 1)
		{
			if (separatedEscapeThemes && !timeOut)
			{
				gameAudioDevice.clip = escapeTheme1;
				gameAudioDevice.Play();
			}
			if (changeSkyDuringEscape) 
			{
				if (timeOut) RenderSettings.ambientLight = new Color(0.6f,0.3f,0.3f);
				else RenderSettings.ambientLight = new Color(1f,0.6f,0.6f);
				if (timeOut) RenderSettings.fogColor = new Color(0.6f,0.3f,0.3f);
				else RenderSettings.fogColor = new Color(1f,0.6f,0.6f);
			}
		}
		else if (closedExits == 2)
		{
			if (separatedEscapeThemes && !timeOut)
			{
				gameAudioDevice.clip = escapeTheme2;
				gameAudioDevice.Play();
			}
			if (changeSkyDuringEscape) 
			{
				if (timeOut) RenderSettings.ambientLight = new Color(0.6f,0.2f,0.2f);
				else RenderSettings.ambientLight = new Color(1f,0.3f,0.3f);
				if (timeOut) RenderSettings.fogColor = new Color(0.6f,0.2f,0.2f);
				else RenderSettings.fogColor = new Color(1f,0.3f,0.3f);
			}
		}
		else if (closedExits == 3)
		{
			if (separatedEscapeThemes && !timeOut)
			{
				gameAudioDevice.clip = escapeTheme3;
				gameAudioDevice.Play();
			}
			if (changeSkyDuringEscape) 
			{
				if (timeOut) RenderSettings.ambientLight = new Color(0.6f,0.1f,0.1f);
				else RenderSettings.ambientLight = new Color(1f,0.1f,0.1f);
				if (timeOut) RenderSettings.fogColor = new Color(0.6f,0.1f,0.1f);
				else RenderSettings.fogColor = new Color(1f,0.1f,0.1f);
			}
		}
	}
	public void NotebookCollect()
	{
		notebooks = notebooks + 1;
	}
	public void ExitClose()
	{
		gameAudioDevice.PlayOneShot(exitCloseSound);
		closedExits = closedExits + 1;
		SchoolhouseEscapeUpdate();
	}
	public void FinalExit()
	{
		closedExits = 4;
		Time.timeScale = 0f;
		gameAudioDevice.Stop();
		foreach (GameObject character in spoopCharacters) if (character != null) character.SetActive(false);
		RenderSettings.ambientLight = new Color(0.8f,0.8f,0.8f);
		finalExitReached = true;
		winScreen.SetActive(true);
		gameAudioDevice.clip = winSFX;
		gameAudioDevice.Play();
		gameAudioDevice.loop = false;
        PlayerPrefs.SetInt("YTPs", PlayerPrefs.GetInt("YTPs") + 50);
	}
	public void PauseGame()
	{
		if (!mathPadActive)
		{
			UnlockMouse();
		}
		Time.timeScale = 0f;
		gamePaused = true;
		pauseMenu.SetActive(true);
	}
	public void UnpauseGame()
	{
		Time.timeScale = 1f;
		gamePaused = false;
		pauseMenu.SetActive(false);
		quitMenu.SetActive(false);
		LockMouse();
	}
	public void GetAngry(float value)
	{
		if (!spoopMode)
		{
			BeginSpoopMode();
		}
		spoopBaldiScript.GetAngry(value);
	}
	public void YTPGain(int ytpAddingAmount)
	{
		midGameYTPs = midGameYTPs + ytpAddingAmount;
        if (ytpAddingAmount > 0) PlayerPrefs.SetInt("YTPs", PlayerPrefs.GetInt("YTPs") + ytpAddingAmount);
		gameAudioDevice.PlayOneShot(ytpFoundSound);
		StartCoroutine(YTPCounterAnim());
	}
	public IEnumerator YTPCounterAnim()
	{
		float animTime = 3f;
		while (animTime > 0f)
		{
			animTime -= Time.deltaTime;
			ytpDisplay.color = new Color (255f, 255f, 255f, 255f);
			ytpText.color = new Color (255f, 0f, 0f, 255f);
			ytpText.text = midGameYTPs.ToString();
			yield return null;
		}
		ytpDisplay.color = new Color (255f, 255f, 255f, 0f);
		ytpText.color = new Color (255f, 0f, 0f, 0f);
		yield break;
	}
	public void ActivateLearningGame()
	{
		mathPadActive = true;
		UnlockMouse();
		tutorBaldiVoice.Stop();
		if (!spoopMode)
		{
			schoolTheme.Stop();
			YCTPTheme.clip = YCTP0Theme;
			YCTPTheme.Play();
		}
		if (!timeOut && !escapeSequence) gameAudioDevice.Stop();
	}
	public void DeactivateLearningGame(GameObject mathPad)
	{
		mathPadActive = false;
		LockMouse();
		Destroy(mathPad);
		if (player.stamina < 100f)
		{
			player.stamina = 100f;
		}
		if (!spoopMode)
		{
			schoolTheme.Play();
			YCTPTheme.Stop();
		}
		if (notebooks == 1 && !spoopMode && !quarterGiven)
		{
			baldisQuarter.SetActive(true);
			tutorBaldiVoice.clip = baldiPrizeSFX;
			tutorBaldiVoice.Play();
			quarterGiven = true;
		}
		if (notebooks == maxNotebooks && !escapeSequence)
		{
			SchoolhouseEscapeStart();
			escapeSequence = true;
		}
		if (!timeOut && !escapeSequence) gameAudioDevice.Play();
	}

	public string[] itemNames = new string[]
	{
		"Nothing",
		"BSODA",
		"Energy Flavored Zesty Bar",
		"Swinging Door Lock",
		"Quarter",
		"Baldi's Least Favorite Tape",
		"Principal's Keys",
		"Safety Scissors",
		"WD-NoSquee",
		"Alarm Clock",
		"Techno Boots",
		"Dangerous Teleporter",
		"(5) Grappling Hook",
		"An Apple for Baldi",
		"Dirty Chalk Eraser",
		"Principal Whistle",
		"Faculty Nametag",
		"Portal Poster",
		"'Nana Peel",
		"Diet Bsoda",
		"Bus Pass",
		"Invisibility Elixir",
		"Reflex Hammer",
		"Plunger",
		"Bomb",
		"Firewood",
		"Coal",
		"Gasoline",
		"Circle Key",
		"Triangle Key",
		"Square Key",
		"Star Key",
		"Heart Key",
		"Weird Key",
		"(4) Grappling Hook",
		"(3) Grappling Hook",
		"(2) Grappling Hook",
		"(1) Grappling Hook",
		"Super Stretchy Glove",
	};
	public Texture[] itemSlotTextures = new Texture[39];
	public float mapTimer;
	public bool mapTimerEnabled;
	public bool timeOut;
	public bool modifiersEnabled;
    public AudioSource schoolTheme;
	public bool changeSkyDuringEscape;
	public bool spoopMode;
	public int notebooks;
	public int maxNotebooks;
	public int closedExits;
	public int midGameYTPs;
	public bool separatedEscapeThemes;
	public bool twoNotebooksRequired;
	public AudioSource YCTPTheme;
	public AudioClip exitCloseSound;
	public AudioClip escapeTheme0;
	public AudioClip escapeTheme1;
	public AudioClip escapeTheme2;
	public AudioClip escapeTheme3;
	public AudioClip timeOutTheme;
	public AudioClip winSFX;
	public AudioClip teleportSFX;
	public AudioClip sodaSFX;
	public AudioClip elixirSFX;
	public AudioClip zBarSFX;
	public AudioClip chalkSFX;
	public AudioClip whistleSFX;
	public AudioClip ytpFoundSound;
	public AudioSource gameAudioDevice;
	public TMP_Text notebookCount;
	public TMP_Text timeText;
	public GameObject timeIcon;
	public AudioClip YCTP0Theme;
	public AudioClip YCTP1Theme;
	public AudioClip YCTP2Theme;
	public AudioClip baldiPrizeSFX;
	public AudioClip allNotebookVoice;
	public AudioClip timeOutVoice;
    public CursorControllerScript cursorController;
	public SpoopBaldi spoopBaldiScript;
	public Principal principalScript;
	public PlayerController player;
	public bool mouseLocked;
	public bool escapeSequence;
	public bool finalExitReached;
	public bool mathPadActive;
    public Image ytpDisplay;
    public TMP_Text ytpText;
    public GameObject reticle;
	public GameObject tutorBaldi;
	public AudioSource tutorBaldiVoice;
	public GameObject baldi;
	public GameObject artsAndCrafters;
	public GameObject playtime;
	public GameObject[] spoopCharacters;
	public GameObject baldisQuarter;
	public GameObject bsodaSpray;
	public GameObject dietBsodaSpray;
	public GameObject alarmClock;
	public GameObject chalkDust;
	public int failedNotebooks;
	public bool quarterGiven;
	public bool dangerouslyTeleporting;
	public GameObject winScreen;
	public Transform playerTransform;
	public CharacterController playerCharacter;
	public Collider playerCollider;
	public Transform cameraTransform;
	public Camera playerCamera;
	public string mapMode;
	public string gameMode;
	public bool killedPlayer;
	public Material escapeSequenceSky;
	public AIWanderPointSelector wanderSelector;
	private bool gamePaused;
	public GameObject pauseMenu;
	public GameObject quitMenu;
	public LightingManager lightManager;
}