using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelData", menuName = "BriefSpace/LevelData")]
public class LevelData : ScriptableObject
{
    public string levelName;
    public List<ItemData> availableItems;
    public List<ItemData> requiredItems;
    public float bagCapacity = 10f;
}