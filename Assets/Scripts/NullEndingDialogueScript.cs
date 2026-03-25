using UnityEngine;
using UnityEngine.SceneManagement;

public class NullEndingDialogueScript : MonoBehaviour
{
	private void Update()
	{
		if (!audioDevice.isPlaying & played)
		{
            if (Application.platform != RuntimePlatform.WebGLPlayer)
            {
			    Application.Quit();
            }
            else
            {
                SceneManager.LoadScene("WarningScreen");
            }
            Debug.Log("Game exited");
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.name == "Player" & !played)
		{
			audioDevice.Play();
			played = true;
		}
	}
	public AudioSource audioDevice;
	private bool played;
}