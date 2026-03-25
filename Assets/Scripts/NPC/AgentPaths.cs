#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class NavMeshAgentRay : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        if (showPath && agent != null)
        {
            NavMeshPath path = agent.path;
            if (path != null)
            {
                Handles.color = pathColor;
                for (int i = 0; i < path.corners.Length - 1; i++)
                {
                    Handles.DrawAAPolyLine(pathWidth, path.corners[i], path.corners[i + 1]);
                }
            }
        }
    }
    [Header("References")]
    [SerializeField] private NavMeshAgent agent;
    [Header("Settings")]
    [SerializeField] private bool showPath = true;
    [SerializeField] private Color pathColor;
    [SerializeField] private float pathWidth = 15f;
}
#endif