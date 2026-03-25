using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CampFireScript : MonoBehaviour
{
    private void Update()
    {
        if (remainingTime >= 0f && gc.gameMode != "campingAEndless")
        {
            remainingTime -= 1f * Time.deltaTime;
        }
        if (remainingTime < 0f)
        {
            remainingTime = 9999f;
            gc.YTPGain(Mathf.RoundToInt(fuelTime * 1.5f));
            gc.YTPGain(25);
            music.Stop();
            gc.FinalExit();
        }
        if (fuelTime >= 0f)
        {
            fuelTime -= 1f * Time.deltaTime;
        }
        if (fuelTime > 60f)
        {
            fuelTime = 60f;
        }
        if (fuelTime < 0f && !spoopBaldi.isActiveAndEnabled)
        {
            music.Stop();
            music.clip = chaseMusic;
            music.Play();
            spoopBaldi.gameObject.SetActive(true);
        }
        fuelTimeText.text = Mathf.RoundToInt(fuelTime).ToString() + "s Fuel";
        gameTimeText.text = Mathf.RoundToInt(remainingTime).ToString() + "s Left";
        Animation();
    }
    public void Animation()
    {
        // I'm So Good At Coding
        if (fuelTime > 50f)
        {
            campfireSpriteAnim.SetBool("campfire6", true);
            campfireSpriteAnim.SetBool("campfire5", false);
            campfireSpriteAnim.SetBool("campfire4", false);
            campfireSpriteAnim.SetBool("campfire3", false);
            campfireSpriteAnim.SetBool("campfire2", false);
            campfireSpriteAnim.SetBool("campfire1", false);
            campfireSpriteAnim.SetBool("campfire0", false);
        }
        if (fuelTime > 40f & fuelTime <= 50f)
        {
            campfireSpriteAnim.SetBool("campfire6", false);
            campfireSpriteAnim.SetBool("campfire5", true);
            campfireSpriteAnim.SetBool("campfire4", false);
            campfireSpriteAnim.SetBool("campfire3", false);
            campfireSpriteAnim.SetBool("campfire2", false);
            campfireSpriteAnim.SetBool("campfire1", false);
            campfireSpriteAnim.SetBool("campfire0", false);
        }
        if (fuelTime > 30f & fuelTime <= 40f)
        {
            campfireSpriteAnim.SetBool("campfire6", false);
            campfireSpriteAnim.SetBool("campfire5", false);
            campfireSpriteAnim.SetBool("campfire4", true);
            campfireSpriteAnim.SetBool("campfire3", false);
            campfireSpriteAnim.SetBool("campfire2", false);
            campfireSpriteAnim.SetBool("campfire1", false);
            campfireSpriteAnim.SetBool("campfire0", false);
        }
        if (fuelTime > 20f & fuelTime <= 30f)
        {
            campfireSpriteAnim.SetBool("campfire6", false);
            campfireSpriteAnim.SetBool("campfire5", false);
            campfireSpriteAnim.SetBool("campfire4", false);
            campfireSpriteAnim.SetBool("campfire3", true);
            campfireSpriteAnim.SetBool("campfire2", false);
            campfireSpriteAnim.SetBool("campfire1", false);
            campfireSpriteAnim.SetBool("campfire0", false);
        }
        if (fuelTime > 10f & fuelTime <= 20f)
        {
            campfireSpriteAnim.SetBool("campfire6", false);
            campfireSpriteAnim.SetBool("campfire5", false);
            campfireSpriteAnim.SetBool("campfire4", false);
            campfireSpriteAnim.SetBool("campfire3", false);
            campfireSpriteAnim.SetBool("campfire2", true);
            campfireSpriteAnim.SetBool("campfire1", false);
            campfireSpriteAnim.SetBool("campfire0", false);
        }
        if (fuelTime > 0f & fuelTime <= 10f)
        {
            campfireSpriteAnim.SetBool("campfire6", false);
            campfireSpriteAnim.SetBool("campfire5", false);
            campfireSpriteAnim.SetBool("campfire4", false);
            campfireSpriteAnim.SetBool("campfire3", false);
            campfireSpriteAnim.SetBool("campfire2", false);
            campfireSpriteAnim.SetBool("campfire1", true);
            campfireSpriteAnim.SetBool("campfire0", false);
        }
        if (fuelTime <= 0f)
        {
            campfireSpriteAnim.SetBool("campfire6", false);
            campfireSpriteAnim.SetBool("campfire5", false);
            campfireSpriteAnim.SetBool("campfire4", false);
            campfireSpriteAnim.SetBool("campfire3", false);
            campfireSpriteAnim.SetBool("campfire2", false);
            campfireSpriteAnim.SetBool("campfire1", false);
            campfireSpriteAnim.SetBool("campfire0", true);
        }
    }
    public void FuelFire(float fuelPower)
    {
        if (spoopBaldi.isActiveAndEnabled)
        {
            spoopBaldi.ActivityPraise(3f);
            spoopBaldi.GetAngry(-0.5f);
        }
        ps.stamina += fuelPower * 3;
        if (ps.stamina >= ps.staminaMax) ps.stamina = ps.staminaMax;
        fuelTime += fuelPower;
        fireAudio.PlayOneShot(fireFuelingSound);
        gc.YTPGain(Mathf.RoundToInt(fuelPower * 2));
    }
    public AudioSource music;
    public AudioClip chaseMusic;
    public SpoopBaldi spoopBaldi;
    public AudioSource fireAudio;
    public AudioClip fireFuelingSound;
    public BasicPlayerScript ps;
    public BasicGameController gc;
    public float fuelTime;
    public float remainingTime;
    public TMP_Text fuelTimeText;
    public TMP_Text gameTimeText;
    public Animator campfireSpriteAnim;
}
