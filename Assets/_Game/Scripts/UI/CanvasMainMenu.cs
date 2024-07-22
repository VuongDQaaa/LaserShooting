using UnityEngine;
using UnityEngine.UI;

public class CanvasMainMenu : UICanvas
{
    [SerializeField] private Button playButton;

    private void OnEnable()
    {
        playButton.onClick.AddListener(PlayButton);
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveAllListeners();
    }

    private void PlayButton()
    {
        GameManager.Instance.currentGameState = GameState.Playing;
        GameManager.Instance.SpawnMap();
        Close(0);
        UIManager.Instance.OpenUI<CanvasGamePlay>();
    }
}
