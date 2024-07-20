using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float rotateAngleLimit;
    private float verticalInput;
    private float horizontalInput;
    private float yRotation;

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RotatePlayer();
        Attack();
    }

    private void MovePlayer()
    {
        //Move by keyboard
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movingDir = new Vector3(horizontalInput, 0f, verticalInput);

        Vector3 nextPos = transform.position + movingDir * moveSpeed * Time.deltaTime;
        transform.position = CheckGround(nextPos);
    }

    private void RotatePlayer()
    {
        float mousexPostion = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;
        yRotation += mousexPostion;
        yRotation = Mathf.Clamp(yRotation, -rotateAngleLimit, rotateAngleLimit);

        transform.rotation = Quaternion.Euler(0, yRotation, 0);
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

    private void Attack()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Attack");
        }
    }
}
