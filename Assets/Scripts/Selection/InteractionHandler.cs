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


    // Start is called before the first frame update
    void Awake()
    {
        rayProvider = GetComponent<IRayProvider>();
        selector = GetComponent<ISelector>();
    }


    /// <summary>
    /// A method that is called when Left Mouse button clicked
    /// Is used with the new Input System.
    /// This method gets the hit info from an object
    /// </summary>
    /// <param name="context"></param>
    public void OnSelectObject()
    {
        //Debug.Log("In OnSelectObject! ++++++++++++++++++++++++++");
        // Create a Ray and Check if it is pointing at a selectable object,
        // if so then set the hitInfo transform to the 'selector' Class local variable 'selection'
        selector.Check(rayProvider.CreateRay()); 

        // If the hitInfo.Transform is not null
        if (selector.GetSelection() != null) 
        {
            hitInfo = selector.GetHitInfo(); // Get the hit info

            Debug.Log(hitInfo.collider.gameObject.name + "! ++++++++++++++++++++++++++");

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
    }
}
