using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class BalloonBuster : MonoBehaviour
{
    private void Start()
    {
        completionReward.SetActive(false);
        audioDevice = gameObject.GetComponent<AudioSource>();
        bonusQuestion = false;
        SetupActivity();
		StartCoroutine(LateStart());
	}
	IEnumerator LateStart()
	{
		yield return null;
		playerScript = FindFirstObjectByType<PlayerController>();
	}
    public void SetupActivity()
    {
        answered = false;
        balloons.Clear();
        balloonTargetAmount = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 12f));
        glowHologram.color = new Color(255f, 255f, 255f);
        int balloonsToInstantiate = Mathf.RoundToInt(UnityEngine.Random.Range(balloonTargetAmount, 16f));
        while (balloonsToInstantiate > 0)
        {
            GameObject newBalloon = Instantiate(balloonPrefab, balloonParent.transform.position, Quaternion.identity);
            newBalloon.transform.position = new Vector3(balloonParent.transform.position.x, 5f, balloonParent.transform.position.z);
            newBalloon.transform.parent = balloonParent.transform;
            newBalloon.GetComponent<BusterBalloon>().balloonBuster = this;
            newBalloon.GetComponent<BusterBalloon>().playerScript = playerScript;
            balloonsToInstantiate--;
        }
        foreach (Transform balloon in balloonParent.transform)
        {
            balloons.Add(balloon.gameObject);
        }
    }
    private void Update()
    {
        displayTargetNum.text = balloonTargetAmount.ToString();
        if (!answered)
        {
            displayResponse.text = "0";
            remainingBalloons = 0;
            foreach (GameObject balloon in balloons)
            {
                if (balloon != null) remainingBalloons++;
            }
            if (remainingBalloons < balloonTargetAmount) AnswerQuestion();
        }
        if (gameController.escapeSequence == true && !bonusQuestion)
        {
            bonusQuestion = true;
            SetupActivity();
        }
    }
    public void AnswerQuestion()
    {
        if (!answered)
        {
            displayResponse.text = remainingBalloons.ToString();
            if (remainingBalloons == balloonTargetAmount)
            {
                audioDevice.PlayOneShot(correctSound);
                completionReward.SetActive(true);
                if (spoopBaldiScript.isActiveAndEnabled) spoopBaldiScript.ActivityPraise(6f);
                if (transform.name == "ItemBalloonBuster") spoopBaldiScript.GetAngry(-0.25f);
                playerScript.stamina = 100f;
                glowHologram.color = new Color(0f, 255f, 0f);
            }
            else
            {
                audioDevice.PlayOneShot(incorrectSound);
                if (gameController.spoopMode)
                {
                    if (spoopBaldiScript.isActiveAndEnabled) spoopBaldiScript.GetAngry(0.5f);
                    if (spoopBaldiScript.isActiveAndEnabled) spoopBaldiScript.GetTempAngry(0.5f);
                    if (spoopBaldiScript.isActiveAndEnabled) spoopBaldiScript.Hear(completionReward.transform.position, 126f);
                    glowHologram.color = new Color(255f, 0f, 0f);
                }
                if (transform.name == "NBBalloonBuster")
                {
                    completionReward.SetActive(true);
                    gameController.failedNotebooks++;
                }
            }
            answered = true;
        }
    }
    public int balloonTargetAmount;
    public int remainingBalloons;
    public bool answered;
    public bool bonusQuestion;
    public TMP_Text displayTargetNum;
    public TMP_Text displayResponse;
    public GameObject completionReward;
    public GameObject balloonPrefab;
    public GameObject balloonParent;
    public SpriteRenderer glowHologram;
    public List<GameObject> balloons;
	public GameController gameController;
	public SpoopBaldi spoopBaldiScript;
    public AudioSource audioDevice;
    public AudioClip correctSound;
    public AudioClip incorrectSound;
    public PlayerController playerScript;
}
