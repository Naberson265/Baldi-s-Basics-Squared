using UnityEngine;

public class RandomTreeGenerator : MonoBehaviour
{
    private void Start()
    {
        treeAmount = Mathf.RoundToInt(UnityEngine.Random.Range(treeMin, treeMax));
        TreeGen();
    }
    public void TreeGen()
    {
        for (currentTrees = 0; currentTrees < treeAmount; currentTrees++)
        {
            float treeX = Random.Range(-treeSpawnRange + transform.position.x, treeSpawnRange + transform.position.x);
            float treeZ = Random.Range(-treeSpawnRange + transform.position.z, treeSpawnRange + transform.position.z);
            Vector3 treePos = new Vector3(treeX, 6.5f, treeZ);
            GameObject newTree = Instantiate(tree, treePos, Quaternion.identity);
            newTree.transform.parent = objectsHierarchy.transform;
        }
    }
    public int currentTrees;
    public GameObject tree;
    public GameObject objectsHierarchy;
    public int treeMax;
    public int treeMin;
    public int treeAmount;
    public float treeSpawnRange;
}
