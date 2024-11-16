using UnityEngine;
using System.Collections.Generic;

public class Bag : MonoBehaviour
{
    public List<DraggableItem> itemsInBag = new();
    public float maxCapacity = 10f;
   //[SerializeField] private BoxCollider bagCollider;

    void Awake()
    {
        //bagCollider = GetComponentInChildren<BoxCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<DraggableItem>(out var item))
        {
            if(!itemsInBag.Contains(item)){
            itemsInBag.Add(item);
            GameManager.Instance.CheckWinCondition(); //??
        }}
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<DraggableItem>(out var item))
        {
            itemsInBag.Remove(item);
        }
    }

    public bool HasSpace(DraggableItem newItem)
    {
        float currentWeight = 0f;
        foreach (var item in itemsInBag)
        {
            currentWeight += item.itemData.weight;
        }
        return currentWeight + newItem.itemData.weight <= maxCapacity;
    }
}
