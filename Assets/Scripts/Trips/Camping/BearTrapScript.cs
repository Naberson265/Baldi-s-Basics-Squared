using UnityEngine;

public class BearTrapScript : MonoBehaviour
{
    private void Start()
    {
        trapped = false;
    }
    private void Update()
    {
        if (trapTime >= 0f && trapped)
        {
            trapTime -= Time.deltaTime;
        }
        else if (trapTime < 0f && trapped)
        {
            trapped = false;
            player.frozen = false;
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            trapped = true;
            player.frozen = true;
            audioSource.PlayOneShot(trapSound);
        }
    }
    public AudioSource audioSource;
    public AudioClip trapSound;
    public float trapTime;
    public bool trapped;
    public BasicPlayerScript player;
}
