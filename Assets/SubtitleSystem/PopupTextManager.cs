using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;

public class UIPopupTextManagerWithMovement : MonoBehaviour
{
    public static UIPopupTextManagerWithMovement Instance;

    [Header("UI Prefab (must contain TextMeshProUGUI and Image as background)")]
    public GameObject popupPrefab;

    [Header("Canvas for placement")]
    public Canvas parentCanvas;

    [Header("Appearance Settings")]
    public float defaultDelay = 0.5f;
    public float defaultDuration = 2f;
    public float fadeInTime = 0.2f;
    public float fadeOutTime = 0.2f;

    [Header("Circle Settings (as in Baldi's Basics)")]
    public float minRadius = 100f;
    public float maxRadius = 300f;
    public float minDistance = 1f;
    public float maxDistance = 10f;

    [Header("Scale Settings")]
    public float minScale = 0.5f;
    public float maxScale = 1.5f;

    [Header("Canvas Edge Padding")]
    public Vector2 canvasPadding = new Vector2(50f, 50f);

    [Header("Angular Offset Parameters")]
    public float angularSpeedMultiplier = 30f;
    public float maxAngularSpeedDeg = 720f;
    public float rotationDamping = 8f;

    [Header("Background Padding and Minimum Size")]
    public Vector2 imagePadding = new Vector2(20f, 12f);
    public Vector2 minBackgroundSize = new Vector2(60f, 30f);

    private Transform playerTransform;
    private Camera mainCamera;

