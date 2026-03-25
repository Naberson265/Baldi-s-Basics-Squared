using UnityEngine;

public class RandomTrapGenerator : MonoBehaviour
{
    private void Update()
    {
        if (timeUntilRespawn >= 0f)
        {
            timeUntilRespawn -= 1f * Time.deltaTime;
        }
        else if (timeUntilRespawn < 0f)
        {
            timeUntilRespawn = timeBetweenRespawns;
            DestroyAllCurrentTraps();
            TrapGen();
        }
    }
    public void DestroyAllCurrentTraps()
    {
        for (int remainingTrap = objectsHierarchy.transform.childCount - 1; remainingTrap >= 0; remainingTrap--)
        {
            Destroy(objectsHierarchy.transform.GetChild(remainingTrap).gameObject);
        }
    }
    public void TrapGen()
    {
        trapAmount = Mathf.RoundToInt(UnityEngine.Random.Range(trapMin, trapMax));
        for (currentTraps = 0; currentTraps < trapAmount; currentTraps++)
        {
            float trapX = Random.Range(-trapSpawnRange + transform.position.x, trapSpawnRange + transform.position.x);
            float trapZ = Random.Range(-trapSpawnRange + transform.position.z, trapSpawnRange + transform.position.z);
            Vector3 trapPos = new Vector3(trapX, 1f, trapZ);
            GameObject newTrap = Instantiate(trap, trapPos, Quaternion.identity);
            newTrap.GetComponent<BearTrapScript>().player = player;
            newTrap.transform.parent = objectsHierarchy.transform;
        }
    }
    public BasicPlayerScript player;
    public float timeBetweenRespawns;
    public float timeUntilRespawn;
    public int currentTraps;
    public GameObject trap;
    public GameObject objectsHierarchy;
    public int trapMax;
    public int trapMin;
    public int trapAmount;
    public float trapSpawnRange;
}
