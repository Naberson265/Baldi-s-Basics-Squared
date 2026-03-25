using UnityEngine;

public class WindowScript : MonoBehaviour
{
	private void Start()
	{
		windowAudio = GetComponent<AudioSource>();
		barrier.enabled = true;
		windowBroken = false;
		inRenderer.material = intact;
		outRenderer.material = intact;
	}
	private void Update()
	{
	}
	public void BreakWindow(bool hear)
	{
		if (!windowBroken)
		{
			if (spoopBaldiScript.isActiveAndEnabled && hear)
			{
				spoopBaldiScript.Hear(transform.position, 120f);
			}
			windowAudio.PlayOneShot(windowBreak, 1f);
		}
		barrier.enabled = false;
		inRenderer.material = broken;
		outRenderer.material = broken;
		obstacle.SetActive(false);
		windowBroken = true;
	}
	public SpoopBaldi spoopBaldiScript;
	public MeshCollider barrier;
	public MeshCollider trigger;
	public GameObject obstacle;
	public MeshRenderer inRenderer;
	public MeshRenderer outRenderer;
	public AudioClip windowBreak;
	public Material intact;
	public Material broken;
	public bool windowBroken;
	private AudioSource windowAudio;
}