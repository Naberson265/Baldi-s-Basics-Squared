using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGameScript : MonoBehaviour
{
    public void ExitGame()
    {
        PlayerPrefs.Save();
        Debug.Log("Game exited");
        if (Application.platform != RuntimePlatform.WebGLPlayer)
        {
		    Application.Quit();
        }
        else
        {
            SceneManager.LoadScene("WarningScreen");
        }
    }
}
