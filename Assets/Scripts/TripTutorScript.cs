using UnityEngine;

public class TripTutorDialogueScript : MonoBehaviour
{
	private void Update()
	{
		if (!audioDevice.isPlaying & played)
		{
			played = false;
			animator.SetBool("talking", false);
		}
		else if (audioDevice.isPlaying)
		{
			animator.SetBool("talking", true);
			played = true;
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.name == "Player" & !played) audioDevice.Play();
	}
	public Animator animator;
	public AudioSource audioDevice;
	private bool played;
}