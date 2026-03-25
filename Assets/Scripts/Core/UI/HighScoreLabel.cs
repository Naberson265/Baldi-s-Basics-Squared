using UnityEngine;
using TMPro;

public class HighScoreLabel : MonoBehaviour
{
    public void Start()
    {
        highScoreText = gameObject.GetComponent<TMP_Text>();
    }
    public void Update()
    {
        highScoreText.text = PlayerPrefs.GetInt(relatedScore).ToString() + " Notebooks";
    }
    private TMP_Text highScoreText;
    public string relatedScore;
}
