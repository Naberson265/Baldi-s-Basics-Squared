using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class ExitMP : MonoBehaviour
{
    public void ExitRoom(string sceneFallback)
    {
        if (!disconnect) PhotonNetwork.LeaveRoom();
        exitToScene = sceneFallback;
        if (disconnect) PhotonNetwork.Disconnect();
        if (exitToScene != "" && exitToScene != " ") SceneManager.LoadScene(exitToScene);
    }
    public bool disconnect;
    private string exitToScene;
}
