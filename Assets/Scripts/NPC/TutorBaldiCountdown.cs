using UnityEngine;

public class TutorBaldiCountdown : MonoBehaviour
{
    public void Start()
    {
        counting = false;
		currentNumber = 10;
		if (tutorAudioSource.clip != null)
		{
			UIPopupTextManagerWithMovement.Show(spawnCaption, Color.green, transform, tutorAudioSource.clip.length, 0.25f);
		}
    }
    public void Update()
    {
		if (hideNSeekMode)
		{
			if (timeUntilNextVoice > 0f && counting) timeUntilNextVoice -= Time.deltaTime;
			if (countTimer > 0f && counting) countTimer -= Time.deltaTime;
			else if (counting)
			{
				counting = false;
				gameController.BeginSpoopMode();
			}
			if (counting)
			{
				if (countTimer >= 21f) currentNumber = 10;
				else if (countTimer >= 19f) currentNumber = 9;
				else if (countTimer >= 17f) currentNumber = 8;
				else if (countTimer >= 15f) currentNumber = 7;
				else if (countTimer >= 13f) currentNumber = 6;
				else if (countTimer >= 11f) currentNumber = 5;
				else if (countTimer >= 9f) currentNumber = 4;
				else if (countTimer >= 7f) currentNumber = 3;
				else if (countTimer >= 5f) currentNumber = 2;
				else if (countTimer >= 3f) currentNumber = 1;
				else currentNumber = 0;
				if (currentNumber != 0 && !tutorAudioSource.isPlaying && timeUntilNextVoice <= 0f)
				{
					tutorAudioSource.PlayOneShot(countingNumbers[currentNumber - 1]);
					timeUntilNextVoice = 2f;
					UIPopupTextManagerWithMovement.Show("BAL_Num" + (currentNumber).ToString(), Color.green, transform, 1f, 0f);
				}
				else if (!tutorAudioSource.isPlaying && timeUntilNextVoice <= 0f)
				{
					tutorAudioSource.PlayOneShot(readyOrNot);
					UIPopupTextManagerWithMovement.Show("BAL_ReadyOrNot", Color.green, transform, 3f, 0f);
				}
			}
		}
    }
	private void OnTriggerExit(Collider other)
	{
		if (other.transform.name == "Player" && !counting && hideNSeekMode)
		{
			tutorAudioSource.Stop();
            countTimer = 23f;
            tutorBaldiAnimator.SetTrigger("counting");
            counting = true;
		}
    }
    public bool hideNSeekMode = false;
    public bool counting;
    public float countTimer;
    public float timeUntilNextVoice;
    public string spawnCaption = "BAL_WelcomeSchool";
    public Animator tutorBaldiAnimator;
	public AudioSource tutorAudioSource;
	public int currentNumber;
	public AudioClip readyOrNot;
    public AudioClip[] countingNumbers = new AudioClip[10];
	public GameController gameController;
}
