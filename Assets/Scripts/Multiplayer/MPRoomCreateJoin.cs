using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using ExitGames.Client.Photon;

public class MPRoomCreateJoin : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        if (PlayerPrefs.GetString("MPUsername") != "" && PlayerPrefs.GetString("MPUsername") != " ") userField.text = PlayerPrefs.GetString("MPUsername");
    }
    private void Update()
    {
        PlayerPrefs.SetString("MPUsername", userField.text);
        PlayerPrefs.SetInt("MPCharSkin", 0);
        PhotonNetwork.NickName = PlayerPrefs.GetString("MPUsername");
    }
    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 5;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        roomOptions.EmptyRoomTtl = 10000;
        roomOptions.PlayerTtl = 8000;
        roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable{{"Map", 1},{"Modifier", 0}};
        roomOptions.CustomRoomPropertiesForLobby = new string [] {"Map", "Modifier"};
        string roomCode;
        if (createField.text != "" && createField.text != " ") roomCode = createField.text;
        else roomCode = Mathf.RoundToInt(Random.Range(100000,999999)).ToString();
        PhotonNetwork.CreateRoom(roomCode, roomOptions);
    }
    public void JoinRoom()
    {
        if (joinField.text != "" && joinField.text != " ") PhotonNetwork.JoinRoom(joinField.text);
    }
    public void OnJoinRoomFailed()
    {
        Debug.Log("Failure!!");
    }
    public override void OnJoinedRoom()
    {
        int charSkin = PlayerPrefs.GetInt("MPCharSkin");
        ExitGames.Client.Photon.Hashtable customUserProps = new ExitGames.Client.Photon.Hashtable();
        customUserProps.Add("CharacterSkin", charSkin);
        PhotonNetwork.LocalPlayer.SetCustomProperties(customUserProps);
        PhotonNetwork.LoadLevel("MPWaitRoom");
    }
    public TMP_InputField createField;
    public TMP_InputField joinField;
    public TMP_InputField userField;
}
