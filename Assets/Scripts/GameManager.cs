using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public LevelData currentLevel;
    public Bag bag;
    public Transform itemSpawnArea;
    public bool isGameActive = true;
    public GameObject winEffect;

    [SerializeField] LevelData[] levels;
    private int currentLevelIndex;
    [SerializeField] private float spawnPopDelay = 0.3f;
    
    [Header("Scene References")]
    [SerializeField] private AnimationManager animationManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Transform itemParent;

    [SerializeField] List<DraggableItem> draggableItems = new();

    public GridLayout itemGrid;

    private void Awake()
    {
        if (Instance == null){Instance = this;}

        currentLevelIndex = 0;
        LoadLevel(currentLevel);
    }

    private void Start()
    {
        GetAllItems();
    }

    private void GetAllItems()
    {

        foreach (Transform item in itemParent)
        {
            var draggable  = item.GetComponent<DraggableItem>();
            draggableItems.Add(draggable); 
            draggable.PopUp(Random.Range(spawnPopDelay, spawnPopDelay * 2.4f));
        }
    }

    public void LoadLevel(LevelData levelData)
    {
        currentLevel = levelData;
        ClearLevel();
        //SpawnItems();
        //SpawnItemsInGrid();
        animationManager.ShowPrompt(levelData.levelPrompt);
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

    private void SpawnItemsInGrid()
    {
        int currentIndex = 0;
        float delay = 0;
        foreach (var itemData in currentLevel.availableItems)
        {
            Vector3 spawnPosition = itemGrid.GetGridPosition(currentIndex);
            
            GameObject item = Instantiate(itemData.prefab, spawnPosition, Quaternion.identity);
            DraggableItem draggable = item.GetComponent<DraggableItem>();
            draggable.itemData = itemData;
            draggable.gridLayout = itemGrid; // assign grid reference to item
            draggable.PopUp(delay);
            delay += spawnPopDelay;
            currentIndex++;
        }
    }

    private void ClearLevel()
    {
        foreach (var item in draggableItems)
        {
            item.ReturnToStart();
        }
        bag.itemsInBag.Clear();
        animationManager.ShowPrompt("");
        animationManager.CloseBag(false);

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

        StartCoroutine(CloseBriefCaseAndCheck());
        IEnumerator CloseBriefCaseAndCheck()
        {
            animationManager.CloseBag(true);
            yield return new WaitForSeconds(0.6f);
            
            if (hasAllRequired)
            {
                WinLevel();
            }
            else
            {
                LoseLevel();
            }
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
        uiManager.ShowGameOverUI(true, currentLevel.winPrompt);

        // additional win logics
    }
    
    private void LoseLevel()
    {
        isGameActive = false;
        print("Level Lost!");
        // additional lose logics
        uiManager.ShowGameOverUI(false, currentLevel.losePrompt);

    }

    public void GetNextLevel()
    {
        currentLevelIndex++;
        if (currentLevelIndex < levels.Length)
        {
            LoadLevel(levels[currentLevelIndex]);
        }
    }
}
