using UnityEngine;

public class VendingScript : MonoBehaviour
{
    private void Start()
    {
        used = false;
    }
    private void Update()
    {
        if (used) front.material = usedMaterial;
        else front.material = unusedMaterial;
    }
    public void Dispense()
    {
        if (transform.name != "RandomVending") uses--;
        if (uses == 0) used = true;
        else used = false;
        GetComponent<AudioSource>().Play();
    }
    public bool used;
    public int returnedItem = 1;
    public int uses = 1;
    public MeshRenderer front;
    public Material usedMaterial;
	public Material unusedMaterial;
}
