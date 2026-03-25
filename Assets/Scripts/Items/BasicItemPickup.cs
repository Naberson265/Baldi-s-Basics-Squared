using UnityEngine;

public class BasicItemPickup : MonoBehaviour
{
	private void Update()
	{
        interact = Input.GetButtonDown("Interact");
		if (interact == true && Time.timeScale != 0f)
		{
			Ray ray = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit))
			{                                                                                                              // This part is an absolute mess, but I can't find a way
                if (raycastHit.transform.name == "Bsoda" && Vector3.Distance(player.position, transform.position) < 10f)  // to automate it and make it more organized, so it'll
                {                                                                                                        // just be like this. I'm So Good At Programming
			        gc.CollectItem(1);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Zesty Bar" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(2);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Door Lock" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(3);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Quarter" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(4);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Tape" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(5);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "PKeys" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(6);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Scissors" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(7);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "NoSquee" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(8);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Clock" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(9);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Boots" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(10);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Teleporter" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(11);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Grapple 5" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(12);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Apple" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(13);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Chalk" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(14);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Whistle" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(15);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Nametag" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(16);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Portal Poster" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(17);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Nana Peel" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(18);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Diet Bsoda" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(19);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Bus Pass" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(20);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Invis Elixir" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(21);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Hammer" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(22);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Plunger" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(23);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Bomb" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(24);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Firewood" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(25);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Coal" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(26);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Gasoline" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(27);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Circle Key" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(28);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Triangle Key" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(29);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Square Key" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(30);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Star Key" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(31);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Heart Key" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(32);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Weird Key" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(33);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Grapple 4" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(34);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Grapple 3" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(35);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Grapple 2" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(36);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Grapple 1" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(37);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "Glove" && Vector3.Distance(player.position, transform.position) < 10f)
                {
			        gc.CollectItem(38);
                    gc.gameAudioDevice.PlayOneShot(itemClickAudio);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "YTP Green" && Vector3.Distance(player.position, transform.position) < 10f)
                {
				    gc.YTPGain(25);
                    gc.gameAudioDevice.PlayOneShot(ytpGreenSound);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "YTP Silver" && Vector3.Distance(player.position, transform.position) < 10f)
                {
				    gc.YTPGain(50);
                    gc.gameAudioDevice.PlayOneShot(ytpSilverSound);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "YTP Gold" && Vector3.Distance(player.position, transform.position) < 10f)
                {
				    gc.YTPGain(100);
                    gc.gameAudioDevice.PlayOneShot(ytpGoldSound);
                    raycastHit.transform.gameObject.SetActive(false);
                }
                else if (raycastHit.transform.name == "YTP Diamond" && Vector3.Distance(player.position, transform.position) < 10f)
                {
				    gc.YTPGain(250);
                    gc.gameAudioDevice.PlayOneShot(ytpGoldSound);
                    raycastHit.transform.gameObject.SetActive(false);
                }
            }
		}
	}
	public BasicGameController gc;
    public AudioClip itemClickAudio;
    public AudioClip ytpGreenSound;
    public AudioClip ytpSilverSound;
    public AudioClip ytpGoldSound;
    public bool interact;
	public Transform player;
}