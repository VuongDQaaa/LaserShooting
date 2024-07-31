using UnityEngine;

public class LaserController : MonoBehaviour
{
    [Header("Laser setting")]
    [SerializeField] private LineRenderer laserBeam;
    [SerializeField] private float laserDistance;
    [SerializeField] private int maxReflect;
    [SerializeField] private int maxEnemy;
    [SerializeField] private int laserDamge;
    [SerializeField] private float damageTime;

    [Header("Effect")]
    [SerializeField] private ParticleSystem fireBeamStart;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private AudioSource laserSound;
    private float timeCounter = 0f;
    //private List<GameObject> generatedEffects;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.currentGameState == GameState.Playing)
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButton(0))
        {
            laserBeam.enabled = true;
            fireBeamStart.gameObject.SetActive(true);
            if (!laserSound.isPlaying)
            {
                laserSound.Play();
            }
            ShootLaser();
        }
        else
        {
            fireBeamStart.gameObject.SetActive(false);
            if (laserSound.isPlaying)
            {
                laserSound.Stop();
            }
            laserBeam.positionCount = 1;
            laserBeam.enabled = false;
            timeCounter = 0;
        }
    }

    private void ShootLaser()
    {
        Vector3 laserStartPos = transform.position;
        Vector3 laserDirection = transform.forward;
        laserBeam.positionCount = 1;
        laserBeam.SetPosition(0, laserStartPos);

        RaycastHit hit;
        Vector3 currentPos = laserStartPos;
        Vector3 currentDirection = laserDirection;

        int reflections = 0;
        int enemyPenetrations = 0;

        while (reflections <= maxReflect)
        {
            if (Physics.Raycast(currentPos, currentDirection, out hit, laserDistance))
            {
                laserBeam.positionCount++;
                laserBeam.SetPosition(laserBeam.positionCount - 1, hit.point);

                if (hit.collider.CompareTag(Constant.ENEMY_TAG_NAME))
                {
                    // Handle enemy hit, e.g., reduce health
                    EnemyController enemyController = hit.collider.GetComponent<EnemyController>();
                    if (enemyController != null)
                    {
                        DamageEnemy(enemyController);
                    }

                    enemyPenetrations++;
                    if (enemyPenetrations > maxEnemy)
                    {
                        break;
                    }
                    currentPos = hit.point;
                }
                else
                {
                    // Reflect laser off wall or other objects
                    reflections++;
                    currentPos = hit.point;
                    currentDirection = Vector3.Reflect(currentDirection, hit.normal);
                }
            }

            else
            {
                laserBeam.positionCount++;
                laserBeam.SetPosition(laserBeam.positionCount - 1, currentPos + (currentDirection * laserDistance));
                break;
            }
        }
    }

    //TODO effect when hit and shoot laser
    private void DamageEnemy(EnemyController enemyController)
    {
        timeCounter += Time.deltaTime;
        if (enemyController != null && laserBeam.enabled)
        {
            if (timeCounter >= damageTime)
            {
                enemyController.TakeDamage(laserDamge);
                timeCounter = 0;
            }
        }
    }
}
