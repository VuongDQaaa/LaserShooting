using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float rotateAngleLimit;
    private Vector3 movingDirection;
    private float verticalInput;
    private float horizontalInput;

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        //Input
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        //move player
        movingDirection = new Vector3(horizontalInput, 0f, verticalInput);
        Vector3 nextPos = transform.position + movingDirection * moveSpeed * Time.deltaTime;
        transform.position = CheckGround(nextPos);

        //Rotate player
        RotateToMouse();
        // if (movingDirection.magnitude >= 0.1f)
        // {
        //     float targetAgnle = Mathf.Atan2(horizontalInput, verticalInput) * Mathf.Rad2Deg;
        //     Quaternion rotation = Quaternion.Euler(0, targetAgnle, 0);
        //     transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * moveSpeed);
        // }
    }

    private void RotateToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Vector3 target = hitInfo.point;
            Vector3 direction = (target - transform.position).normalized;
            direction.y = 0; // Giữ hướng xoay theo mặt phẳng ngang
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * moveSpeed);
            }
        }
    }


    private Vector3 CheckGround(Vector3 nextPos)
    {
        //check if the below surface is walkable by using layerMash
        RaycastHit hit;
        Debug.DrawRay(nextPos, Vector3.down, Color.blue);
        if (Physics.Raycast(nextPos, Vector3.down, out hit, groundLayer))
        {
            Vector3 newPos = hit.point + Vector3.up * transform.localScale.y;
            newPos.y += 0.2f;
            return newPos;
        }
        return transform.position;
    }
}
