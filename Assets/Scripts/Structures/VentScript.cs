using UnityEngine;

public class VentScript : MonoBehaviour
{
    void Start()
    {
        ventAud = GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            PlayerController player = other.transform.GetComponent<PlayerController>();
            player.inVent = true;
            if (ventPartType == "enter")
            {
                player.height = 19;
                ventAud.PlayOneShot(ventEnterSound[0]);
                player.gameController.gameAudioDevice.PlayOneShot(ventEnterSound[1]);
                player.transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
		        UIPopupTextManagerWithMovement.Show("Struct_Vent_Enter", Color.white, transform, 1.5f, 0f);
            }
            if (ventPartType == "bend")
            {
                ventAud.PlayOneShot(ventMainSound[Mathf.RoundToInt(Random.Range(0.5f, 3.5f))]);
                player.ventTarget = transform.position + (transform.forward * 99999f);
		        UIPopupTextManagerWithMovement.Show("Struct_Vent_Bang", Color.white, transform, 1f, 0f);
            }
            if (ventPartType == "exit")
            {
                player.height = 4;
                ventAud.PlayOneShot(ventMainSound[Mathf.RoundToInt(Random.Range(0.5f, 3.5f))]);
                player.ventTarget = new Vector3(0f, 0f, 0f);
                player.transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
                player.spoopBaldiScript.Hear(player.transform.position, 78f);
		        UIPopupTextManagerWithMovement.Show("Door_Slam", Color.white, transform, 1f, 0f);
            }
        }
        if (other.transform.tag == "NPC")
        {
            BsodaNPCEffect targetNpc = other.transform.GetComponent<BsodaNPCEffect>();
            targetNpc.inVent = true;
            if (ventPartType == "enter")
            {
                ventAud.PlayOneShot(ventEnterSound[0]);
                ventAud.PlayOneShot(ventEnterSound[1]);
                targetNpc.transform.position = new Vector3(transform.position.x, 17f, transform.position.z);
		        UIPopupTextManagerWithMovement.Show("Struct_Vent_Enter", Color.white, transform, 1.5f, 0f);
            }
            if (ventPartType == "bend")
            {
                ventAud.PlayOneShot(ventMainSound[Mathf.RoundToInt(Random.Range(0.5f, 3.5f))]);
                targetNpc.ventTarget = transform.position + (transform.forward * 99999f);
		        UIPopupTextManagerWithMovement.Show("Struct_Vent_Bang", Color.white, transform, 1f, 0f);
            }
            if (ventPartType == "exit")
            {
                ventAud.PlayOneShot(ventMainSound[Mathf.RoundToInt(Random.Range(0.5f, 3.5f))]);
                targetNpc.ventTarget = new Vector3(0f, 0f, 0f);
                targetNpc.transform.position = new Vector3(transform.position.x, targetNpc.spawnPos.y, transform.position.z);
		        UIPopupTextManagerWithMovement.Show("Door_Slam", Color.white, transform, 1f, 0f);
            }
        }
    }
    public string ventPartType;
    private AudioSource ventAud;
    public AudioClip[] ventEnterSound;
    public AudioClip[] ventMainSound;
}
