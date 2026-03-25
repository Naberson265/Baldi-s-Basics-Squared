using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BonusItem : MonoBehaviour
{
    public void Update()
    {
        itemImg.texture = itemIcons[PlayerPrefs.GetInt(itemSlotString)];
        if (itemToggle.isOn)
        {
            if (PlayerPrefs.GetInt(itemSlotString) != 0 && PlayerPrefs.GetInt("BonusItemSlot1Enabled") == 0 && PlayerPrefs.GetInt("BonusItemSlot2Enabled") != slotId && PlayerPrefs.GetInt("BonusItemSlot3Enabled") != slotId) PlayerPrefs.SetInt("BonusItemSlot1Enabled", slotId);
            else if (PlayerPrefs.GetInt(itemSlotString) != 0 && PlayerPrefs.GetInt("BonusItemSlot2Enabled") == 0 && PlayerPrefs.GetInt("BonusItemSlot3Enabled") != slotId && PlayerPrefs.GetInt("BonusItemSlot1Enabled") != slotId) PlayerPrefs.SetInt("BonusItemSlot2Enabled", slotId);
            else if (PlayerPrefs.GetInt(itemSlotString) != 0 && PlayerPrefs.GetInt("BonusItemSlot3Enabled") == 0 && PlayerPrefs.GetInt("BonusItemSlot2Enabled") != slotId && PlayerPrefs.GetInt("BonusItemSlot1Enabled") != slotId) PlayerPrefs.SetInt("BonusItemSlot3Enabled", slotId);
        }
        else if (!itemToggle.isOn)
        {
            if (PlayerPrefs.GetInt("BonusItemSlot1Enabled") == slotId) PlayerPrefs.SetInt("BonusItemSlot1Enabled", 0);
            else if (PlayerPrefs.GetInt("BonusItemSlot2Enabled") == slotId) PlayerPrefs.SetInt("BonusItemSlot2Enabled", 0);
            else if (PlayerPrefs.GetInt("BonusItemSlot3Enabled") == slotId) PlayerPrefs.SetInt("BonusItemSlot3Enabled", 0);
        }
        if (itemToggle.isOn && PlayerPrefs.GetInt(itemSlotString) == 0) itemToggle.isOn = false;
    }
    public Toggle itemToggle;
    public Texture[] itemIcons = new Texture[39];
    public string itemSlotString;
    public int slotId;
    public RawImage itemImg;
}
