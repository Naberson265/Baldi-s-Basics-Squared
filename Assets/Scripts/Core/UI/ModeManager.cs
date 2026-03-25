using UnityEngine;
using UnityEngine.UI;

public class ModeManager : MonoBehaviour
{
    public void Awake()
    {
        PlayerPrefs.SetInt("BonusItemSlot1Enabled", 0);
        PlayerPrefs.SetInt("BonusItemSlot2Enabled", 0);
        PlayerPrefs.SetInt("BonusItemSlot3Enabled", 0);
    }
    public void Update()
    {
        if (Input.GetButtonDown("Interact")) audSource.PlayOneShot(clickSound);
        if (ITOModifier.isOn) PlayerPrefs.SetInt("TimeOutModifier", 1);
        else PlayerPrefs.SetInt("TimeOutModifier", 0);
        if (HAModifier.isOn) PlayerPrefs.SetInt("HardActivitiesModifier", 1);
        else PlayerPrefs.SetInt("HardActivitiesModifier", 0);
        if (OADModifier.isOn) PlayerPrefs.SetInt("DietItemModifier", 1);
        else PlayerPrefs.SetInt("DietItemModifier", 0);
        if (MMModifier.isOn) PlayerPrefs.SetInt("MirrorModeModifier", 1);
        else PlayerPrefs.SetInt("MirrorModeModifier", 0);
        if (itemRestriction.isOn) PlayerPrefs.SetInt("ItemRestriction", 1);
        else PlayerPrefs.SetInt("ItemRestriction", 0);
        if (sprintRestriction.isOn) PlayerPrefs.SetInt("StaminaRestriction", 1);
        else PlayerPrefs.SetInt("StaminaRestriction", 0);
        if (behindRestriction.isOn) PlayerPrefs.SetInt("LookBackRestriction", 1);
        else PlayerPrefs.SetInt("LookBackRestriction", 0);
    }
    public void SetMapMode(string mapMode)
    {
        PlayerPrefs.SetString("MapMode", mapMode);
    }
    public void SetGameMode(string gameMode)
    {
        PlayerPrefs.SetString("GameMode", gameMode);
    }
    public Toggle ITOModifier;
    public Toggle HAModifier;
    public Toggle OADModifier;
    public Toggle MMModifier;
    public Toggle itemRestriction;
    public Toggle sprintRestriction;
    public Toggle behindRestriction;
    public AudioClip clickSound;
    public AudioSource audSource;
}
