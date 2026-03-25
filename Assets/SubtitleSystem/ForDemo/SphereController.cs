using UnityEngine;

public class SphereController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float rotationSpeed = 100f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        
        float rotateHorizontal = Input.GetAxisRaw("Forward");

        if (rotateHorizontal != 0)
        {
            
            transform.Rotate(Vector3.up * rotateHorizontal * rotationSpeed * Time.deltaTime);
        }
    }
}
