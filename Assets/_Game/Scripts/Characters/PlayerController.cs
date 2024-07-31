using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    private Vector3 moveDirection;

    [Header("Body")]
    [SerializeField] private Transform tankTurret;
    private Vector3 bodyRotation;
    private Vector3 turretRotation;
    private float verticalInput;
    private float horizontalInput;


    //Awake is called when the script instance is being loaded.
    void Awake()
    {
        bodyRotation = Vector3.forward;
    }

    //Update is called every frame, if the MonoBehaviour is enabled.
    void Update()
    {
        //Input
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        moveDirection = new Vector3(horizontalInput, 0f, verticalInput);

        //update tank body rotation
        if (moveDirection != Vector3.zero)
        {
            bodyRotation = moveDirection * rotateSpeed * Time.deltaTime;
        }

        RotateTurret();
    }


    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (GameManager.Instance.currentGameState == GameState.Playing)
        {
            //move player
            Vector3 movingDirection = moveDirection.normalized * moveSpeed * Time.deltaTime;
            Vector3 nextPos = transform.position + movingDirection;
            rb.MovePosition(nextPos);

            //Rotate player
            RotatePlayer();
        }
        else
        {
            rb.MovePosition(transform.position);
        }
    }

    private void RotatePlayer()
    {
        Quaternion lookRotation = Quaternion.LookRotation(bodyRotation);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
    }

    private void RotateTurret()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Vector3 target = hitInfo.point;
            Vector3 direction = (target - transform.position).normalized;
            direction.y = 0;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                tankTurret.rotation = Quaternion.Slerp(tankTurret.rotation, lookRotation, Time.deltaTime * rotateSpeed);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.ENEMY_TAG_NAME))
        {
            EnemyController enemyController = other.transform.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                if (!enemyController.isDead)
                {
                    GameManager.Instance.currentGameState = GameState.Loose;
                    UIManager.Instance.OpenUI<CanvasLoseUI>();
                }
            }
        }
    }
}