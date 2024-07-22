using UnityEngine;
using UnityEngine.UI;

public class CanvasPause : UICanvas
{
    [SerializeField] private Button resumeButton, restartButton, mainMenuButton;

    private void OnEnable()
    {
        resumeButton.onClick.AddListener(ResumeButton);
        restartButton.onClick.AddListener(RestartButton);
        mainMenuButton.onClick.AddListener(MainMenuButton);
    }

    private void OnDisable()
    {
        resumeButton.onClick.RemoveAllListeners();
        restartButton.onClick.RemoveAllListeners();
        mainMenuButton.onClick.RemoveAllListeners();
    }

    private void ResumeButton()
    {
        GameManager.Instance.currentGameState = GameState.Playing;
        UIManager.Instance.OpenUI<CanvasGamePlay>();
        Close(0);
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

