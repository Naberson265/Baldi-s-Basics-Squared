using UnityEngine;

public class ChalkDust : MonoBehaviour
{
    public void Start()
    {
        if (PlayerPrefs.GetInt("DietItemModifier") == 1) chalkTimer = 20f;
        else chalkTimer = 60f;
    }
    public void Update()
    {
        if (transform.name != "ChalkDust") transform.name = "ChalkDust";
		chalkTimer -= 1f * Time.deltaTime;
        if (chalkTimer <= 0f) Destroy(gameObject);
    }
    public float chalkTimer;
}
