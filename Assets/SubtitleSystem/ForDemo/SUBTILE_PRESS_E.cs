using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SUBTILE_PRESS_E : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            UIPopupTextManagerWithMovement.Show("TEST", Color.green, transform, 1f, 0f);
        }
    }
}
