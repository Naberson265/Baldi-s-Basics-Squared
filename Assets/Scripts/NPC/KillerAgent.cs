using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System.Collections;

public class KillerAgent : MonoBehaviour
{
    private void Awake()
	{
		killed = false;
        killerActive = true;
	}
    void Start()
    {
		StartCoroutine(LateStart());
	}
	IEnumerator LateStart()
	{
		yield return null;
		player = FindFirstObjectByType<PlayerController>();
	}
    private void Update()
	{
		if (killAnim > 0f)
        {
            killAnim -= 1f * Time.deltaTime;
        }
        if (killAnim <= 0f && killed)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("TitleScreen");
        }
	}
    private void OnTriggerStay(Collider other)
	{
        if (other.tag == "Player" && !killed && killerActive)
        {
            if (!acceptsApple) Kill();
            else if (basic)
            {
                if (basicGameController.item[0] != 13 && basicGameController.item[1] != 13 && basicGameController.item[2] != 13)
                {
                    Kill();
                }
                else
                {
                    spoopBaldiScript.GiveApple(12f);
                    if (basicGameController.item[0] == 13) basicGameController.DeleteSpecificItem(0);
                    else if (basicGameController.item[1] == 13) basicGameController.DeleteSpecificItem(1);
                    else if (basicGameController.item[2] == 13) basicGameController.DeleteSpecificItem(2);
                }
            }
            else if (!basic)
            {
                if (other.transform.gameObject.GetComponent<PlayerController>().item[0] != 13 && other.transform.gameObject.GetComponent<PlayerController>().item[1] != 13 && other.transform.gameObject.GetComponent<PlayerController>().item[2] != 13)
                {
                    Kill();
                }
                else
                {
                    spoopBaldiScript.GiveApple(12f);
                    if (other.transform.gameObject.GetComponent<PlayerController>().item[0] == 13) other.transform.gameObject.GetComponent<PlayerController>().DeleteSpecificItem(0);
                    else if (other.transform.gameObject.GetComponent<PlayerController>().item[1] == 13) other.transform.gameObject.GetComponent<PlayerController>().DeleteSpecificItem(1);
                    else if (other.transform.gameObject.GetComponent<PlayerController>().item[2] == 13) other.transform.gameObject.GetComponent<PlayerController>().DeleteSpecificItem(2);
                }
            }
        }
	}
    public void Kill()
    {
        Time.timeScale = 0f;
        killAnim = 1f;
        killed = true;
        agent.speed = 0f;
        audioSource.Play();
        if (!basic)
        {
            gameController.UnlockMouse();
            player.frozen = true;
            cameraScript.KillCam();
            player.runSpeed = 0f;
            player.walkSpeed = 0f;
            player.playerSpeed = 0f;
        }
        else if (basic)
        {
            basicGameController.UnlockMouse();
            basicPlayer.frozen = true;
            basicCameraScript.KillCam();
            basicPlayer.runSpeed = 0f;
            basicPlayer.walkSpeed = 0f;
            basicPlayer.playerSpeed = 0f;
        }
        if (PlayerPrefs.GetString("GameMode") == "endless")
        {
            if (PlayerPrefs.GetString("MapMode") == "trueclassic" && gameController.notebooks > PlayerPrefs.GetInt("ClassicNotebookHighScore"))
            {
                PlayerPrefs.SetInt("ClassicNotebookHighScore", gameController.notebooks);
            }
            else if (PlayerPrefs.GetString("MapMode") == "grandopening" && gameController.notebooks > PlayerPrefs.GetInt("GrandNotebookHighScore"))
            {
                PlayerPrefs.SetInt("GrandNotebookHighScore", gameController.notebooks);
            }
            else if (PlayerPrefs.GetString("MapMode") == "main" && gameController.notebooks > PlayerPrefs.GetInt("MainNotebookHighScore"))
            {
                PlayerPrefs.SetInt("MainNotebookHighScore", gameController.notebooks);
            }
            else if (PlayerPrefs.GetString("MapMode") == "fgpd" && gameController.notebooks > PlayerPrefs.GetInt("FGPDNotebookHighScore"))
            {
                PlayerPrefs.SetInt("FGPDNotebookHighScore", gameController.notebooks);
            }
            if (gameController.notebooks >= 10 && PlayerPrefs.GetInt("EndlessApprenticeAchievement") != 1)
            {
                PlayerPrefs.SetInt("EndlessApprenticeAchievement", 1);
                audioSource.PlayOneShot(achievementSFX);
            }
            if (gameController.notebooks >= 20 && PlayerPrefs.GetInt("EndlessProAchievement") != 1)
            {
                PlayerPrefs.SetInt("EndlessProAchievement", 1);
                audioSource.PlayOneShot(achievementSFX);
            }
            if (gameController.notebooks >= 50 && PlayerPrefs.GetInt("EndlessMasterAchievement") != 1)
            {
                PlayerPrefs.SetInt("EndlessMasterAchievement", 1);
                audioSource.PlayOneShot(achievementSFX);
            }
        }
    }
    public bool basic;
    public bool killed;
    public bool killerActive;
    public bool acceptsApple;
    public float killAnim;
    public AudioSource audioSource;
    public AudioClip achievementSFX;
    public NavMeshAgent agent;
    public Transform killerTransform;
    public SpoopBaldi spoopBaldiScript;
    // Non-basic.
    public CameraScript cameraScript;
    public PlayerController player;
    public GameController gameController;
    // Basic.
    public BasicCameraScript basicCameraScript;
    public BasicPlayerScript basicPlayer;
    public BasicGameController basicGameController;
}