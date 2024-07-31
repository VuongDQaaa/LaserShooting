using UnityEngine;
using UnityEngine.UI;

public class CanvasLoseUI : UICanvas
{
 [SerializeField] private Button restartButton, mainMenuButton;

    private void OnEnable()
    {
        restartButton.onClick.AddListener(RestartButton);
        mainMenuButton.onClick.AddListener(MainMenuButton);
    }

    private void OnDisable()
    {
        restartButton.onClick.RemoveAllListeners();
        mainMenuButton.onClick.RemoveAllListeners();
    }

    private void RestartButton()
    {
        GameManager.Instance.ClearMap();
        GameManager.Instance.SpawnMap();
        GameManager.Instance.currentGameState = GameState.Playing;
        UIManager.Instance.OpenUI<CanvasGamePlay>();
        Close(0);
    }

    private void MainMenuButton()
    {
        GameManager.Instance.ClearMap();
        GameManager.Instance.currentGameState = GameState.Start;
        UIManager.Instance.OpenUI<CanvasMainMenu>();
        Close(0);
    }
}
