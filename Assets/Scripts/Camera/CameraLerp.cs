using UnityEngine;


public class CameraLerp : MonoBehaviour
{
    private Quaternion defaultRotation;
    private Quaternion bookcaseRotation;
    private Quaternion targetRotation;
    private Vector3 defaultPosition;
    private Vector3 targetPosition;
    private float rotationSpeed = 0.05f;
    private float positionSpeed = 0.1f;
    private bool isMoving = false;


    private void Awake()
    {      
        defaultPosition = new Vector3(-3.5f, 3, -7f);
        targetPosition = new Vector3(0, 0, 0);
        defaultRotation = Quaternion.Euler(15, 90f, 0); // rotating downwards
        bookcaseRotation = Quaternion.Euler(30, 90f, 0); // rotating downwards
        targetRotation = Quaternion.Euler(15f, 90f, 0);
    }


    private void Update()
    {
        // Move towards object
        if (isMoving)
        {
            // Lerp toward target rotation and position at all times.
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed);
            transform.position = Vector3.Lerp(transform.position, targetPosition, positionSpeed);

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
        targetPosition = hitInfo.transform.position - new Vector3(2.5f, -hitInfo.collider.bounds.size.y * 1.25f, 0);

        // If the object is past half way of the floor away from the player then increase the rotation angle to look down more on the object
        if (hitInfo.transform.tag.Equals("Bookcase"))
        {
            targetRotation = bookcaseRotation;
        }
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