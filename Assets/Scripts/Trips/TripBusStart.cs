using UnityEngine;
using UnityEngine.SceneManagement;

public class TripBusStart : MonoBehaviour
{
	public void OnTriggerEnter(Collider other)
	{
        if (other.name == "Player")
        {
            if (gc.item[0] == 20 || gc.item[1] == 20 || gc.item[2] == 20)
            {
                SceneManager.LoadScene(scene);
            }
            else
            {
                baldiAudio.Stop();
                baldiAudio.PlayOneShot(needBusPassSFX);
            }
        }
    }
    public AudioSource baldiAudio;
    public AudioClip needBusPassSFX;
    public BasicGameController gc;
    public string scene;
}