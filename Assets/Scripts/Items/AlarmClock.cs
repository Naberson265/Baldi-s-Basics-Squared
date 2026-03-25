using UnityEngine;

public class AlarmClock : MonoBehaviour
{
    private void Start()
	{
		if (PlayerPrefs.GetInt("DietItemModifier") == 1) timeLeft = 15f;
		else timeLeft = 30f;
		lifeSpan = timeLeft + 5f;
	}
	private void Update()
	{
		if (timeLeft >= 0f)
		{
			timeLeft -= Time.deltaTime;
		}
		else if (!rang)
		{
			Alarm();
		}
		if (lifeSpan >= 0f)
		{
			lifeSpan -= Time.deltaTime;
		}
		else
		{
			Destroy(gameObject, 0f);
		}
	}
	private void Alarm()
	{
		rang = true;
		if (spoopBaldiScript.isActiveAndEnabled) 
        {
			if (PlayerPrefs.GetInt("DietItemModifier") == 1) spoopBaldiScript.Hear(transform.position, 64f);
			else spoopBaldiScript.Hear(transform.position, 112f);
        }
		audioDevice.clip = ring;
		audioDevice.loop = false;
		audioDevice.Play();
	}
	public float timeLeft;
	private float lifeSpan;
	private bool rang;
	public SpoopBaldi spoopBaldiScript;
	public AudioClip ring;
	public AudioSource audioDevice;
}
