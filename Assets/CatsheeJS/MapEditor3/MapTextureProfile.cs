using UnityEngine;

[CreateAssetMenu(menuName ="Assets/CJS/Map Textures")]
public class MapTextureProfile : ScriptableObject
{
    [Header("Hallway textures")]
    public MapTexture DefaultFloorTex;
    public MapTexture DefaultWallTex;
    public MapTexture DefaultThinWallTex;
    public MapTexture DefaultCeilingTex;

    [Space()]
    [Header("Basic Room textures")]
    public MapTexture RoomFloorTex;
    public MapTexture RoomWallTex;
    public MapTexture RoomThinWallTex;
    public MapTexture RoomCeilingTex;

    [Space()]
    public MapTexture[] floorTextures;
    public MapTexture[] wallTextures;
    public MapTexture[] ceilingTextures;
}

[System.Serializable]
public struct MapTexture
{
    public string name;
    public Material mat;
}