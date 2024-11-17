using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RectTransform gameOverPanelRect;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private Button replayButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button nextLevelButton;

    private void Awake() {
        replayButton.onClick.AddListener(()=>{
            SceneManager.LoadScene(1);
        });
        menuButton.onClick.AddListener(()=>{
            SceneManager.LoadScene(0);
        });

        gameOverPanelRect.gameObject.SetActive(false);
    }

    private void Start()
    {
        nextLevelButton.onClick.AddListener(()=>{
            GameManager.Instance.GetNextLevel();
        });
    }

    public void ShowGameOverUI(bool isSuccess, string text){
        gameOverText.text = isSuccess? "SUCCESS!": "FAILED!";
        gameOverPanelRect.gameObject.SetActive(true);
        gameOverText.text = text;
        replayButton.gameObject.SetActive(!isSuccess);
        menuButton.gameObject.SetActive(!isSuccess);
        nextLevelButton.gameObject.SetActive(isSuccess);

    }
}
