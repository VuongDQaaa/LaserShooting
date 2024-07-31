using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    //[SerializeField] private float smoothSpeed = 0.125f;


    // Update is called once per frame
    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = desiredPosition;
            transform.LookAt(target.position);
        }
    }

    public void SetTarget(GameObject player)
    {
        target = player.transform;
    }
}
