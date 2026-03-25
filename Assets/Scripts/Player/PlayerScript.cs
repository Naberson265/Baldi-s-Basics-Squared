using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour
{
	private void Awake()
    {
		heldBalloon = -1f;
		if (PlayerPrefs.GetFloat("MouseSensitivity") != 0) mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
		else mouseSensitivity = 1f;
		frozen = false;
		invisTime = 0f;
		infReachTime = 0f;
		int[] array = new int[3];
		array[0] = 270;
		array[1] = 321;
		array[2] = 372;
		itemSelectOffset = array;
    }
	private void Start()
    {
		stamina = staminaMax;
		playerRotation = transform.rotation;
		principalBugFixer = 1;
        height = transform.position.y;
		reachDist = baseReachDist;
		if (bonusItemsAllowed) BonusItems();
		if (PlayerPrefs.GetInt("StaminaRestriction") == 1) 
		{
			runSpeed = walkSpeed;
			staminaIncreaseSpd = 0;
			staminaDecreaseSpd = 1000;
			stamina = 0;
			staminaMax = 1;
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
	private void Update()
	{
		transform.position = new Vector3(transform.position.x, height, transform.position.z);
        interactDown = Input.GetButtonDown("Interact");
		if (ventTarget != new Vector3(0f, 0f, 0f))
		{
			inVent = true;
			transform.position = Vector3.MoveTowards(transform.position, ventTarget, ventSpeed * Time.deltaTime);
		}
		else inVent = false;
		if (!frozen)
		{
			if (!dangerouslyTeleporting && !inVent)
			{
				Movement();
				Interactions();
			}
			Mouse();
			Stamina();
			GuiltCheck();
		}
		if (charControl.velocity.magnitude > 0f) gameController.LockMouse();
		if ((jumpRope & (transform.position - playtimeObj.transform.position).magnitude >= 60f) || (jumpRope & hugging) || (jumpRope & sweeping))
		{
			DeactivateJumpRope();
			playtime.Disappoint();
		}
		if (sweepingFailsave > 0f) sweepingFailsave -= Time.deltaTime;
		else
		{
			sweeping = false;
			hugging = false;
		}
		if (bootsTimer > 0f) bootsTimer -= Time.deltaTime;
		else bootsActive = false;
		if (invisTime > 0f)
		{
			invisTime -= Time.deltaTime;
			gameObject.layer = 7;
		}
		else gameObject.layer = 3;
		if (infReachTime > 0f)
		{
			infReachTime -= Time.deltaTime;
			reachDist = 99999f;
		}
		else reachDist = baseReachDist;
		if (facultyTime > 0f)
		{
			facultyTime -= Time.deltaTime;
			nametagUI.SetActive(true);
		}
		else nametagUI.SetActive(false);
		if (Input.GetAxis("Mouse ScrollWheel") > 0f && Time.timeScale != 0f) DecreaseItemSelection();
		else if (Input.GetAxis("Mouse ScrollWheel") < 0f && Time.timeScale != 0f) IncreaseItemSelection();
		if (Input.GetButtonDown("Item") == true && Time.timeScale != 0f && !inVent && !frozen) UseItem();
		if (Input.GetButtonDown("Select Item 0"))
		{
			itemSelected = 0;
			itemSelectBox.anchoredPosition = new Vector3((float)itemSelectOffset[itemSelected], 197f, 0f);
			itemNameText.text = gameController.itemNames[item[itemSelected]];
		}
		if (Input.GetButtonDown("Select Item 1"))
		{
			itemSelected = 1;
			itemSelectBox.anchoredPosition = new Vector3((float)itemSelectOffset[itemSelected], 197f, 0f);
			itemNameText.text = gameController.itemNames[item[itemSelected]];
		}
		if (Input.GetButtonDown("Select Item 2"))
		{
			itemSelected = 2;
			itemSelectBox.anchoredPosition = new Vector3((float)itemSelectOffset[itemSelected], 197f, 0f);
			itemNameText.text = gameController.itemNames[item[itemSelected]];
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
			if (charControl.velocity.magnitude > 0.1f & !hugging & !sweeping) ResetGuilt("running", 0.1f);
		}
		else if (!frozen) playerSpeed = walkSpeed;
		if (Input.GetAxis("Up/Down") == 1) movement = transform.forward;
		if (Input.GetAxis("Up/Down") == -1) movement = -transform.forward;
		if (Input.GetAxis("Left/Right") == 1) sideMovement = transform.right;
		if (Input.GetAxis("Left/Right") == -1) sideMovement = -transform.right;
		playerSpeed *= Time.deltaTime;
		moveDirection = (movement + sideMovement).normalized * playerSpeed;
		if (!(!jumpRope & !sweeping & !hugging))
		{
			if (sweeping && !bootsActive) moveDirection = gottaSweep.velocity * Time.deltaTime + moveDirection * 0.3f;
			else if (hugging && !bootsActive) moveDirection = (firstPrize.velocity * 1.2f * Time.deltaTime + (new Vector3(firstPrizeTransform.position.x, height, firstPrizeTransform.position.z) + new Vector3((float)Mathf.RoundToInt(firstPrizeTransform.forward.x), 0f, (float)Mathf.RoundToInt(firstPrizeTransform.forward.z)) * 3f - transform.position)) * (float)principalBugFixer;
			else if (jumpRope)
			{
				if (cameraScript.jumpHeight > 0f) moveDirection = ((movement + sideMovement).normalized * playerSpeed) / 4;
				else moveDirection = new Vector3 (0f,0f,0f);
			}
		}
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
        if (charControl.velocity.magnitude > charControl.minMoveDistance && !dangerouslyTeleporting && !inVent)
		{
			if (running) stamina = Mathf.Max(stamina - staminaDecreaseSpd * Time.deltaTime, 0f);
		}
		else if (stamina < staminaMax)
		{
			stamina += staminaIncreaseSpd * Time.deltaTime;
		}
		staminaRound = (Mathf.FloorToInt(stamina));
		StaminaText.text = staminaRound.ToString() + "%";
		staminaBar.value = staminaRound / staminaMax * 100f;
    }
	public void Interactions()
	{
        interactDown = Input.GetButtonDown("Interact");
		if (interactDown && Time.timeScale != 0f)
		{
			Ray ray = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit))
			{
				if (Vector3.Distance(raycastHit.transform.position, transform.position) < reachDist)
				{
					if (raycastHit.transform.name == "ItemPickup")
					{
						CollectItem(raycastHit.collider.gameObject.GetComponent<ItemPickup>().itemID);
						gameAudioDevice.PlayOneShot(raycastHit.collider.gameObject.GetComponent<ItemPickup>().itemClickAudio);
						raycastHit.transform.gameObject.SetActive(false);
					}
					else if (raycastHit.transform.name == "Present")
					{
						CollectRandomItem(false);
						gameAudioDevice.PlayOneShot(raycastHit.collider.gameObject.GetComponent<ItemPickup>().itemClickAudio);
						raycastHit.transform.gameObject.SetActive(false);
					}
					else if (raycastHit.transform.name == "YTP Green")
					{
						gameController.YTPGain(25);
						gameAudioDevice.PlayOneShot(raycastHit.collider.gameObject.GetComponent<ItemPickup>().ytpGreenSound);
						raycastHit.transform.gameObject.SetActive(false);
					}
					else if (raycastHit.transform.name == "YTP Silver")
					{
						gameController.YTPGain(50);
						gameAudioDevice.PlayOneShot(raycastHit.collider.gameObject.GetComponent<ItemPickup>().ytpSilverSound);
						raycastHit.transform.gameObject.SetActive(false);
					}
					else if (raycastHit.transform.name == "YTP Gold")
					{
						gameController.YTPGain(100);
						gameAudioDevice.PlayOneShot(raycastHit.collider.gameObject.GetComponent<ItemPickup>().ytpGoldSound);
						raycastHit.transform.gameObject.SetActive(false);
					}
					else if (raycastHit.transform.name == "YTP Diamond")
					{
						gameController.YTPGain(250);
						gameAudioDevice.PlayOneShot(raycastHit.collider.gameObject.GetComponent<ItemPickup>().ytpGoldSound);
						raycastHit.transform.gameObject.SetActive(false);
					}
					else if (raycastHit.collider.gameObject.GetComponent<StandardDoorScript>())
					{
						StandardDoorScript doorScript = raycastHit.collider.gameObject.GetComponent<StandardDoorScript>();
						if (!doorScript.DoorLocked)
						{
							if (doorScript.silentOpens > 0 && doorScript.openTime <= 0f) doorScript.silentOpens--;
							doorScript.OpenDoor(true);
						}
					}
					else if (raycastHit.collider.gameObject.GetComponent<NumBalloon>())
					{
						NumBalloon balloonNumberScr = raycastHit.collider.gameObject.GetComponent<NumBalloon>();
						if (!balloonNumberScr.mathMachine.answered)
						{
							if (!balloonNumberScr.currentlyHeld) 
							{
								heldBalloon = balloonNumberScr.balloonNumber;
								balloonNumberScr.currentlyHeld = true;
							}
						}
					}
					else if (raycastHit.collider.gameObject.GetComponent<BusterBalloon>())
					{
						raycastHit.collider.gameObject.GetComponent<BusterBalloon>().PopBalloon();
					}
					else if (raycastHit.transform.name == "NBMathMachine" || raycastHit.transform.name == "ItemMathMachine")
					{
						if (heldBalloon != -1f) raycastHit.collider.gameObject.GetComponent<MathMachine>().AnswerQuestion();
					}
					else if (raycastHit.transform.name == "NBBalloonBuster" || raycastHit.transform.name == "ItemBalloonBuster")
					{
						raycastHit.collider.gameObject.GetComponent<BalloonBuster>().AnswerQuestion();
					}
					else if (raycastHit.collider.gameObject.GetComponent<NotebookScript>())
					{
						NotebookScript nbScript = raycastHit.collider.gameObject.GetComponent<NotebookScript>();
						nbScript.collected = true;
						gameController.NotebookCollect();
						nbScript.respawnTime = 120f;
						nbScript.notebookAudio.PlayOneShot(nbScript.notebookCollect, 1f);
						if (stamina <= staminaMax) stamina = staminaMax;
						if (nbScript.notebookMinigame == "YCTP")
						{
							gameController.YTPGain(25);
							GameObject gameObject = Instantiate<GameObject>(nbScript.notebookYCTP);
							gameObject.GetComponent<YCTPMinigame>().gameController = gameController;
							gameObject.GetComponent<YCTPMinigame>().spoopBaldiScript = nbScript.baldi;
							gameObject.GetComponent<YCTPMinigame>().playerPosition = transform.position;
						}
						else if (nbScript.notebookMinigame == "")
						{
							gameController.YTPGain(25);
							if (spoopBaldiScript.isActiveAndEnabled)
							{
								if (gameController.gameMode == "endless") spoopBaldiScript.GetAngry(-1f);
								else spoopBaldiScript.GetAngry(1f);
							}
						}
						else if (nbScript.notebookMinigame == "mathmachine" || nbScript.notebookMinigame == "balloonbuster" && gameController.spoopMode)
						{
							if (gameController.gameMode == "endless") spoopBaldiScript.GetAngry(-1f);
							else spoopBaldiScript.GetAngry(1f);
						}
						if (gameController.notebooks == gameController.maxNotebooks && !gameController.escapeSequence)
						{
							gameController.SchoolhouseEscapeStart();
							gameController.escapeSequence = true;
						}
					}
					else if (raycastHit.transform.name == "WaterFountain")
					{
						if (stamina <= staminaMax)
						{
							stamina = staminaMax;
							gameAudioDevice.PlayOneShot(fountainSlurpSFX);
						}
					}
					else if (raycastHit.transform.name == "Temp")
					{
						// Template for new interactable objects based on naming.
					}
					else if (raycastHit.collider.gameObject.GetComponent<PlayerScript>())
					{
						// Template for new interactable objects based on components.
					}
				}
            }
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
		itemNameText.text = gameController.itemNames[item[itemSelected]];
	}
	private void DecreaseItemSelection()
	{
		itemSelected--;
		if (itemSelected < 0)
		{
			itemSelected = 2;
		}
		itemSelectBox.anchoredPosition = new Vector3((float)itemSelectOffset[itemSelected], 197f, 0f);
		itemNameText.text = gameController.itemNames[item[itemSelected]];
	}
	private void UpdateItemSelection()
	{
		itemSelectBox.anchoredPosition = new Vector3((float)itemSelectOffset[itemSelected], 197f, 0f);
		itemNameText.text = gameController.itemNames[item[itemSelected]];
	}
	public void CollectItem(int item_ID)
	{
		if (item[itemSelected] == 0)
		{
			item[itemSelected] = item_ID;
			itemSlot[itemSelected].texture = gameController.itemSlotTextures[item_ID];
		}
		else if (item[0] == 0)
		{
			item[0] = item_ID;
			itemSlot[0].texture = gameController.itemSlotTextures[item_ID];
		}
		else if (item[1] == 0)
		{
			item[1] = item_ID;
            itemSlot[1].texture = gameController.itemSlotTextures[item_ID];
        }
		else if (item[2] == 0)
		{
			item[2] = item_ID;
            itemSlot[2].texture = gameController.itemSlotTextures[item_ID];
        }
		else
		{
			item[itemSelected] = item_ID;
			itemSlot[itemSelected].texture = gameController.itemSlotTextures[item_ID];
		}
		itemNameText.text = gameController.itemNames[item[itemSelected]];
	}
	public void UseItem()
	{
		if (item[itemSelected] != 0)
		{
			if (item[itemSelected] == 1)
			{
				Instantiate<GameObject>(bsodaSpray, transform.position, cameraScript.transform.rotation);
				DeleteItem();
				ResetGuilt("drink", 1f);
				gameAudioDevice.PlayOneShot(sodaSFX);
			}
			else if (item[itemSelected] == 2)
			{
				if (PlayerPrefs.GetInt("DietItemModifier") == 1) stamina = staminaMax * 1.5f;
				else stamina = staminaMax * 2f;
				DeleteItem();
				gameAudioDevice.PlayOneShot(zBarSFX);
			}
			else if (item[itemSelected] == 3)
			{
				Ray ray = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider.tag == "SwingingDoor" & Vector3.Distance(transform.position, raycastHit.transform.position) <= 10f))
				{	
					if (PlayerPrefs.GetInt("DietItemModifier") == 1) raycastHit.collider.gameObject.GetComponent<SwingingDoorScript>().LockDoor(15f);
					else raycastHit.collider.gameObject.GetComponent<SwingingDoorScript>().LockDoor(30f);
					DeleteItem();
				}
			}
			else if (item[itemSelected] == 4)
			{
				Ray ray3 = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
				RaycastHit raycastHit3;
				if (Physics.Raycast(ray3, out raycastHit3))
				{
					if ((raycastHit3.collider.name == "Vending") & Vector3.Distance(transform.position, raycastHit3.transform.position) <= 10f)
					{
						if (!raycastHit3.collider.gameObject.GetComponent<VendingScript>().used)
						{
							raycastHit3.collider.gameObject.GetComponent<VendingScript>().Dispense();
							DeleteItem();
							if (raycastHit3.collider.gameObject.GetComponent<VendingScript>().returnedItem != 0)
							{
								CollectItem(raycastHit3.collider.gameObject.GetComponent<VendingScript>().returnedItem);
							}
							else
							{
        						int giveTeleporter = Mathf.RoundToInt(UnityEngine.Random.Range(1,8));
								if (giveTeleporter == 3) CollectItem(11);
								else CollectRandomItem(true);
							}
						}
					}
					else if (raycastHit3.collider.name == "Payphone" & Vector3.Distance(transform.position, raycastHit3.transform.position) <= 10f)
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
					if (raycastHit3.collider.name == "AntiHearingTape" & Vector3.Distance(transform.position, raycastHit3.transform.position) <= 10f)
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
				if (Physics.Raycast(ray2, out raycastHit2) && (raycastHit2.collider.tag == "Door" & Vector3.Distance(transform.position, raycastHit2.transform.position) <= 10f))
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
				Ray ray6 = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
				RaycastHit raycastHit6;
				if (jumpRope)
				{
					DeactivateJumpRope();
					playtime.GetComponent<Playtime>().Disappoint();
					DeleteItem();
				}
				else if (Physics.Raycast(ray6, out raycastHit6) && raycastHit6.collider.gameObject.GetComponent<FirstPrize>())
				{
					raycastHit6.collider.gameObject.GetComponent<FirstPrize>().CutWires();
					DeleteItem();
				}
			}
			else if (item[itemSelected] == 8)
			{
				Ray ray = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider.tag == "Door" & Vector3.Distance(transform.position, raycastHit.transform.position) <= 10f))
				{
					raycastHit.collider.gameObject.GetComponent<StandardDoorScript>().SilenceDoor();
					DeleteItem();
				}
			}
			else if (item[itemSelected] == 9)
			{
				GameObject gameObject = Instantiate<GameObject>(alarmClock, transform.position, cameraScript.transform.rotation);
				gameObject.GetComponent<AlarmClock>().spoopBaldiScript = spoopBaldiScript;
				DeleteItem();
			}
			else if (item[itemSelected] == 10)
			{
				ActivateBoots();
				DeleteItem();
			}
			else if (item[itemSelected] == 11)
			{
				StartCoroutine(DangerousTeleporter());
				DeleteItem();
			}
			else if (item[itemSelected] == 14)
			{
				Instantiate<GameObject>(chalkDust, transform.position, cameraScript.transform.rotation);
				DeleteItem();
				gameAudioDevice.PlayOneShot(chalkSFX);
			}
			else if (item[itemSelected] == 15)
			{
				if (principalScript.isActiveAndEnabled) principalScript.WhistleItemActivated();
				gameAudioDevice.PlayOneShot(whistleSFX);
				if (PlayerPrefs.GetInt("DietItemModifier") == 1 && spoopBaldiScript.isActiveAndEnabled) spoopBaldiScript.Hear(transform.position, 64f);
				DeleteItem();
			}
			else if (item[itemSelected] == 16)
			{
				if (PlayerPrefs.GetInt("DietItemModifier") == 1) facultyTime = 15f;
				else facultyTime = 30f;
				DeleteItem();
			}
			else if (item[itemSelected] == 19)
			{
				Instantiate<GameObject>(dietBsodaSpray, transform.position, cameraScript.transform.rotation);
				DeleteItem();
				gameAudioDevice.PlayOneShot(sodaSFX);
			}
			else if (item[itemSelected] == 21)
			{
				if (PlayerPrefs.GetInt("DietItemModifier") == 1) invisTime = 15f;
				else invisTime = 30f;
				gameAudioDevice.PlayOneShot(elixirSFX);
				DeleteItem();
			}
			else if (item[itemSelected] == 25)
			{
				Ray ray = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider.tag == "Campfire" & Vector3.Distance(transform.position, raycastHit.transform.position) <= 10f))
				{
					raycastHit.collider.gameObject.GetComponent<CampFireScript>().FuelFire(10f);
					DeleteItem();
				}
			}
			else if (item[itemSelected] == 26)
			{
				Ray ray = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider.tag == "Campfire" & Vector3.Distance(transform.position, raycastHit.transform.position) <= 10f))
				{
					raycastHit.collider.gameObject.GetComponent<CampFireScript>().FuelFire(15f);
					DeleteItem();
				}
			}
			else if (item[itemSelected] == 27)
			{
				Ray ray = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider.tag == "Campfire" & Vector3.Distance(transform.position, raycastHit.transform.position) <= 10f))
				{
					raycastHit.collider.gameObject.GetComponent<CampFireScript>().FuelFire(20f);
					DeleteItem();
				}
			}
			else if (item[itemSelected] == 38)
			{
				infReachTime = 60f;
				DeleteItem();
			}
			// Template for new items.
			else if (item[itemSelected] == 99)
			{
				
			}
		}
	}
	private IEnumerator DangerousTeleporter()
	{
		dangerouslyTeleporting = true;
		GetComponent<Collider>().enabled = false;
		int teleports = UnityEngine.Random.Range(12, 16);
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
				height = 4f;
			}
			yield return null;
		}
		dangerouslyTeleporting = false;
		GetComponent<Collider>().enabled = true;
		yield break;
	}
	private void Teleport()
	{
		gameController.wanderSelector.GetNewTarget();
		transform.position = gameController.wanderSelector.transform.position + Vector3.up * height;
		gameAudioDevice.PlayOneShot(teleportSFX);
	}
	private void DeleteItem()
	{
		item[itemSelected] = 0;
		itemSlot[itemSelected].texture = gameController.itemSlotTextures[0];
		itemNameText.text = gameController.itemNames[item[itemSelected]];
	}
	public void DeleteSpecificItem(int id)
	{
		item[id] = 0;
		itemSlot[id].texture = gameController.itemSlotTextures[0];
		itemNameText.text = gameController.itemNames[item[itemSelected]];
	}
	public void CollectRandomItem(bool vendItems)
	{
		int randomItem = Mathf.RoundToInt(UnityEngine.Random.Range(1, 21));
		// Used to prevent certain items from being given.
		if (vendItems) while (randomItem == 0 || randomItem == 4 || randomItem == 12 || randomItem == 17 || randomItem == 18 || randomItem == 20) randomItem = Mathf.RoundToInt(UnityEngine.Random.Range(1, 21));
		else while (randomItem == 0 || randomItem == 11 || randomItem == 12 || randomItem == 13 || randomItem == 17 || randomItem == 18 || randomItem == 20) randomItem = Mathf.RoundToInt(UnityEngine.Random.Range(1, 21));
		if (item[itemSelected] == 0)
		{
			item[itemSelected] = randomItem;
			itemSlot[itemSelected].texture = gameController.itemSlotTextures[randomItem];
		}
		else if (item[0] == 0)
		{
			item[0] = randomItem;
			itemSlot[0].texture = gameController.itemSlotTextures[randomItem];
		}
		else if (item[1] == 0)
		{
			item[1] = randomItem;
            itemSlot[1].texture = gameController.itemSlotTextures[randomItem];
        }
		else if (item[2] == 0)
		{
			item[2] = randomItem;
            itemSlot[2].texture = gameController.itemSlotTextures[randomItem];
        }
		else
		{
			item[itemSelected] = randomItem;
			itemSlot[itemSelected].texture = gameController.itemSlotTextures[randomItem];
		}
		itemNameText.text = gameController.itemNames[item[itemSelected]];
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.name == "Playtime" & !jumpRope & playtime.playCool <= 0f && facultyTime <= 0f)
		{
			ActivateJumpRope();
		}
	}
	private void OnTriggerStay(Collider other)
	{
		if (other.transform.name == "GottaSweep")
		{
			sweeping = true;
			sweepingFailsave = 0.2f;
		}
		else if (other.transform.name == "1stPrize" || other.transform.name == "5thPrize")
		{
			firstPrize = other.transform.gameObject.GetComponent<NavMeshAgent>();
			firstPrizeTransform = other.transform;
			if (firstPrize.velocity.magnitude > 5f)
			{
				hugging = true;
				sweepingFailsave = 0.2f;
			}
		}
		else if (other.transform.name == "ChalkDust")
		{
			if (invisTime <= 0.1f) invisTime = 0.1f;
		}
		else if (other.transform.tag == "LightingAffected")
		{
			Renderer otherRenderer = other.gameObject.GetComponent<Renderer>();
			collidedColor = otherRenderer.material.color;
			if (collidedColor.r < 0.16f && collidedColor.g < 0.16f && collidedColor.b < 0.16f)
			{
				if (invisTime <= 0.1f) invisTime = 0.1f;
			}
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.transform.name == "OfficeTrigger")
		{
			ResetGuilt("escape", officeDoor.lockTime);
		}
		else if (other.transform.name == "GottaSweep")
		{
			sweeping = false;
		}
		else if (other.transform.name == "1stPrize" || other.transform.name == "5thPrize")
		{
			hugging = false;
		}
	}
	public void ResetGuilt(string type, float amount)
	{
		if (amount >= guilt && guiltType != "outdoors")
		{
			guilt = amount;
			guiltType = type;
		}
	}
	private void GuiltCheck()
	{
		if (guilt > 0f)
		{
			guilt -= Time.deltaTime;
		}
	}
	public void ActivateBoots()
	{
		bootsActive = true;
		bootsTimer = 60f;
	}
	public void ActivateJumpRope()
	{
		jumpRopeScreen.SetActive(true);
		jumpRope = true;
		frozenPosition = transform.position;
	}
	public void DeactivateJumpRope()
	{
		jumpRopeScreen.SetActive(false);
		jumpRope = false;
	}
	private void OnCollisionStay(Collision other)
	{
	}
    public CharacterController charControl;
	public CameraScript cameraScript;
	public Color collidedColor;
	public StandardDoorScript officeDoor;
	public AudioClip fountainSlurpSFX;
	public float playerSpeed;
	public float walkSpeed;
	public float reachDist;
	public float baseReachDist = 10f;
	public int principalBugFixer;
	public float runSpeed;
	public float stamina;
	public bool bootsActive;
	public bool frozen;
	public bool jumpRope;
	public bool sweeping;
	public bool hugging;
	public bool interactDown;
	public float heldBalloon;
	public float sweepingFailsave;
	public float staminaDecreaseSpd;
	public float staminaIncreaseSpd;
	public float staminaMax;
	public float mouseSensitivity;
	public float staminaRound;
	public float height;
	public float facultyTime;
	public NavMeshAgent gottaSweep;
	public NavMeshAgent firstPrize;
	public Playtime playtime;
	public Transform firstPrizeTransform;
	public Slider staminaBar;
	public Quaternion playerRotation;
	public TMP_Text StaminaText;
	public GameController gameController;
	public Vector3 moveDirection;
	public Vector3 frozenPosition;
	public GameObject jumpRopeScreen;
	public GameObject playtimeObj;
	public GameObject nametagUI;
	private bool running;
	private float bootsTimer;
	public string guiltType;
	public float guilt;
	public float initGuilt;
	public float invisTime;
	public float infReachTime;
	public bool dangerouslyTeleporting;
	public bool inVent;
	public float ventSpeed = 35f;
	public Vector3 ventTarget;
	public AudioSource gameAudioDevice;
	public SpoopBaldi spoopBaldiScript;
	public Principal principalScript;
	
	public TMP_Text itemNameText;
	public int itemSelected;
	public RectTransform itemSelectBox;
	public RawImage[] itemSlot = new RawImage[3];
	public int[] item = new int[3];
	private int[] itemSelectOffset;
	public bool bonusItemsAllowed;
	public AudioClip teleportSFX;
	public AudioClip sodaSFX;
	public AudioClip elixirSFX;
	public AudioClip zBarSFX;
	public AudioClip chalkSFX;
	public AudioClip whistleSFX;
	public GameObject bsodaSpray;
	public GameObject dietBsodaSpray;
	public GameObject alarmClock;
	public GameObject chalkDust;
}