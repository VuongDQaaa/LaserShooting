using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGamePlay : UICanvas
{
    [SerializeField] private Button pauseButon;
    [SerializeField] private TextMeshProUGUI enemiesText;

    private void OnEnable()
    {
        pauseButon.onClick.AddListener(PauseButon);
    }

    private void OnDisable()
    {
        pauseButon.onClick.RemoveAllListeners();
    }

    // Update is called every frame, if the MonoBehaviour is enabled.
    void Update()
    {
        enemiesText.text = "Enemies: " + GameManager.Instance.currentEnemies.ToString();
    }

    private void PauseButon()
    {
        GameManager.Instance.currentGameState = GameState.Pause;
        UIManager.Instance.OpenUI<CanvasPause>();
        Close(0);
    }


}
