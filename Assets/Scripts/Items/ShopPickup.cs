using UnityEngine;

public class ShopPickup : MonoBehaviour
{
	private void Update()
	{
        player = FindFirstObjectByType<PlayerController>().transform;
		if (PlayerPrefs.GetInt("ItemRestriction") == 1) Destroy(gameObject);
        interact = Input.GetButtonDown("Interact");
		if (interact == true && Time.timeScale != 0f)
		{
			Ray ray = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit))
			{                                                                                                              // This part is an absolute mess, but I can't find a way
                if (raycastHit.transform.name == "Bsoda" && Vector3.Distance(player.position, transform.position) < 10f)  // to automate it and make it more organized, so it'll
                {                                                                                                        // just be like this. I'm So Good At Programming
			        ShopCollectItem(1, 500);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Zesty Bar" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(2, 300);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Door Lock" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(3, 500);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Quarter" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(4, 250);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Tape" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(5, 400);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "PKeys" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(6, 200);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Scissors" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(7, 300);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "NoSquee" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(8, 250);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Clock" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(9, 300);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Boots" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(10, 200);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Teleporter" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(11, 1000);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Grapple 5" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(12, 750);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Apple" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(13, 1000);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Chalk" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(14, 300);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Whistle" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(15, 250);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Nametag" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(16, 400);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Portal Poster" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(17, 750);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Nana Peel" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(18, 400);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Diet Bsoda" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(19, 300);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Bus Pass" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(20, 500);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Invis Elixir" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(21, 750);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Hammer" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(22, 500);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Plunger" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(23, 250);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Bomb" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(24, 500);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Firewood" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(25, 50);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Coal" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(26, 50);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Gasoline" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(27, 50);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Circle Key" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(28, 50);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Triangle Key" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(29, 50);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Square Key" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(30, 50);
                    gc.YTPGain(-50);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Star Key" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(31, 50);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Heart Key" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(32, 50);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Weird Key" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(33, 50);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Grapple 4" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(34, 650);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Grapple 3" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(35, 500);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Grapple 2" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(36, 350);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Grapple 1" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(37, 250);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
                else if (raycastHit.transform.name == "Glove" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        ShopCollectItem(38, 500);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                }
            }
		}
	}
    public void ShopCollectItem(int itemId, int ytpCost)
    {
        if (gc.midGameYTPs >= ytpCost)
        {   
            if (isBonusItem)
            {
                gameObject.SetActive(false);
                gc.YTPGain(-ytpCost);
                johnnyAudio.Stop();
                johnnyAudio.PlayOneShot(boughtSFX[Mathf.RoundToInt(Random.Range(0, 6))]);
                if (PlayerPrefs.GetInt("BonusItemSlot1") == 0)
                {
                    PlayerPrefs.SetInt("BonusItemSlot1", itemId);
                }
                else if (PlayerPrefs.GetInt("BonusItemSlot2") == 0)
                {
                    PlayerPrefs.SetInt("BonusItemSlot2", itemId);
                }
                else if (PlayerPrefs.GetInt("BonusItemSlot3") == 0)
                {
                    PlayerPrefs.SetInt("BonusItemSlot3", itemId);
                }
                else if (PlayerPrefs.GetInt("BonusItemSlot4") == 0)
                {
                    PlayerPrefs.SetInt("BonusItemSlot4", itemId);
                }
                else
                {
                    PlayerPrefs.SetInt("BonusItemSlot5", itemId);
                }
            }
            else
            {
                gameObject.SetActive(false);
                gc.YTPGain(-ytpCost);
                johnnyAudio.Stop();
                johnnyAudio.PlayOneShot(boughtSFX[Mathf.RoundToInt(Random.Range(0, 6))]);
                player.GetComponent<PlayerController>().CollectItem(itemId);
            }
        }
        else if (gc.midGameYTPs < ytpCost)
        {
            johnnyAudio.Stop();
            johnnyAudio.PlayOneShot(expensiveSFX[Mathf.RoundToInt(Random.Range(0, 3))]);
        }
    }
    public AudioSource johnnyAudio;
	public GameController gc;
    public AudioClip itemClickAudio;
    public AudioClip[] expensiveSFX = new AudioClip[3];
    public AudioClip[] boughtSFX = new AudioClip[6];
    public bool interact;
	public Transform player;
    public bool isBonusItem;
}