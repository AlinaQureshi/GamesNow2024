using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RectTransform gameOverPanelRect;
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] private Button replayButton;
    [SerializeField] private Button menuButton;

    private void Awake() {
        replayButton.onClick.AddListener(()=>{
            SceneManager.LoadScene(1);
        });
        menuButton.onClick.AddListener(()=>{
            SceneManager.LoadScene(0);
        });

        gameOverPanelRect.gameObject.SetActive(false);
    }

    public void ShowGameOverUI(bool isSuccess){
        gameOverText.text = isSuccess? "SUCCESS!": "FAILED!";
        gameOverPanelRect.gameObject.SetActive(true);

    }
}
