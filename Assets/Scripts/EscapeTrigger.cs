using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EscapeTrigger : MonoBehaviour
{
	void Start()
	{
		StartCoroutine(LateStart());
	}
	IEnumerator LateStart()
	{
		yield return null;
		gameController = FindFirstObjectByType<GameController>();
		playerScript = FindFirstObjectByType<PlayerController>();
	}
	private void OnTriggerEnter(Collider other)
	{
		if (gameController.escapeSequence & other.tag == "Player")
		{
			if (PlayerPrefs.GetInt("StoryModeAchievement") != 1) 
			{
				PlayerPrefs.SetInt("StoryModeAchievement", 1);
				PlayerPrefs.SetInt("YTPs", PlayerPrefs.GetInt("YTPs") + 100);
			}
			if (gameController.mapMode == "trueclassic" || gameController.mapMode == "main")
			{
				if (gameController.failedNotebooks >= gameController.maxNotebooks)
				{
            		PlayerPrefs.SetInt("YTPs", PlayerPrefs.GetInt("YTPs") + 50);
                	SceneManager.LoadScene("SecretClassicRoom");
					PlayerPrefs.SetInt("CSEAchievement", 1);
				}
			}
			if (gameController.mapMode == "grandopening")
			{
				if (gameController.failedNotebooks >= gameController.maxNotebooks)
				{
            		PlayerPrefs.SetInt("YTPs", PlayerPrefs.GetInt("YTPs") + 50);
                	SceneManager.LoadScene("SecretClassicRoom");
					PlayerPrefs.SetInt("GOSEAchievement", 1);
				}
			}
			if (PlayerPrefs.GetInt("TimeOutModifier") == 1 && PlayerPrefs.GetInt("DietItemModifier") == 1)
			{
				if (PlayerPrefs.GetInt("FunSettingsAchievement") != 1) 
				{
					PlayerPrefs.SetInt("FunSettingsAchievement", 1);
					PlayerPrefs.SetInt("YTPs", PlayerPrefs.GetInt("YTPs") + 100);
				}
			}
			if (PlayerPrefs.GetInt("ItemRestriction") == 1 && PlayerPrefs.GetInt("StaminaRestriction") == 1 && PlayerPrefs.GetInt("LookBackRestriction") == 1)
			{
				if (PlayerPrefs.GetInt("RestrictionsAchievement") != 1) 
				{
					PlayerPrefs.SetInt("RestrictionsAchievement", 1);
					PlayerPrefs.SetInt("YTPs", PlayerPrefs.GetInt("YTPs") + 100);
				}
			}
			if (PlayerPrefs.GetInt("ItemRestriction") == 1 && PlayerPrefs.GetInt("StaminaRestriction") == 1 && PlayerPrefs.GetInt("LookBackRestriction") == 1
			&& PlayerPrefs.GetInt("TimeOutModifier") == 1 && PlayerPrefs.GetInt("DietItemModifier") == 1)
			{
				if (PlayerPrefs.GetInt("AllModifiersAchievement") != 1) 
				{
					PlayerPrefs.SetInt("AllModifiersAchievement", 1);
					PlayerPrefs.SetInt("YTPs", PlayerPrefs.GetInt("YTPs") + 250);
				}
			}
			else
			{
				gameController.FinalExit();
				Time.timeScale = 0f;
			}
		}
	}
	public GameController gameController;
	public PlayerController playerScript;
}