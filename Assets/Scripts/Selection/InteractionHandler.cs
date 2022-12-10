using UnityEngine;
using UnityEngine.InputSystem;


public class InteractionHandler : MonoBehaviour
{
    private IRayProvider rayProvider;
    private ISelector selector;
    private RaycastHit hitInfo;

    [SerializeField] private RaycastHitEventSO OnPlayerLeftClicked;
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

            // If you click on the Players "Body" object 
            if (hitInfo.collider.gameObject.name == "Body")
            {
                // Raise event to deal with what happens when the Player is left clicked 
                OnPlayerLeftClicked.Raise(hitInfo); 
            }
            else
            {
                string gameObjectTag = hitInfo.collider.gameObject.tag;

                switch (gameObjectTag)
                {
                    case "Furniture":
                    case "Bookcase":
                    case "Desk":
                        Debug.Log("Key Object: " + hitInfo.collider.gameObject.name + "! ++++++++++++++++++");
                        OnRequestRaycastHitInfo.Raise(hitInfo);
                        break;

                    default:
                        Debug.Log("NOT a Key Object: " + hitInfo.collider.gameObject.name + "! +++++++++++++++++");
                        OnResetCameraPosition.Raise();
                        break;
                }
            }
        }
        else
        {
            OnResetCameraPosition.Raise();
        }
    }
}