    // Queue for each source
    private Dictionary<Transform, Queue<PopupRequest>> popupQueues = new Dictionary<Transform, Queue<PopupRequest>>();
    private Dictionary<Transform, bool> isProcessing = new Dictionary<Transform, bool>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) playerTransform = player.transform;
        else UnityEngine.Debug.LogError("[UIPopupTextManager] Player not found!");

        mainCamera = Camera.main;
        if (mainCamera == null) UnityEngine.Debug.LogError("[UIPopupTextManager] MainCamera not found!");
    }

    public void SpawnText(string message, Color color, Transform sourceTransform, float duration = -1f, float delay = -1f, Vector2? offsetFromSource = null)
    {
        if (duration <= 0) duration = defaultDuration;
        if (delay < 0) delay = defaultDelay;

        if (popupPrefab == null || parentCanvas == null || sourceTransform == null || playerTransform == null || mainCamera == null)
        {
            UnityEngine.Debug.LogError("[UIPopupTextManager] Error in popup data!");
            return;
        }

        if (!popupQueues.ContainsKey(sourceTransform))
            popupQueues[sourceTransform] = new Queue<PopupRequest>();

        popupQueues[sourceTransform].Enqueue(new PopupRequest
        {
            message = message,
            color = color,
            sourceTransform = sourceTransform,
            duration = duration,
            delay = delay,
            offsetFromSource = offsetFromSource
        });

        if (!isProcessing.ContainsKey(sourceTransform) || !isProcessing[sourceTransform])
            StartCoroutine(ProcessQueue(sourceTransform));
    }

    private IEnumerator ProcessQueue(Transform sourceTransform)
    {
        isProcessing[sourceTransform] = true;

        while (popupQueues[sourceTransform].Count > 0)
        {
            PopupRequest request = popupQueues[sourceTransform].Dequeue();

            GameObject obj = Instantiate(popupPrefab, parentCanvas.transform, false);
            yield return HandlePopupLifecycle(obj, request.message, request.color, request.sourceTransform, request.duration, request.delay, request.offsetFromSource);
        }

        isProcessing[sourceTransform] = false;
    }

    private Vector2 ClampPositionToCanvas(RectTransform rt, Vector2 desiredPosition)
    {
        RectTransform canvasRect = parentCanvas.GetComponent<RectTransform>();
        Vector2 canvasSize = canvasRect.sizeDelta;
        Vector2 halfCanvasSize = canvasSize * 0.5f;

        Vector2 prefabSize = rt.sizeDelta * rt.localScale;
        Vector2 halfPrefabSize = prefabSize * 0.5f;

        float minX = -halfCanvasSize.x + halfPrefabSize.x + canvasPadding.x;
        float maxX = halfCanvasSize.x - halfPrefabSize.x - canvasPadding.x;
        float minY = -halfCanvasSize.y + halfPrefabSize.y + canvasPadding.y;
        float maxY = halfCanvasSize.y - halfPrefabSize.y - canvasPadding.y;

        return new Vector2(Mathf.Clamp(desiredPosition.x, minX, maxX),
                           Mathf.Clamp(desiredPosition.y, minY, maxY));
    }

    private IEnumerator HandlePopupLifecycle(GameObject obj, string message, Color color, Transform sourceTransform, float duration, float delay, Vector2? offsetFromSource)
    {
        if (obj == null)
            yield break;

        TextMeshProUGUI tmp = obj.GetComponentInChildren<TextMeshProUGUI>();
        if (tmp != null)
        {
            tmp.text = message;
            tmp.color = color;

            tmp.ForceMeshUpdate();

            Vector2 pref = tmp.GetPreferredValues(message, 10000f, 10000f);
            tmp.rectTransform.sizeDelta = pref;

            Image bg = obj.GetComponentInChildren<Image>();
            if (bg != null)
            {
                Vector2 bgSize = pref + imagePadding;
                bgSize.x = Mathf.Max(bgSize.x, minBackgroundSize.x);
                bgSize.y = Mathf.Max(bgSize.y, minBackgroundSize.y);
                bg.rectTransform.sizeDelta = bgSize;
                LayoutRebuilder.ForceRebuildLayoutImmediate(bg.rectTransform);
            }
        }

        RectTransform rt = obj.GetComponent<RectTransform>();
        if (rt == null)
        {
            Destroy(obj);
            yield break;
        }

        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.localRotation = Quaternion.identity;

        CanvasGroup cg = obj.GetComponent<CanvasGroup>();
        if (cg == null) cg = obj.AddComponent<CanvasGroup>();
        cg.alpha = 0f;

        if (delay > 0f) yield return new WaitForSeconds(delay);

        Vector3 dir = sourceTransform.position - playerTransform.position;
        Vector3 dirXZ = new Vector3(dir.x, 0f, dir.z);
        if (dirXZ.sqrMagnitude < 0.0001f) dirXZ = mainCamera.transform.forward;
        dirXZ.Normalize();

        Vector3 camForwardXZ = new Vector3(mainCamera.transform.forward.x, 0f, mainCamera.transform.forward.z).normalized;
        float baseAngle = Vector3.SignedAngle(camForwardXZ, dirXZ, Vector3.up);

        float distance = Vector3.Distance(sourceTransform.position, playerTransform.position);
        float radius = maxRadius;

        float rad = baseAngle * Mathf.Deg2Rad;

        Vector2 offset = offsetFromSource ?? Vector2.zero;
        Vector2 circlePos = new Vector2(Mathf.Sin(rad), Mathf.Cos(rad)) * radius + offset;

        rt.anchoredPosition = ClampPositionToCanvas(rt, circlePos);

        float scale = Mathf.Lerp(maxScale, minScale, Mathf.InverseLerp(minDistance, maxDistance, distance));
        rt.localScale = new Vector3(scale, scale, 1f);

        float t = 0f;
        while (t < fadeInTime)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Clamp01(t / fadeInTime);
            yield return null;
        }
        cg.alpha = 1f;

        Vector3 prevSourcePos = sourceTransform.position;
        float rotationOffsetAngle = 0f;
        float displayedAngle = baseAngle;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            distance = Vector3.Distance(sourceTransform.position, playerTransform.position);


            radius = maxRadius;

            dir = sourceTransform.position - playerTransform.position;
            dirXZ = new Vector3(dir.x, 0f, dir.z);
            if (dirXZ.sqrMagnitude < 0.0001f) dirXZ = mainCamera.transform.forward;
            dirXZ.Normalize();

            camForwardXZ = new Vector3(mainCamera.transform.forward.x, 0f, mainCamera.transform.forward.z).normalized;
            baseAngle = Vector3.SignedAngle(camForwardXZ, dirXZ, Vector3.up);

            Vector3 sourceVelocity = Time.deltaTime > 0f ? (sourceTransform.position - prevSourcePos) / Time.deltaTime : Vector3.zero;
            Vector3 tangent = Vector3.Cross(Vector3.up, dirXZ).normalized;
            float sign = Mathf.Sign(Vector3.Dot(sourceVelocity, tangent));
            float angularSpeed = Mathf.Clamp(sourceVelocity.magnitude * angularSpeedMultiplier, 0f, maxAngularSpeedDeg);
            rotationOffsetAngle += angularSpeed * sign * Time.deltaTime;
            prevSourcePos = sourceTransform.position;

            float targetAngle = baseAngle + rotationOffsetAngle;
            displayedAngle = Mathf.LerpAngle(displayedAngle, targetAngle, 1f - Mathf.Exp(-rotationDamping * Time.deltaTime));

            rad = displayedAngle * Mathf.Deg2Rad;
            circlePos = new Vector2(Mathf.Sin(rad), Mathf.Cos(rad)) * radius + offset;
            rt.anchoredPosition = ClampPositionToCanvas(rt, circlePos);

            scale = Mathf.Lerp(maxScale, minScale, Mathf.InverseLerp(minDistance, maxDistance, distance));
            rt.localScale = new Vector3(scale, scale, 1f);

            yield return null;
        }

        // FadeOut
        t = 0f;
        while (t < fadeOutTime)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Clamp01(1f - t / fadeOutTime);
            yield return null;
        }

        Destroy(obj);
    }

    public static void Show(string message, Color color, Transform source, float duration = -1f, float delay = -1f, Vector2? offset = null)
    {
        if (Instance != null)
            Instance.SpawnText(message, color, source, duration, delay, offset);
        else
            UnityEngine.Debug.LogError("[UIPopupTextManager] Instance does not exist in the scene!");
    }

    private struct PopupRequest
    {
        public string message;
        public Color color;
        public Transform sourceTransform;
        public float duration;
        public float delay;
        public Vector2? offsetFromSource;
    }
}