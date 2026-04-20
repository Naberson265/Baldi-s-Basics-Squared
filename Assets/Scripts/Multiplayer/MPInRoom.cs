using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MPInRoomMenu : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    private void Update()
    {
        roomCodeText.text = PhotonNetwork.CurrentRoom.Name;
        foreach (TMP_Text userText in userNameTexts)
        {
            userText.color = noPlayerColor;
            userText.text = "No One";
        }
        foreach (Image userImage in userSkinImages)
        {
            userImage.color = noPlayerColor;
            userImage.sprite = unjoinedSkin;
        }
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            userNameTexts[player.ActorNumber - 1].text = player.NickName;
            userNameTexts[player.ActorNumber - 1].color = playerColor;
            userSkinImages[player.ActorNumber - 1].sprite = skinIndex[0];
            userSkinImages[player.ActorNumber - 1].color = playerColor;
        }
        if (PhotonNetwork.IsMasterClient)
        {
            foreach (GameObject uiObject in hostOnlyObjects)
            {
                uiObject.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject uiObject in hostOnlyObjects)
            {
                uiObject.SetActive(false);
            }
        }
        if (!PhotonNetwork.IsMasterClient)
        {
            mapText.text = mapNames[(int)PhotonNetwork.CurrentRoom.CustomProperties["Map"]];
            modifierText.text = modifierNames[(int)PhotonNetwork.CurrentRoom.CustomProperties["Modifier"]];
        }
    }
    public void IncreaseRoomSetting(int setting)
    {
        if (setting == 0)
        {
            if (rMap < 2) rMap++;
            else rMap = 0;
        }
        if (setting == 1)
        {
            if (rModifier < 4) rModifier++;
            else rModifier = 0;
        }
        UpdateRoomSettings();
    }
    public void DecreaseRoomSetting(int setting)
    {
        if (setting == 0)
        {
            if (rMap > 0) rMap--;
            else rMap = 2;
        }
        if (setting == 1)
        {
            if (rModifier > 0) rModifier--;
            else rModifier = 4;
        }
        UpdateRoomSettings();
    }
    public void UpdateRoomSettings()
    {
        mapText.text = mapNames[rMap];
        modifierText.text = modifierNames[rModifier];
        if (PhotonNetwork.IsMasterClient)
        {
            updatingProperties = new ExitGames.Client.Photon.Hashtable { {"Map", rMap}, {"Modifier", rModifier} };
            PhotonNetwork.CurrentRoom.SetCustomProperties(updatingProperties);
        }
    }
    public void LoadGameScene()
    {
        if (PhotonNetwork.IsMasterClient) PhotonNetwork.LoadLevel("MPGame");
    }
    public GameObject[] hostOnlyObjects;
    public int rMap;
    public int rModifier;
    public string[] mapNames;
    public string[] modifierNames;
    public ExitGames.Client.Photon.Hashtable updatingProperties;
    public TMP_Text[] userNameTexts = new TMP_Text[5];
    public Image[] userSkinImages = new Image[5];
    public Sprite[] skinIndex;
    public Sprite unjoinedSkin;
    public TMP_Text roomCodeText;
    public TMP_Text mapText;
    public TMP_Text modifierText;
    public Color playerColor;
    public Color noPlayerColor;
}
