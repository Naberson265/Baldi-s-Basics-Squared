using UnityEngine;
using TMPro;

public class MathMachine : MonoBehaviour
{
    private void Start()
    {
        completionReward.SetActive(false);
        audioDevice = gameObject.GetComponent<AudioSource>();
        bonusQuestion = false;
        SetupActivity();
    }
    public void SetupActivity()
    {
        answered = false;
        qOperator = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
        // First number to be set will be the one that makes it easiest to create an answer from 0-9.
        if (qOperator == 1) qNum1 = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 9f));
        else if (qOperator == 0) qNum2 = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 9f));
        if (qOperator == 1) qNum2 = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 9f - qNum1));
        else if (qOperator == 0) qNum1 = Mathf.RoundToInt(UnityEngine.Random.Range(qNum2, 9f));
        if (qOperator == 1) qAnswer = qNum1 + qNum2;
        else if (qOperator == 0) qAnswer = qNum1 - qNum2;
    }
    private void Update()
    {
        displayNum1.text = qNum1.ToString();
        displayNum2.text = qNum2.ToString();
        if (qOperator == 1) displayOperator.text = "+";
        else if (qOperator == 0) displayOperator.text = "-";
        if (answered) displayAnswer.text = qAnswer.ToString();
        else displayAnswer.text = "?";
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
            if (playerScript.heldBalloon == qAnswer)
            {
                audioDevice.PlayOneShot(correctSound);
                if (!bonusQuestion) completionReward.SetActive(true);
                if (spoopBaldiScript.isActiveAndEnabled) spoopBaldiScript.ActivityPraise(6f);
                playerScript.stamina = 100f;
            }
            else
            {
                audioDevice.PlayOneShot(incorrectSound);
                if (gameController.spoopMode)
                {
                    if (spoopBaldiScript.isActiveAndEnabled) spoopBaldiScript.GetAngry(0.5f);
                    if (spoopBaldiScript.isActiveAndEnabled) spoopBaldiScript.GetTempAngry(0.5f);
                    if (spoopBaldiScript.isActiveAndEnabled) spoopBaldiScript.Hear(completionReward.transform.position, 126f);
                }
                if (transform.name == "NBMathMachine")
                {
                    if (!bonusQuestion) completionReward.SetActive(true);
                    gameController.failedNotebooks++;
                }
            }
            playerScript.heldBalloon = -1f;
            answered = true;
        }
    }
    public float qNum1;
    public int qOperator;
    public float qNum2;
    public float qAnswer;
    public bool answered;
    public bool interact;
    public bool bonusQuestion = false;
    public Transform player;
    public TMP_Text displayNum1;
    public TMP_Text displayOperator;
    public TMP_Text displayNum2;
    public TMP_Text displayAnswer;
    public GameObject completionReward;
	public GameController gameController;
	public SpoopBaldi spoopBaldiScript;
    public PlayerScript playerScript;
    public AudioSource audioDevice;
    public Material correctAnsMaterial;
    public Material incorrectAnsMaterial;
    public AudioClip correctSound;
    public AudioClip incorrectSound;
}
