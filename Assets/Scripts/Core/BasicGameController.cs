using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BasicGameController : MonoBehaviour
{
	// This is basically a template, used for new modes and cutscene/ending based areas.
	// If you see any empty or generally short functions, it is because other scripts rely on them.
	// They are only kept to make sure the game will function properly in the rare occasion they are needed.
	// Need to make sure an inactive SpoopBaldi exists so most things function without needing a new script.
	void Awake()
	{
		int[] array = new int[3];
		array[0] = 270;
		array[1] = 321;
		array[2] = 372;
		itemSelectOffset = array;
	}
    void Start()
	{
		killedPlayer = false;
		dangerouslyTeleporting = false;
        schoolTheme.Play();
		LockMouse();
		gameMode = PlayerPrefs.GetString("GameMode");
		itemSelected = 0;
		midGameYTPs = 0;
		if (bonusItemsAllowed) BonusItems();
    }
    void Update()
    {
		if (!mathPadActive)
		{
			if (Input.GetButtonDown("Pause") == true)
				{
					if (!gamePaused)
					{
						PauseGame();
					}
					else
           		 	{
						UnpauseGame();
         	 	    }
				}
			if (!gamePaused && Time.timeScale != 1f && !escapeExitReached && !killedPlayer)
			{
				Time.timeScale = 1f;
			}
			if (escapeExitReached)
			{
				Time.timeScale = 0f;
			}
			if (Input.GetAxis("Mouse ScrollWheel") > 0f && Time.timeScale != 0f)
			{
				DecreaseItemSelection();
			}
			else if (Input.GetAxis("Mouse ScrollWheel") < 0f && Time.timeScale != 0f)
			{
				IncreaseItemSelection();
			}
			if (Input.GetButtonDown("Item") == true && Time.timeScale != 0f)
			{
				UseItem();
			}
			if (Input.GetButtonDown("Select Item 0"))
			{
				itemSelected = 0;
				itemSelectBox.anchoredPosition = new Vector3((float)itemSelectOffset[itemSelected], 197f, 0f);
				itemNameText.text = itemNames[item[itemSelected]];
			}
			if (Input.GetButtonDown("Select Item 1"))
			{
				itemSelected = 1;
				itemSelectBox.anchoredPosition = new Vector3((float)itemSelectOffset[itemSelected], 197f, 0f);
				itemNameText.text = itemNames[item[itemSelected]];
			}
			if (Input.GetButtonDown("Select Item 2"))
			{
				itemSelected = 2;
				itemSelectBox.anchoredPosition = new Vector3((float)itemSelectOffset[itemSelected], 197f, 0f);
				itemNameText.text = itemNames[item[itemSelected]];
			}
		}
		else
		{
			if (Time.timeScale != 0f)
			{
				Time.timeScale = 0f;
			}
		}
		if (gameMode == "classic" || gameMode == "challenge")
       	{
			notebookCount.text = notebooks.ToString() + "/" + maxNotebooks.ToString();
		}
		else if (gameMode == "endless")
		{
			notebookCount.text = notebooks.ToString() + " Notebooks";
		}
		else if (gameMode == "campingABase" || gameMode == "campingAEndless" || gameMode == "farmABase")
		{
		}
		else
		{
			notebookCount.text = notebooks.ToString() + "/0";
		}
		if (escapeExitReached || gamePaused || mathPadActive || player.frozen)
		{
			UnlockMouse();
		}
    }
	public void BonusItems()
	{
		int slot1 = PlayerPrefs.GetInt("BonusItemSlot1Enabled");
		int slot2 = PlayerPrefs.GetInt("BonusItemSlot2Enabled");
		int slot3 = PlayerPrefs.GetInt("BonusItemSlot3Enabled");
		if (slot1 != 0)
		{
			CollectItem(PlayerPrefs.GetInt("BonusItemSlot" + slot1.ToString()));
			PlayerPrefs.SetInt("BonusItemSlot" + slot1.ToString(), 0);
		}
		if (slot2 != 0)
		{
			CollectItem(PlayerPrefs.GetInt("BonusItemSlot" + slot2.ToString()));
			PlayerPrefs.SetInt("BonusItemSlot" + slot2.ToString(), 0);
		}
		if (slot3 != 0)
		{
			CollectItem(PlayerPrefs.GetInt("BonusItemSlot" + slot3.ToString()));
			PlayerPrefs.SetInt("BonusItemSlot" + slot3.ToString(), 0);
		}
	}
	private void IncreaseItemSelection()
	{
		itemSelected++;
		if (itemSelected > 2)
		{
			itemSelected = 0;
		}
		itemSelectBox.anchoredPosition = new Vector3((float)itemSelectOffset[itemSelected], 197f, 0f);
		itemNameText.text = itemNames[item[itemSelected]];
	}
	private void DecreaseItemSelection()
	{
		itemSelected--;
		if (itemSelected < 0)
		{
			itemSelected = 2;
		}
		itemSelectBox.anchoredPosition = new Vector3((float)itemSelectOffset[itemSelected], 197f, 0f);
		itemNameText.text = itemNames[item[itemSelected]];
	}
	private void UpdateItemSelection()
	{
		itemSelectBox.anchoredPosition = new Vector3((float)itemSelectOffset[itemSelected], 197f, 0f);
		itemNameText.text = itemNames[item[itemSelected]];
	}
	public void CollectItem(int item_ID)
	{
		if (item[itemSelected] == 0)
		{
			item[itemSelected] = item_ID;
			itemSlot[itemSelected].texture = itemSlotTextures[item_ID];
		}
		else if (item[0] == 0)
		{
			item[0] = item_ID;
			itemSlot[0].texture = itemSlotTextures[item_ID];
		}
		else if (item[1] == 0)
		{
			item[1] = item_ID;
            itemSlot[1].texture = itemSlotTextures[item_ID];
        }
		else if (item[2] == 0)
		{
			item[2] = item_ID;
            itemSlot[2].texture = itemSlotTextures[item_ID];
        }
		else
		{
			item[itemSelected] = item_ID;
			itemSlot[itemSelected].texture = itemSlotTextures[item_ID];
		}
		itemNameText.text = itemNames[item[itemSelected]];
	}
	public void UseItem()
	{
		if (item[itemSelected] != 0)
		{
			if (item[itemSelected] == 1)
			{
				Instantiate<GameObject>(bsodaSpray, playerTransform.position, cameraTransform.rotation);
				DeleteItem();
				gameAudioDevice.PlayOneShot(sodaSFX);
			}
			else if (item[itemSelected] == 2)
			{
				player.stamina = player.staminaMax * 2f;
				DeleteItem();
			}
			else if (item[itemSelected] == 3)
			{
				Ray ray = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider.tag == "SwingingDoor" & Vector3.Distance(playerTransform.position, raycastHit.transform.position) <= 10f))
				{
					raycastHit.collider.gameObject.GetComponent<SwingingDoorScript>().LockDoor(30f);
					DeleteItem();
				}
			}
			else if (item[itemSelected] == 4)
			{
				Ray ray3 = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
				RaycastHit raycastHit3;
				if (Physics.Raycast(ray3, out raycastHit3))
				{
					if ((raycastHit3.collider.name == "BSODAVending") & Vector3.Distance(playerTransform.position, raycastHit3.transform.position) <= 10f)
					{
						if (!raycastHit3.collider.gameObject.GetComponent<VendingScript>().used)
						{
							raycastHit3.collider.gameObject.GetComponent<VendingScript>().Dispense();
							DeleteItem();
							CollectItem(1);
						}
					}
					else if ((raycastHit3.collider.name == "DietBSODAVending") & Vector3.Distance(playerTransform.position, raycastHit3.transform.position) <= 10f)
					{
						if (!raycastHit3.collider.gameObject.GetComponent<VendingScript>().used)
						{
							raycastHit3.collider.gameObject.GetComponent<VendingScript>().Dispense();
							DeleteItem();
							CollectItem(19);
						}
					}
					else if ((raycastHit3.collider.name == "ZestyBarVending") & Vector3.Distance(playerTransform.position, raycastHit3.transform.position) <= 10f)
					{
						if (!raycastHit3.collider.gameObject.GetComponent<VendingScript>().used)
						{
							raycastHit3.collider.gameObject.GetComponent<VendingScript>().Dispense();
							DeleteItem();
							CollectItem(2);
						}
					}
					else if ((raycastHit3.collider.name == "TeleporterVending") & Vector3.Distance(playerTransform.position, raycastHit3.transform.position) <= 10f)
					{
						if (!raycastHit3.collider.gameObject.GetComponent<VendingScript>().used)
						{
							raycastHit3.collider.gameObject.GetComponent<VendingScript>().Dispense();
							DeleteItem();
							CollectItem(11);
						}
					}
					else if (raycastHit3.collider.name == "Payphone" & Vector3.Distance(playerTransform.position, raycastHit3.transform.position) <= 10f)
					{
						raycastHit3.collider.gameObject.GetComponent<AntiHearing>().Play();
						DeleteItem();
					}
				}
			}
			else if (item[itemSelected] == 5)
			{
				Ray ray3 = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
				RaycastHit raycastHit3;
				if (Physics.Raycast(ray3, out raycastHit3))
				{
					if (raycastHit3.collider.name == "AntiHearingTape" & Vector3.Distance(playerTransform.position, raycastHit3.transform.position) <= 10f)
					{
						raycastHit3.collider.gameObject.GetComponent<AntiHearing>().Play();
						DeleteItem();
					}
				}
			}
			else if (item[itemSelected] == 6)
			{
				Ray ray2 = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
				RaycastHit raycastHit2;
				if (Physics.Raycast(ray2, out raycastHit2) && (raycastHit2.collider.tag == "Door" & Vector3.Distance(playerTransform.position, raycastHit2.transform.position) <= 10f))
				{
					StandardDoorScript component = raycastHit2.collider.gameObject.GetComponent<StandardDoorScript>();
					if (component.DoorLocked)
					{
						component.UnlockDoor();
						component.OpenDoor(true);
						DeleteItem();
					}
				}
			}
			else if (item[itemSelected] == 7)
			{
				// Only can be used on characters, thus this section is empty.
			}
			else if (item[itemSelected] == 8)
			{
				Ray ray = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider.tag == "Door" & Vector3.Distance(playerTransform.position, raycastHit.transform.position) <= 10f))
				{
					raycastHit.collider.gameObject.GetComponent<StandardDoorScript>().SilenceDoor();
					DeleteItem();
				}
			}
			else if (item[itemSelected] == 9)
			{
				// Only can be used if Baldi is in the map, thus this section is empty.
			}
			else if (item[itemSelected] == 10)
			{
				player.ActivateBoots();
				DeleteItem();
			}
			else if (item[itemSelected] == 11)
			{
				StartCoroutine(DangerousTeleporter());
				DeleteItem();
			}
			else if (item[itemSelected] == 15)
			{
				
			}
			else if (item[itemSelected] == 19)
			{
				Instantiate<GameObject>(dietBsodaSpray, playerTransform.position, cameraTransform.rotation);
				DeleteItem();
				gameAudioDevice.PlayOneShot(sodaSFX);
			}
			else if (item[itemSelected] == 21)
			{
				player.invisTime = 30f;
				gameAudioDevice.PlayOneShot(elixirSFX);
				DeleteItem();
			}
			else if (item[itemSelected] == 25)
			{
				Ray ray = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider.name == "Campfire" & Vector3.Distance(playerTransform.position, raycastHit.transform.position) <= 10f))
				{
					raycastHit.collider.gameObject.GetComponent<CampFireScript>().FuelFire(10f);
					DeleteItem();
				}
			}
			else if (item[itemSelected] == 26)
			{
				Ray ray = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider.name == "Campfire" & Vector3.Distance(playerTransform.position, raycastHit.transform.position) <= 10f))
				{
					raycastHit.collider.gameObject.GetComponent<CampFireScript>().FuelFire(15f);
					DeleteItem();
				}
			}
			else if (item[itemSelected] == 27)
			{
				Ray ray = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider.name == "Campfire" & Vector3.Distance(playerTransform.position, raycastHit.transform.position) <= 10f))
				{
					raycastHit.collider.gameObject.GetComponent<CampFireScript>().FuelFire(20f);
					DeleteItem();
				}
			}
			// Template for new items.
			else if (item[itemSelected] == 38)
			{
				
			}
		}
	}
	public void DespawnCrafters()
	{
		// Empty since it relies on Arts & Crafters.
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
	private void DeleteItem()
	{
		item[itemSelected] = 0;
		itemSlot[itemSelected].texture = itemSlotTextures[0];
		itemNameText.text = itemNames[item[itemSelected]];
	}
	public void DeleteSpecificItem(int id)
	{
		item[id] = 0;
		itemSlot[id].texture = itemSlotTextures[0];
		itemNameText.text = itemNames[item[itemSelected]];
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
	public void NotebookCollect()
	{
		notebooks = notebooks + 1;
		player.stamina = 100;
	}
	public void FinalExit()
	{
		escapeExitReached = true;
		Time.timeScale = 0f;
		gameAudioDevice.Stop();
		RenderSettings.ambientLight = new Color(0.8f,0.8f,0.8f);
		winScreen.SetActive(true);
		gameAudioDevice.clip = winSFX;
		gameAudioDevice.Play();
		gameAudioDevice.loop = false;
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
		// Empty because it relies on SpoopBaldi and SpoopMode.
	}
	public void ActivateLearningGame()
	{
		mathPadActive = true;
		UnlockMouse();
		schoolTheme.Stop();
		YCTPTheme.clip = YCTP0Theme;
		YCTPTheme.Play();
		gameAudioDevice.Stop();
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
		schoolTheme.Play();
		YCTPTheme.Stop();
		if (notebooks == maxNotebooks && !escapeSequence && gameMode == "classic")
		{
			escapeSequence = true;
		}
		gameAudioDevice.Play();
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
	public bool bonusItemsAllowed;
	public TMP_Text itemNameText;
    public AudioSource schoolTheme;
	public int notebooks;
	public int maxNotebooks;
	public int midGameYTPs;
	public AudioSource YCTPTheme;
	public AudioClip winSFX;
	public AudioClip teleportSFX;
	public AudioClip sodaSFX;
	public AudioClip elixirSFX;
	public AudioSource gameAudioDevice;
	public TMP_Text notebookCount;
	public AudioClip YCTP0Theme;
	public AudioClip YCTP1Theme;
	public AudioClip YCTP2Theme;
	public AudioClip ytpFoundSound;
	public AudioClip allNotebookVoice;
    public CursorControllerScript cursorController;
	public BasicPlayerScript player;
	public bool mouseLocked;
	public bool escapeSequence;
	public bool escapeExitReached;
	public bool mathPadActive;
    public GameObject reticle;
	public GameObject bsodaSpray;
	public GameObject dietBsodaSpray;
	public GameObject alarmClock;
	public int failedNotebooks;
	public bool dangerouslyTeleporting;
	public GameObject winScreen;
	public Transform playerTransform;
	public CharacterController playerCharacter;
	public Collider playerCollider;
	public Transform cameraTransform;
	public Camera playerCamera;
	public int itemSelected;
	public RectTransform itemSelectBox;
	public RawImage[] itemSlot = new RawImage[3];
	public int[] item = new int[3];
	private int[] itemSelectOffset;
	public string gameMode;
	public bool killedPlayer;
	public AIWanderPointSelector wanderSelector;
	private bool gamePaused;
	public GameObject pauseMenu;
	public GameObject quitMenu;
    public Image ytpDisplay;
    public TMP_Text ytpText;
}