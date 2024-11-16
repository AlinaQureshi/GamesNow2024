using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "BriefSpace/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public GameObject prefab;
    public Vector3 size;
    public float weight;
    public bool isRequired;
}
