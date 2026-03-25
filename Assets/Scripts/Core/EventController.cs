using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventController : MonoBehaviour
{
    private void Start()
    {
        currentEventGap = Random.Range(minEventGap, maxEventGap);
        mysteryDoor.SetActive(false);
        mysteryWall.SetActive(true);
    }
    private void Update()
    {
        if (startEventWait >= 0f && gameController.spoopMode)
        {
            startEventWait -= Time.deltaTime;
        }
        if (currentEventGap >= 0f && startEventWait <= 0f && currentEventDuration <= 0f)
        {
            currentEventGap -= Time.deltaTime;
        }
        if (currentEventDuration >= 0f && gameController.spoopMode)
        {
            currentEventDuration -= Time.deltaTime;
        }
        if (eventID == -1 && currentEventGap <= 1f)
        {
            eventID = Mathf.RoundToInt(UnityEngine.Random.Range(0.5f, 2.5f));
            while (!possibleEvents[eventID]) eventID = Mathf.RoundToInt(UnityEngine.Random.Range(0.5f, 2.5f));
        }
        if (startEventWait <= 0f && currentEventGap <= 0f)
        {
            StartEvent();
        }
        if (currentEventDuration <= 0f && eventID != -1)
        {
            EndEvent();
        }
    }
    public void StartEvent()
    {
        currentEventGap = Random.Range(minEventGap, maxEventGap);
        if (currentEventDuration <= 0f)
        {
            currentEventDuration = Random.Range(minEventDuration, maxEventDuration);
            if (possibleEvents[Mathf.RoundToInt(eventID)])
            {
                if (eventID == 0)
                {
                    // eventAudio.PlayOneShot(eventAnnouncementSound);
		            if (RenderSettings.fog == false)
                    {
                        RenderSettings.fog = true;
                    }
                    StartCoroutine(Fog());
                    eventAudio.PlayOneShot(baldiTvVoices[0]);
                    eventAudio.PlayOneShot(fogMusic);
                }
                else if (eventID == 2)
                {
                    // eventAudio.PlayOneShot(eventAnnouncementSound);
                    mysteryDoor.SetActive(true);
                    mysteryWall.SetActive(false);
                    eventAudio.PlayOneShot(baldiTvVoices[2]);
                }
                else if (eventID == 3)
                {
                    // eventAudio.PlayOneShot(eventAnnouncementSound);
                    gameController.spoopBaldiScript.RulerSnap();
                    eventAudio.PlayOneShot(baldiTvVoices[3]);
                }
            }
        }
    }
    public void EndEvent()
    {
        // Note that this script will reset things related to EVERY event, so all event effects will be stopped.
        // For example, even if the event is fog, Baldi will regain his ability to kill.
        if (currentEventDuration >= 0f)
        {
            currentEventDuration = 0.05f;
        }
        RenderSettings.fogDensity = 0f;
        RenderSettings.fog = false;
        if (gameController.spoopBaldiScript.isActiveAndEnabled) gameController.spoopBaldiScript.killScript.killerActive = true;
        mysteryDoor.SetActive(false);
        mysteryWall.SetActive(true);
        eventID = -1;
    }
    private IEnumerator Fog()
	{
        while (currentEventDuration > 1f)
		{
            if (RenderSettings.fogDensity < 0.05f) RenderSettings.fogDensity = RenderSettings.fogDensity + 0.001f;
			yield return null;
		}
        while (currentEventDuration > 0f && currentEventDuration < 3f)
		{
            if (RenderSettings.fogDensity >= 0f) RenderSettings.fogDensity = RenderSettings.fogDensity - 0.001f;
			yield return null;
		}
        EndEvent();
		yield break;
    }
    public void BaldiTV(AudioClip currentVoiceline)
	{
        // Unused for the time being.
		tvScreenAnimator.SetTrigger("StartJingle");
        float transparency = 0f;
        while (transparency < 1f)
		{
            transparency = transparency + 0.01f;
		}
    }
    // IDs:
    // 0-Fog, 1-Party, 2-Mystery Room, 3-Ruler Break, 4-Gravity Chaos
    // 5-Flood, 6-Test Procedure, 7-Balder Dash, 8-Student Shuffle
    // Written since they aren't found anywhere else, unlike for items.
    public GameController gameController;
    public int eventID;
    public float startEventWait;
    public float minEventGap;
    public float maxEventGap;
    public float currentEventGap;
    public float minEventDuration;
    public float maxEventDuration;
    public float currentEventDuration;
    public Image eventAnnounceTV;
    public Image eventAnnounceScreen;
    public bool[] possibleEvents = new bool[9];
    public GameObject mysteryDoor;
    public GameObject mysteryWall;
    public AudioSource eventAudio;
	public AudioClip[] baldiTvVoices = new AudioClip[9];
    public AudioClip fogMusic;
    public Animator tvScreenAnimator;
    public AudioClip eventAnnouncementSound;
}
