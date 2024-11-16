using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public LevelData currentLevel;
    public Bag bag;
    public Transform itemSpawnArea;
    public bool isGameActive = true;
    public GameObject winEffect;

    private void Awake()
    {
        if (Instance == null){Instance = this;}

        LoadLevel(currentLevel);
    }

    public void LoadLevel(LevelData levelData)
    {
        currentLevel = levelData;
        ClearLevel();
        SpawnItems();
    }

    private void SpawnItems()
    {
        float spacing = 1.5f;
        int itemsPerRow = 3;
        int currentItem = 0;

        foreach (var itemData in currentLevel.availableItems)
        {
            Vector3 spawnPosition = itemSpawnArea.position;
            spawnPosition.x += (currentItem % itemsPerRow) * spacing;
            spawnPosition.z += (currentItem / itemsPerRow) * spacing;

            GameObject item = Instantiate(itemData.prefab, spawnPosition, Quaternion.identity);
            DraggableItem draggable = item.GetComponent<DraggableItem>();
            draggable.itemData = itemData;

            currentItem++;
        }
    }

    private void ClearLevel()
    {
        foreach (var item in bag.itemsInBag)
        {
            Destroy(item.gameObject);
        }
        bag.itemsInBag.Clear();
    }

    public void OnItemDropped(DraggableItem item)
    {
        if (!bag.HasSpace(item))
        {
            item.ReturnToStart();
        }
    }

    public void CheckWinCondition()
    {
        bool hasAllRequired = currentLevel.requiredItems.All(required =>
            bag.itemsInBag.Any(item => item.itemData.itemName == required.itemName));

        if (hasAllRequired)
        {
            WinLevel();
        }
    }

    private void WinLevel()
    {
        isGameActive = false;
        if (winEffect != null)
        {
            Instantiate(winEffect, bag.transform.position + Vector3.up * 2f, Quaternion.identity);
        }
        print("Level Complete!");
        // additional win logics
    }
}
