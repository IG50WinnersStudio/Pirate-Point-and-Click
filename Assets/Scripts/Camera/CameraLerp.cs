using UnityEngine;


public class CameraLerp : MonoBehaviour
{
    private Quaternion defaultRotation;
    private Quaternion bookcaseRotation;
    private Quaternion deskRotation;
    private Quaternion targetRotation;
    private Vector3 defaultPosition;
    private Vector3 targetPosition;
    private float rotationSpeed = 0.05f;
    private float positionSpeed = 0.05f;
    private bool isMoving = false;


    private void Awake()
    {      
        defaultPosition = new Vector3(-3.5f, 3f, -8f);
        targetPosition = new Vector3(0f, 0f, 0f);
        defaultRotation = Quaternion.Euler(15f, 90f, 0f); // rotating downwards
        targetRotation = Quaternion.Euler(15f, 90f, 0f);
        bookcaseRotation = Quaternion.Euler(30f, 90f, 0f); // rotating downwards
        deskRotation = Quaternion.Euler(20f, 40f, 0f);
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

        // If the object is past half way of the floor away from the player 
        // then increase the rotation angle to look down more on the object
        if (hitInfo.transform.tag.Equals("Bookcase"))
        {
            targetRotation = bookcaseRotation;
            // (x = distance from object, y = height, z = left or right of the object)
            targetPosition = hitInfo.transform.position - new Vector3(2f, -hitInfo.collider.bounds.size.y, 0f);
        }
        else if (hitInfo.transform.tag.Equals("Desk"))
        {
            targetRotation = deskRotation;
            targetPosition = hitInfo.transform.position - new Vector3(1.5f, -hitInfo.collider.bounds.size.y * 2f, 1.5f);
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