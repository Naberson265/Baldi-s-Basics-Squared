using UnityEngine;

public class Johnny : MonoBehaviour
{
    private void Start()
    {
        audSource = gameObject.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter()
    {
        audSource.Stop();
        audSource.PlayOneShot(welcome);
    }
    private void OnTriggerExit()
    {
        audSource.Stop();
        audSource.PlayOneShot(exit);
    }
    public AudioClip welcome;
    public AudioClip exit;
    private AudioSource audSource;
}
