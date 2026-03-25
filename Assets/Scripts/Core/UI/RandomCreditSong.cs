using UnityEngine;

public class RandomCreditSong : MonoBehaviour
{
    public void Update()
    {
        int songID = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 2f));
        if (gameObject.GetComponent<AudioSource>().isPlaying == false)
        {
            gameObject.GetComponent<AudioSource>().clip = songs[songID];
            gameObject.GetComponent<AudioSource>().PlayOneShot(songs[songID]);
        }
    }
	public AudioClip[] songs = new AudioClip[3];
}
