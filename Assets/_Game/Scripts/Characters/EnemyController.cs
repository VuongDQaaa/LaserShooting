using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public MapController currentMap;
    public int maxHP;
    public int currentHP;
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //TODO
        //Die effect

        currentMap.currentEnemies--;
        if(currentMap.currentEnemies == 0)
        {
            GameManager.Instance.currentGameState = GameState.Victory;
            UIManager.Instance.OpenUI<CanvasWin>();
        }
        Destroy(gameObject);
    }
}
