using UnityEngine;
using System.Collections;

public class BusterBalloon : MonoBehaviour
{
    public void Start()
    {
        origin = transform.position;
        poppingTime = UnityEngine.Random.Range(0.5f, 3f);
        balloonSprite = GetComponent<SpriteRenderer>();
        balloonSprite.sprite = balloonSprites[Mathf.RoundToInt(UnityEngine.Random.Range(0f, 7f))];
		StartCoroutine(LateStart());
	}
	IEnumerator LateStart()
	{
		yield return null;
		playerScript = FindFirstObjectByType<PlayerController>();
	}
    public void Update()
    {
        gameObject.layer = 7;
		if (poppingTime > 0f && balloonBuster.answered)
		{
			poppingTime -= Time.deltaTime;
		}
        if (poppingTime <= 0f && balloonBuster.answered)
        {
            PopBalloon();
        }
    }
    public void PopBalloon()
    {
        balloonBuster.audioDevice.PlayOneShot(popSound);
        Destroy(gameObject);
    }
    private void OnTriggerExit(Collider other)
	{
        if (other.transform.name == "BalloonBoundary") transform.position = origin;
    }
    public float poppingTime;
    public SpriteRenderer balloonSprite;
    public Sprite[] balloonSprites;
    public PlayerController playerScript;
    public BalloonBuster balloonBuster;
    public AudioClip popSound;
	public Vector3 origin;
}
