using UnityEngine;
using UnityEngine.InputSystem;


public class InteractionHandler : MonoBehaviour
{
    private IRayProvider rayProvider;
    private ISelector selector;
    private RaycastHit hitInfo;

    //[SerializeField] private GameEventSO OnChangeCam;
    //[SerializeField] private GameEventSO OnEscapeDoorClicked;
    //[SerializeField] private GameEventSO OnKeyPadDoorClicked;

    [SerializeField] private RaycastHitEventSO OnRequestRaycastHitInfo;
    [SerializeField] private GameEventSO OnResetCameraPosition;


    // Start is called before the first frame update
    void Awake()
    {
        rayProvider = GetComponent<IRayProvider>();
        selector = GetComponent<ISelector>();
    }


    /// <summary>
    /// A method that is called when Left Mouse button is clicked
    /// Is used with the new Input System.
    /// This method gets the hit info from an object
    /// </summary>
    /// 
    public void OnSelectObject()
    {
        // Create a Ray and Check if it is touching an 'interactable' layered object,
        selector.Check(rayProvider.CreateRay()); 

        // If the Ray is touching an 'interactable' layered object, hence not null
        if (selector.GetSelection() != null) 
        {
            // Get the hit info
            hitInfo = selector.GetHitInfo(); 

            string gameObjectTag = hitInfo.collider.gameObject.tag;

            // Send the hitInfo to whatever listener is waiting for it like the CameraLerp script
            // that will use this Transform position to move the camera to this hitInfo position
            //OnRequestRaycastHitInfo.Raise(hitInfo);


            switch (gameObjectTag)
            {
                case "Furniture":
                case "Bookcase":
                case "Desk":
                    Debug.Log("Key Object: " + hitInfo.collider.gameObject.name + "! ++++++++++++++++++++++++++");
                    OnRequestRaycastHitInfo.Raise(hitInfo);
                    break;

                default:
                    Debug.Log("NOT a Key Object: " + hitInfo.collider.gameObject.name + "! ++++++++++++++++++++++++++");

                    //OnResetCameraPosition.Raise();
                    break;
            }


            //// Check the left click distance to the body and change camera's if close enough, else display alert text and return
            //if (hitInfo.collider.transform.parent.gameObject.CompareTag("OperationMan"))
            //{                  
            //    OnChangeCam.Raise(); // Raise event to change camera                   
            //}
            //// Check the left click distance to the keypad door, else display alert text and return
            //if (hitInfo.collider.gameObject.name == "KeyPadDoor") // If the hitInfo's object's name is 'KeyPadDoor'
            //{
            //    print("OnKeyPadDoorClicked ************************************");
            //    OnKeyPadDoorClicked.Raise();
            //}
        }
        else
        {
            OnResetCameraPosition.Raise();
        }
    }
}
