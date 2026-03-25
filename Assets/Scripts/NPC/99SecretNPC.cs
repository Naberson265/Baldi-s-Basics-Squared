using UnityEngine;
using System.Collections;

public class NineNineSecretNPC : MonoBehaviour
{
	private void Start()
	{
		audioDevice = GetComponent<AudioSource>();
	}
	private void Update()
	{
		if (forceShowTime > 0f)
		{
			forceShowTime -= Time.deltaTime;
		}
		if (gettingAngry)
		{
			anger += Time.deltaTime;
			if (anger >= requiredlookTime & !angry)
			{
				angry = true;
			}
		}
		else if (anger > 0f)
		{
			anger -= Time.deltaTime;
		}
		if (angry && !triggered)
		{
			triggered = true;
			audioDevice.Play();
			StartCoroutine(SuddenScare());
		}
	}
	public IEnumerator SuddenScare()
	{
		cc.enabled = false;
		float flickerTime = 0.5f;
		Color pastAmbientColor = RenderSettings.ambientLight;
		while (flickerTime > 0f)
		{
			flickerTime -= Time.deltaTime;
			spriteImage.sprite = flickerSprite;
			RenderSettings.ambientLight = new Color(1f,0f,0f);
			yield return null;
		}
		RenderSettings.ambientLight = pastAmbientColor;
		cc.enabled = true;
		if (baldi.isActiveAndEnabled)
		{
			baldi.Hear(transform.position, 64f);
		}
		Destroy(gameObject);
		yield break;
	}
	private void FixedUpdate()
	{
		Vector3 direction = player.position - transform.position;
		RaycastHit raycastHit;
		if (Physics.Raycast(transform.position, direction, out raycastHit, Mathf.Infinity, rayLayerMask, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player" & craftersRenderer.isVisible & sprite.activeSelf)
		{
			gettingAngry = true;
		}
		else
		{
			gettingAngry = false;
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && !triggered)
		{
			triggered = true;
			audioDevice.Play();
			StartCoroutine(SuddenScare());
		}
	}
	public bool db;
	public bool angry;
	public bool gettingAngry;
	public bool triggered;
	public float anger;
	private float forceShowTime;
	public float requiredlookTime;
	public Transform player;
	public CharacterController cc;
	public Transform playerCamera;
	public GameObject sprite;
	public LayerMask rayLayerMask;
	public Renderer craftersRenderer;
	public SpoopBaldi baldi;
	public SpriteRenderer spriteImage;
	public Sprite flickerSprite;
	public Sprite normalSprite;
	private AudioSource audioDevice;
	public AudioClip aud_Intro;
}
