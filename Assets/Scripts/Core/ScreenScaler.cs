using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScreenScaler : MonoBehaviour
{
    private void Update()
    {
        Adjust();
    }
    public void Adjust()
    {
        float targetAspect = 16f / 9f;
        float currentAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = currentAspect / targetAspect;
        if (scaleHeight < 1f)
        {
            Rect rect = GetComponent<Camera>().rect;
            rect.width = 1f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1f - scaleHeight) / 2f;
            GetComponent<Camera>().rect = rect;
        }
        else
        {
            float scaleWidth = 1f / scaleHeight;
            Rect rect = GetComponent<Camera>().rect;
            rect.width = scaleWidth;
            rect.height = 1f;
            rect.x = (1f - scaleWidth) / 2f;
            rect.y = 0;
            GetComponent<Camera>().rect = rect;
        }
    }
}
