using UnityEngine;

public class ItemPickup : MonoBehaviour
{
	private void Update()
	{
        // This script is mainly just here to hold necessary info like the ItemID.
		if (PlayerPrefs.GetInt("ItemRestriction") == 1) Destroy(gameObject);
	}
	public GameController gc;
    public AudioClip itemClickAudio;
    public AudioClip ytpGreenSound;
    public AudioClip ytpSilverSound;
    public AudioClip ytpGoldSound;
    public bool interact;
    public int itemID;
	public Transform player;
}