using UnityEngine;

public class MobileControlScript : MonoBehaviour
{
    private void Start()
    {
        if (Input.touchSupported) gameObject.SetActive(true);
        else gameObject.SetActive(false);
    }
}
