using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanvasCharacter : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private Slider hpBar;
    [SerializeField] private TextMeshProUGUI lostedHPText;
    [SerializeField] private float disableLostedHPTextTime;

    void Start()
    {
        mainCamera = Camera.main;
        hpBar.maxValue = enemyController.maxHP;
    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
        hpBar.value = enemyController.currentHP;
    }

    public void UpdateLostedHP(float damage)
    {
        if (!lostedHPText.gameObject.activeSelf)
        {
            lostedHPText.gameObject.SetActive(true);
            lostedHPText.text = "-" + damage.ToString();
        }
        StartCoroutine(DisableText(disableLostedHPTextTime));
    }

    IEnumerator DisableText(float time)
    {
        yield return new WaitForSeconds(time);
        if (lostedHPText.gameObject.activeSelf)
        {
            lostedHPText.gameObject.SetActive(false);
        }
    }
}
