using UnityEngine;
public enum GameState
{
    Start = 0,
    Playing = 1,
    Pause = 2,
    Victory = 3
}


public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject mapPrefab;
    private MapController mapController;
    public GameState currentGameState;
    public int currentEnemies;


    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        currentGameState = GameState.Start;
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentGameState == GameState.Pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        if (mapController != null)
        {
            currentEnemies = mapController.currentEnemies;
        }
    }

    public void SpawnMap()
    {
        if (currentGameState == GameState.Playing)
        {
            GameObject sapwnedMap = Instantiate(mapPrefab);
            mapController = sapwnedMap.GetComponent<MapController>();
            currentEnemies = mapController.currentEnemies;
        }
    }

    public void ClearMap()
    {
        if (mapController != null)
        {
            Destroy(mapController.gameObject);
            mapController = null;
        }
    }

    public void Restart()
    {
        ClearMap();
        currentGameState = GameState.Playing;
        SpawnMap();
    }
}
