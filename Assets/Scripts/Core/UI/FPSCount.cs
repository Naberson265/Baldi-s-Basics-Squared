using System.Collections;
using UnityEngine;
using TMPro;

public class FPSCount : MonoBehaviour
{
    public void Update()
    {
        if (PlayerPrefs.GetInt("FPSCountEnabled") == 1)
        {
            if (Time.deltaTime != 0f)
		    {
			    deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
		    	float f = 1f / deltaTime;
    			fpsText.text = Mathf.Ceil(f).ToString() + " FPS";
                if (f >= 90)
                {
                    fpsText.color = Color.cyan;
                }
                else if (f >= 30)
                {
                    fpsText.color = Color.green;
                }
                else if (f >= 15)
                {
                    fpsText.color = Color.yellow;
                }
                else
                {
                    fpsText.color = Color.red;
                }
		    }
        }
        else
        {
            fpsText.text = "";
        }
    }

	public TextMeshProUGUI fpsText;
	public float deltaTime;
}
