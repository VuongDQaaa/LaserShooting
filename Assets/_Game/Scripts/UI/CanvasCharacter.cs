using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasCharacter : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private Slider hpBar;
    [SerializeField] private TextMeshProUGUI damgeText;

    void Start()
    {
        mainCamera = Camera.main;
        hpBar.maxValue = enemyController.maxHP;
    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
        hpBar.value = enemyController.currentHP;
        damgeText.text = enemyController.currentHP + "/" + enemyController.maxHP;
    }
}
