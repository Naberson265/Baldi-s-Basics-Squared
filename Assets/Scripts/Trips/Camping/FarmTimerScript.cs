using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FarmEndScript : MonoBehaviour
{
    private void Update()
    {
        if (timeUntilChase >= 0f)
        {
            timeUntilChase -= 1f * Time.deltaTime;
        }
        if (timeUntilChase < 0f && !spoopBaldi.isActiveAndEnabled)
        {
            music.Stop();
            music.clip = chaseMusic;
            music.Play();
            spoopBaldi.gameObject.SetActive(true);
        }
        timeUntilChaseText.text = Mathf.RoundToInt(timeUntilChase).ToString() + "s Left";
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            gc.YTPGain(Mathf.RoundToInt(timeUntilChase * 1.5f));
            gc.YTPGain(25);
            music.Stop();
            gc.FinalExit();
        }
    }
    public AudioSource music;
    public AudioClip chaseMusic;
    public SpoopBaldi spoopBaldi;
    public BasicPlayerScript ps;
    public BasicGameController gc;
    public float timeUntilChase;
    public TMP_Text timeUntilChaseText;
}
