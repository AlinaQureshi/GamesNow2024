using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelData", menuName = "BriefSpace/LevelData")]
public class LevelData : ScriptableObject
{
    public string levelName;
    public string levelPrompt;
    public List<ItemData> availableItems;
    public List<ItemData> requiredItems;
    public float bagCapacity = 10f;
    public string winPrompt;
    public string losePrompt;
}