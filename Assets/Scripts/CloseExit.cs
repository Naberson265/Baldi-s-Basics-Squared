using UnityEngine;

public class CloseExit : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (gameController.closedExits < 3 & gameController.escapeSequence & other.tag == "Player")
		{
			gameController.ExitClose();
			exitController.Close();
		}
	}
	public GameController gameController;
	public ExitController exitController;
}