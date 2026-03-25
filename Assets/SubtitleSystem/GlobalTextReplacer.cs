using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GlobalTextReplacer : MonoBehaviour
{
    [SerializeField] private string resourceFileName = "triggers";
    private Dictionary<string, string> triggerDict = new Dictionary<string, string>();
    private HashSet<TMP_Text> processedTexts = new HashSet<TMP_Text>();

    void Start()
    {
        LoadTriggers();
        ReplaceAllTexts();
    }

    void Update()
    {
        ReplaceAllTexts(); 
    }

    private void LoadTriggers()
    {
        triggerDict.Clear();

        TextAsset textAsset = Resources.Load<TextAsset>(resourceFileName);
        if (textAsset == null)
        {
            Debug.LogWarning("NotFound in Resources: " + resourceFileName);
            return;
        }

        string[] lines = textAsset.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);

        foreach (string line in lines)
        {
            string trimmedLine = line.Trim(); 
            if (string.IsNullOrWhiteSpace(trimmedLine)) continue;

            int open = trimmedLine.IndexOf('(');
            int close = trimmedLine.IndexOf(')');

            if (open > 0 && close > open)
            {
                string trigger = trimmedLine.Substring(0, open).Trim();
                string replacement = trimmedLine.Substring(open + 1, close - open - 1).Trim();

                if (!triggerDict.ContainsKey(trigger))
                {
                    triggerDict.Add(trigger, replacement);
                }
            }
        }
    }

    private void ReplaceAllTexts()
    {
        TMP_Text[] allTexts = FindObjectsOfType<TMP_Text>(true);

        foreach (var text in allTexts)
        {
            if (text == null || processedTexts.Contains(text)) continue;

            if (triggerDict.ContainsKey(text.text))
            {
                text.text = triggerDict[text.text];
            }

            processedTexts.Add(text);
        }
    }
}
