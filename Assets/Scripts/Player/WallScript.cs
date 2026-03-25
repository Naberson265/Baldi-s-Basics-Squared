using UnityEngine;
using UnityEngine.AI;

public class WallScript : MonoBehaviour
{
    void Start()
    {
        navObstacle = gameObject.GetComponent<NavMeshObstacle>();
        if (navObstacle.size.z == 55000f) navObstacle.size = new Vector3(navObstacle.size.x, navObstacle.size.y, 70000);
        baseSize = new Vector3(navObstacle.size.x, navObstacle.size.y, navObstacle.size.z);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "BSODA")
        navObstacle.size = new Vector3(baseSize.x, baseSize.y, 1);
    }
    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "BSODA")
        navObstacle.size = baseSize;
    }
    private NavMeshObstacle navObstacle;
    private Vector3 baseSize;
}
