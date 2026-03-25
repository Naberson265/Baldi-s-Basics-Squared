using UnityEngine;

public class RandomFuelGenerator : MonoBehaviour
{
    private void Start()
    {
        FuelGen();
        timeUntilRespawn = timeBetweenRespawns;
    }
    private void Update()
    {
        if (timeUntilRespawn >= 0f)
        {
            timeUntilRespawn -= 1f * Time.deltaTime;
        }
        else if (timeUntilRespawn < 0f)
        {
            timeUntilRespawn = timeBetweenRespawns;
            DestroyAllCurrentFuel();
            FuelGen();
        }
    }
    public void DestroyAllCurrentFuel()
    {
        for (int remainingFuel = objectsHierarchy.transform.childCount - 1; remainingFuel >= 0; remainingFuel--)
        {
            Destroy(objectsHierarchy.transform.GetChild(remainingFuel).gameObject);
        }
    }
    public void FuelGen()
    {
        fuelAmount = Mathf.RoundToInt(UnityEngine.Random.Range(fuelMin, fuelMax));
        for (currentFuel = 0; currentFuel < fuelAmount; currentFuel++)
        {
            float fuelX = Random.Range(-fuelSpawnRange + transform.position.x, fuelSpawnRange + transform.position.x);
            float fuelZ = Random.Range(-fuelSpawnRange + transform.position.z, fuelSpawnRange + transform.position.z);
            int fuelType = Mathf.RoundToInt(UnityEngine.Random.Range(0, 3));
            Vector3 fuelPos = new Vector3(fuelX, 5f, fuelZ);
            GameObject newFuel = Instantiate(fuel[fuelType], fuelPos, Quaternion.identity);
            if (newFuel.transform.name == "Coal(Clone)") newFuel.transform.name = "Coal";
            if (newFuel.transform.name == "Firewood(Clone)") newFuel.transform.name = "Firewood";
            if (newFuel.transform.name == "Gasoline(Clone)") newFuel.transform.name = "Gasoline";
            newFuel.GetComponent<BasicItemPickup>().player = player;
            newFuel.GetComponent<BasicItemPickup>().gc = bgc;
            newFuel.transform.parent = objectsHierarchy.transform;
        }
    }
    public Transform player;
    public BasicGameController bgc;
    public float timeBetweenRespawns;
    public float timeUntilRespawn;
    public int currentFuel;
    public GameObject[] fuel = new GameObject[3];
    public GameObject objectsHierarchy;
    public int fuelMax;
    public int fuelMin;
    public int fuelAmount;
    public float fuelSpawnRange;
}
