using UnityEngine;

public class TreeDeadZoneScript : MonoBehaviour
{
    public void OnTriggerStay(Collider other)
    {
        if (other.transform.name == "SunsetTree(Clone)" || other.transform.name == "Coal" || other.transform.name == "Gasoline" || other.transform.name == "BearTrap(Clone)")
        {
            Destroy(other.gameObject);
        }
    }
}
