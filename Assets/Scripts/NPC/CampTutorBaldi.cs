using UnityEngine;

public class CampTutorBaldi : MonoBehaviour
{
    private void Start()
    {
        audDevice = GetComponent<AudioSource>();
        waitTime = 1f;
        anim.SetBool("talking", true);
    }
    private void Update()
    {
        if (!audDevice.isPlaying && waitTime >= 0f)
        {
            waitTime -= 1f * Time.deltaTime;
            anim.SetBool("talking", false);
        }
        if (!audDevice.isPlaying && waitTime <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
    public AudioSource audDevice;
    public float waitTime;
    public Animator anim;
}
