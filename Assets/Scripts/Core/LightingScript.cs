using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingManager : MonoBehaviour
{
    public GameObject[] renderers, lights;
    public List<Vector3> Pos;
    public float timerValue = 0.5f;
    private float timer;
    public bool IsStatic = false;
    public bool update = false;
    public bool setUpOnStart = false;
    public int sets;
    public Color lowestColor = new Color(0.12f,0.12f,0.12f);
    private void Start()
    {
        timer = timerValue;
        renderers = GameObject.FindGameObjectsWithTag("LightingAffected");
        lights = GameObject.FindGameObjectsWithTag("LightSource");
        Pos = new List<Vector3>();
        foreach (GameObject light in lights) Pos.Add(light.transform.position);
        if (setUpOnStart) SetLighting();
    }
    private void Update()
    {
        if (!IsStatic)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer = timerValue;
                CheckIfChanged();
                if (update == true) SetLighting();
            }
        }
    }
    private void CheckIfChanged()
    {
        foreach (GameObject light in lights) if (!Pos.Contains(light.transform.position)) SetLighting();
    }
    public void SetLighting()
    {
        renderers = GameObject.FindGameObjectsWithTag("LightingAffected");
        lights = GameObject.FindGameObjectsWithTag("LightSource");
        sets++;
        Pos.Clear();
        foreach (GameObject light in lights)
        {
            Pos.Add(light.transform.position);
        }
        foreach (GameObject renderer in renderers)
        {
            float minDistance = Mathf.Infinity;
            Vector3 ChangedPos = Vector3.zero;
            foreach (GameObject light in lights)
            {
                float distance = Vector3.Distance(light.transform.position, renderer.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    ChangedPos = light.transform.position;
                    float lighting = Mathf.Clamp((minDistance - 10) / light.GetComponent<Light>().range, 0, 1);
                    renderer.GetComponent<Renderer>().material.color = new Color(light.GetComponent<Light>().color.r - lighting, light.GetComponent<Light>().color.g - lighting, light.GetComponent<Light>().color.b - lighting);
                    if (renderer.GetComponent<Renderer>().material.color.r < lowestColor.r)
                    {
                        renderer.GetComponent<Renderer>().material.color = new Color (lowestColor.r, renderer.GetComponent<Renderer>().material.color.g, renderer.GetComponent<Renderer>().material.color.b);
                    }
                    if (renderer.GetComponent<Renderer>().material.color.g < lowestColor.g)
                    {
                        renderer.GetComponent<Renderer>().material.color = new Color (renderer.GetComponent<Renderer>().material.color.r, lowestColor.g, renderer.GetComponent<Renderer>().material.color.b);
                    }
                    if (renderer.GetComponent<Renderer>().material.color.b < lowestColor.b)
                    {
                        renderer.GetComponent<Renderer>().material.color = new Color (renderer.GetComponent<Renderer>().material.color.r, renderer.GetComponent<Renderer>().material.color.g, lowestColor.b);
                    }
                }
            }
        }
    }
}