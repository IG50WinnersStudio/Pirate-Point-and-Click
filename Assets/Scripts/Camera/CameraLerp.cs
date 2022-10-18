using UnityEngine;


public class CameraLerp : MonoBehaviour
{
    private Quaternion defaultRotation;
    private Quaternion farObjectRotation;
    private Quaternion targetRotation;
    private Vector3 defaultPosition;
    private Vector3 targetPosition;
    private float moveSpeed = 0.025f;
    private float smoothSpeed = 0.025f;
    private bool isMoving = false;


    private void Awake()
    {      
        defaultPosition = new Vector3(0, 3, -3.5f);
        targetPosition = new Vector3(0, 0, 0);
        defaultRotation = Quaternion.Euler(15, 0, 0);
        farObjectRotation = Quaternion.Euler(30, 0, 0);
        targetRotation = Quaternion.Euler(0, 0, 0);
    }


    private void Update()
    {
        // Move towards object
        if (isMoving)
        {
            // Lerp toward target rotation and position at all times.
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, moveSpeed);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

            // If the camera reaches its target position then set 'isMoving' bool to false
            if (transform.position == targetPosition)
            {
                Debug.Log("isMoving is False now: ____________________");
                isMoving = false;
            }

        }
    }


    public void OnMoveCameraPosition(RaycastHit hitInfo)
    {
        isMoving = true;
        targetPosition = hitInfo.transform.position - new Vector3(0, -hitInfo.collider.bounds.size.y * 1.4f, 2.5f);

        // If the object is past half way of the floor away from the player then increase the rotation angle to look down more on the object
        if (hitInfo.transform.localPosition.x > 3)
            targetRotation = farObjectRotation;
        else
            targetRotation = defaultRotation;
        
   }


    public void OnResetCameraPosition()
    {
        isMoving = true;
        targetPosition = defaultPosition;
        targetRotation = defaultRotation;
    }
}