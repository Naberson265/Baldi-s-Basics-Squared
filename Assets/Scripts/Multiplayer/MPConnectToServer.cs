using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class MPConnectToServer : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("MPlobbySelect");
    }
}
