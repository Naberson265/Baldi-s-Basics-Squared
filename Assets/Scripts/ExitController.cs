using UnityEngine;

public class ExitController : MonoBehaviour
{
	public void Awake()
	{
		permLock = false;
		exitClosed = false;
	}
	public void Update()
	{
		if (gameController.spoopMode && !gameController.escapeSequence && !exitClosed)
		{
			Close();
		}
		if (gameController.escapeSequence && exitClosed && !permLock)
		{
			if (transform.name == "PartyExit" && gameController.closedExits < 3)
			{}
			else Open();
		}
	}
	public void Close()
	{
		transform.position = transform.position - new Vector3(0f, 10f, 0f);
		if (gameController.escapeSequence)
		{
			centerWall.material = mapImage;
			permLock = true;
			Transform soundTransform = transform.Find("CloseExitTrigger");
			if (spoopBaldiScript.isActiveAndEnabled)
			{
				gameController.YTPGain(25);
				spoopBaldiScript.Hear(soundTransform.position, 31f);
			}
		}
		exitClosed = true;
	}
	public void Open()
	{
		transform.position = transform.position + new Vector3(0f, 10f, 0f);
		exitClosed = false;
	}
	public GameController gameController;
	public Material mapImage;
	public SpoopBaldi spoopBaldiScript;
	public MeshRenderer centerWall;
	public bool exitClosed;
	public bool permLock;
}