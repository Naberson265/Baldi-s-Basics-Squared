using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    public void Update()
    {
        if (PlayerPrefs.GetInt(achievementName) == 1)
        {
            completionIndicator.sprite = completedIcon;
        }
        else
        {
            completionIndicator.sprite = incompletedIcon;
        }
    }
    public string achievementName;
    public Image completionIndicator;
    public Sprite completedIcon;
    public Sprite incompletedIcon;
}
