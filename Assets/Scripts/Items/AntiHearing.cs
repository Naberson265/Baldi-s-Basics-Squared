using UnityEngine;

public class AntiHearing : MonoBehaviour
{
	private void Start()
	{
	}
	private void Update()
	{
		if (mainAudioDevice.isPlaying & Time.timeScale == 0f)
		{
			mainAudioDevice.Pause();
		}
		else if (Time.timeScale > 0f & spoopBaldi.antiHearingTime > 0f)
		{
			mainAudioDevice.UnPause();
		}
	}
	public void Play()
	{
		sprite.sprite = closedSprite;
		mainAudioDevice.Play();
		if (spoopBaldi.isActiveAndEnabled) spoopBaldi.ActivateAntiHearing(30f);
	}
	public Sprite closedSprite;
	public SpriteRenderer sprite;
	public SpoopBaldi spoopBaldi;
	public AudioSource mainAudioDevice;
}