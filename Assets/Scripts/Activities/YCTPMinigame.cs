using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class YCTPMinigame : MonoBehaviour
{
    private void Start()
    {
        gameController.ActivateLearningGame();
        if (gameController.notebooks == 1)
        {
            QueueAudio(bal_intro);
            QueueAudio(bal_howto);
        }
        NewProblem();
        if (gameController.spoopMode)
        {
            baldiFeedTransform.position = new Vector3(-1000f, -1000f, 0f);
        }
    }
    private void Update()
    {
        if (!baldiAudio.isPlaying)
        {
            if (audioInQueue > 0 & !gameController.spoopMode)
            {
                PlayQueue();
            }
            baldiFeed.SetBool("talking", false);
        }
        else
        {
            baldiFeed.SetBool("talking", true);
        }
        if ((Input.GetKeyDown("return") || Input.GetKeyDown("enter")) & questionInProgress)
        {
            questionInProgress = false;
            CheckAnswer();
        }
        if (problem > 3)
        {
            endDelay -= 1f * Time.unscaledDeltaTime;
            if (endDelay <= 0f)
            {
                ExitGame();
            }
        }
    }
    private void NewProblem()
    {
        playerAnswer.text = string.Empty;
        problem++;
        playerAnswer.ActivateInputField();
        if (problem <= 3)
        {
            QueueAudio(bal_problems[problem - 1]);
            if (((gameController.gameMode == "classic" || gameController.gameMode == "plushidenseek") & (problem <= 2 || gameController.notebooks <= 1)) || (gameController.gameMode == "endless" & (problem <= 2 || gameController.notebooks != 2)))
            {
                num1 = (float)Mathf.RoundToInt(UnityEngine.Random.Range(0f, 9f));
                num2 = (float)Mathf.RoundToInt(UnityEngine.Random.Range(0f, 9f));
                sign = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
                QueueAudio(bal_numbers[Mathf.RoundToInt(num1)]);
                if (sign == 0)
                {
                    solution = num1 + num2;
                    questionText.text = string.Concat(new object[]
                    {
                        "SOLVE MATH Q",
                        problem,
                        ": \n \n",
                        num1,
                        "+",
                        num2,
                        "="
                    });
                    QueueAudio(bal_plus);
                }
                else if (sign == 1)
                {
                    solution = num1 - num2;
                    questionText.text = string.Concat(new object[]
                    {
                        "SOLVE MATH Q",
                        problem,
                        ": \n \n",
                        num1,
                        "-",
                        num2,
                        "="
                    });
                    QueueAudio(bal_minus);
                }
                QueueAudio(bal_numbers[Mathf.RoundToInt(num2)]);
                QueueAudio(bal_equals);
            }
            else
            {
                impossibleMode = true;
                num1 = UnityEngine.Random.Range(1f, 9999f);
                num2 = UnityEngine.Random.Range(1f, 9999f);
                num3 = UnityEngine.Random.Range(1f, 9999f);
                sign = Mathf.RoundToInt((float)UnityEngine.Random.Range(0, 1));
                QueueAudio(bal_screech);
                if (sign == 0)
                {
                    questionText.text = string.Concat(new object[]
                    {
                        "SOLVE MATH Q",
                        problem,
                        ": \n",
                        num1,
                        "+(",
                        num2,
                        "X",
                        num3,
                        "="
                    });
                    QueueAudio(bal_plus);
                    QueueAudio(bal_screech);
                    QueueAudio(bal_times);
                    QueueAudio(bal_screech);
                }
                else if (sign == 1)
                {
                    questionText.text = string.Concat(new object[]
                    {
                        "SOLVE MATH Q",
                        problem,
                        ": \n (",
                        num1,
                        "/",
                        num2,
                        ")+",
                        num3,
                        "="
                    });
                    QueueAudio(bal_divided);
                    QueueAudio(bal_screech);
                    QueueAudio(bal_plus);
                    QueueAudio(bal_screech);
                }
                num1 = UnityEngine.Random.Range(1f, 9999f);
                num2 = UnityEngine.Random.Range(1f, 9999f);
                num3 = UnityEngine.Random.Range(1f, 9999f);
                sign = Mathf.RoundToInt((float)UnityEngine.Random.Range(0, 1));
                if (sign == 0)
                {
                    questionText2.text = string.Concat(new object[]
                    {
                        "SOLVE MATH Q",
                        problem,
                        ": \n",
                        num1,
                        "+(",
                        num2,
                        "X",
                        num3,
                        "="
                    });
                }
                else if (sign == 1)
                {
                    questionText2.text = string.Concat(new object[]
                    {
                        "SOLVE MATH Q",
                        problem,
                        ": \n (",
                        num1,
                        "/",
                        num2,
                        ")+",
                        num3,
                        "="
                    });
                }
                num1 = UnityEngine.Random.Range(1f, 9999f);
                num2 = UnityEngine.Random.Range(1f, 9999f);
                num3 = UnityEngine.Random.Range(1f, 9999f);
                sign = Mathf.RoundToInt((float)UnityEngine.Random.Range(0, 1));
                if (sign == 0)
                {
                    questionText3.text = string.Concat(new object[]
                    {
                        "SOLVE MATH Q",
                        problem,
                        ": \n",
                        num1,
                        "+(",
                        num2,
                        "X",
                        num3,
                        "="
                    });
                }
                else if (sign == 1)
                {
                    questionText3.text = string.Concat(new object[]
                    {
                        "SOLVE MATH Q",
                        problem,
                        ": \n (",
                        num1,
                        "/",
                        num2,
                        ")+",
                        num3,
                        "="
                    });
                }
                QueueAudio(bal_equals);
            }
            questionInProgress = true;
        }
        else
        {
            if (!gameController.spoopMode)
            {
                questionText.text = "WOW! YOU EXIST!";
                endDelay = 5f;
            }
            else if (gameController.gameMode == "endless" & problemsWrong <= 0)
            {
                int num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
                questionText.text = endlessHintText[num];
                if (gameController.notebooks >= 3 || gameController.failedNotebooks >= 2)
                {
                    endDelay = 1.5f;
                }
                else
                {
                    endDelay = 5f;
                }
            }
            else if ((gameController.gameMode == "classic" || gameController.gameMode == "plushidenseek") & problemsWrong >= 3)
            {
                questionText.text = "I HEAR MATH THAT BAD";
                questionText2.text = string.Empty;
                questionText3.text = string.Empty;
                if (spoopBaldiScript.isActiveAndEnabled) spoopBaldiScript.Hear(playerPosition, 79f);
                gameController.failedNotebooks++;
                if (gameController.failedNotebooks >= 1)
                {
                    endDelay = 1.5f;
                }
                else
                {
                    endDelay = 5f;
                }
            }
            else
            {
                int num2 = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
                questionText.text = hintText[num2];
                questionText2.text = string.Empty;
                questionText3.text = string.Empty;
                if (gameController.notebooks >= 3 || gameController.failedNotebooks >= 1)
                {
                    endDelay = 1.5f;
                }
                else
                {
                    endDelay = 5f;
                }
            }
        }
        if (!gameController.spoopMode && problemsWrong <= 0)
        {
            if (problem == 2)
            {
                gameController.YCTPTheme.clip = gameController.YCTP1Theme;
                gameController.YCTPTheme.Play();
            }
            else if (problem == 3)
            {
                gameController.YCTPTheme.clip = gameController.YCTP2Theme;
                gameController.YCTPTheme.Play();
            }
        }
        else
        {
            gameController.YCTPTheme.Stop();
        }
    }
    public void OKButton()
    {
        CheckAnswer();
    }
    public void CheckAnswer()
    {
        //if (playerAnswer.text == "31718")
        //{
        //    StartCoroutine(CheatText("Template"));
        //}
        if (problem <= 3)
        {
            if (playerAnswer.text == solution.ToString() & !impossibleMode)
            {
                results[problem - 1].texture = correct;
                baldiAudio.Stop();
                gameController.YTPGain(10);
                ClearAudioQueue();
                int num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 5f));
                QueueAudio(bal_praises[num]);
                NewProblem();
            }
            else
            {
                problemsWrong++;
                results[problem - 1].texture = incorrect;
                if (!gameController.spoopMode)
                {
                    baldiAudio.Stop();
                    baldiFeed.SetTrigger("angered");
                    baldiAudio.PlayOneShot(hangSfx);
                    gameController.BeginSpoopMode();
                }
                if (gameController.gameMode == "classic")
                {
                    if (problem == 3)
                    {
                        spoopBaldiScript.GetAngry(1f);
                    }
                    else
                    {
                        spoopBaldiScript.GetTempAngry(0.15f);
                    }
                    baldiAudio.Stop();
                }
                else
                {
                    spoopBaldiScript.GetAngry(1f);
                    baldiAudio.Stop();
                }
                ClearAudioQueue();
                NewProblem();
            }
        }
    }
    private void QueueAudio(AudioClip sound)
    {
        audioQueue[audioInQueue] = sound;
        audioInQueue++;
    }
    private void PlayQueue()
    {
        baldiAudio.PlayOneShot(audioQueue[0]);
        UnqueueAudio();
    }
    private void UnqueueAudio()
    {
        for (int i = 1; i < audioInQueue; i++)
        {
            audioQueue[i - 1] = audioQueue[i];
        }
        audioInQueue--;
    }
    private void ClearAudioQueue()
    {
        audioInQueue = 0;
    }
    private void ExitGame()
    {
        if (problemsWrong <= 0 & gameController.gameMode == "endless")
        {
            spoopBaldiScript.GetAngry(-1f);
        }
        gameController.DeactivateLearningGame(gameObject);
    }
    public void ButtonPress(int value)
    {
        if (value >= 0 & value <= 9)
        {
            playerAnswer.text = playerAnswer.text + value;
        }
        else if (value == -1)
        {
            playerAnswer.text = playerAnswer.text + "-";
        }
        else
        {
            playerAnswer.text = string.Empty;
        }
    }
    private IEnumerator CheatText(string text)
    {
        for (; ; )
        {
            questionText.text = text;
            questionText2.text = string.Empty;
            questionText3.text = string.Empty;
            yield return new WaitForEndOfFrame();
        }
    }
    public GameController gameController;
    public SpoopBaldi spoopBaldiScript;
    public Vector3 playerPosition;
    public GameObject mathGame;
    public RawImage[] results = new RawImage[3];
    public Texture correct;
    public Texture incorrect;
    public TMP_InputField playerAnswer;
    public TMP_Text questionText;
    public TMP_Text questionText2;
    public TMP_Text questionText3;
    public Animator baldiFeed;
    public Transform baldiFeedTransform;
    public AudioClip bal_plus;
    public AudioClip bal_minus;
    public AudioClip bal_times;
    public AudioClip bal_divided;
    public AudioClip bal_equals;
    public AudioClip bal_howto;
    public AudioClip bal_intro;
    public AudioClip bal_screech;
    public AudioClip[] bal_numbers = new AudioClip[10];
    public AudioClip[] bal_praises = new AudioClip[6];
    public AudioClip[] bal_problems = new AudioClip[3];
    public AudioClip hangSfx;
    public float endDelay;
    public int problem;
    public int audioInQueue;
    public float num1;
    public float num2;
    public float num3;
    public int sign;
    public float solution;
    public string[] hintText = new string[]
    {
        "I GET ANGRIER FOR EVERY PROBLEM YOU GET WRONG",
        "I HEAR EVERY DOOR YOU OPEN"
    };
    public string[] endlessHintText = new string[]
    {
        "That's more like it...",
        "Keep up the good work or see me after class..."
    };
    public bool questionInProgress;
    public bool impossibleMode;
    public int problemsWrong;
    public AudioClip[] audioQueue = new AudioClip[20];
    public AudioSource baldiAudio;
}
