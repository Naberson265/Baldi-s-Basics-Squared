using UnityEngine;

public class ExitlockTrigger : MonoBehaviour
{
    private void Start()
    {
        closed = false;
    }
	private void OnTriggerExit(Collider other)
	{
		if (other.name == "Player" && !closed)
		{
            exit1.Close();
            exit2.Close();
            exit3.Close();
            exit4.Close();
            closed = true;
            Destroy(gameObject);
		}
	}
    public bool closed;
    public ExitController exit1;
    public ExitController exit2;
    public ExitController exit3;
    public ExitController exit4;
}
