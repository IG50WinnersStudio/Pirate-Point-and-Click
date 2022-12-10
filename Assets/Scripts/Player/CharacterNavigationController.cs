using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;


/// <summary>
/// Supports click to select and click to set waypoint on a mav mesh
/// </summary>
/// 
[RequireComponent(typeof(NavMeshAgent))]
public class CharacterNavigationController : MonoBehaviour
{
    [Header("Selection & Waypoint")]
    [SerializeField]
    [Tooltip("Set the game object used to indicate that the character using this controller is currently selected.")]
    private GameObject selectionPrefab;

    [SerializeField]
    [Tooltip("Set the game object used to indicate a waypoint for the character using this controller for navigation.")]
    private GameObject waypointPrefab;

    [SerializeField]
    [Tooltip("Used by the waypoint when the character is moving. Can be simple empty object.")]
    private GameObject sceneAnchor;

    [Header("Selected Object Buffer")]
    [SerializeField]
    [Tooltip("A scriptable object which holds a reference to the currently selected character")]
    private GameObjectVariable currentlySelectedGameObject;

    [Header("Materials")]
    [SerializeField] private Material frontal;
    [SerializeField] private Material sidal;
    [SerializeField] private MeshRenderer meshRenderer;

    [Header("Body")]
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject playerBody;

    [Header("Events")]
    [SerializeField] private RaycastHitEventSO OnMoveCamera;

    private NavMeshAgent navMeshAgent;
    private IRayProvider rayProvider;
    private ISelector selector;
    private RaycastHit hitInfo;
    private bool isSelected;
    private bool isWaypointActive;
    private bool canCameraMove;
    private float navAgentWalkSpeed = 1f;
    private float navAgentRunSpeed = 2f;


    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rayProvider = GetComponent<IRayProvider>();
        selector = GetComponent<ISelector>();
         
        SetSelected(false);
    }

    /// <summary>
    /// Called when a player left clicks mouse button and can select the on-screen player avatar
    /// </summary>
    /// 
    public void OnSelectPlayer(RaycastHit hitInfo)
    {
        Debug.Log(hitInfo.collider.gameObject);

        //if player is selected and we click and select a different player
        if (currentlySelectedGameObject.Value != null && currentlySelectedGameObject.Value != gameObject)
        {
            //de-select current
            currentlySelectedGameObject.Value = null;
            SetSelected(false);
        }

        //if player is selected then deselect
        if (isSelected)
        {
            //de-select current
            currentlySelectedGameObject.Value = null;
            SetSelected(false);
        }
        else // if player is not selected then select it
        {
            //set selected new player object
            SetSelected(true);
            currentlySelectedGameObject.Value = gameObject;
        }
    }

    /// <summary>
    /// Called when player selects a destination point on the navmesh
    /// </summary>
    /// 
    public void OnSelectWaypoint(InputAction.CallbackContext ctx)
    {
        //if a player is selected then determine destination
        if (isSelected && ctx.performed)
        {
            selector.Check(rayProvider.CreateRay());

            if (selector.GetSelection() != null)
            {
                hitInfo = selector.GetHitInfo();
                SetDestination(hitInfo.point);
                SetWaypoint();
                SetSelected(false);

                // Set the players material and sprite to an image of the player facing left
                meshRenderer.material = sidal;

                // If you click to the right of the player flip the Z scale to show right facing sprite
                if (hitInfo.point.z < transform.localPosition.z)
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 
                        transform.localScale.z * -1);
                else // Show left facing sprite by getting the absolute value of Z which is originaly facing left
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 
                        Math.Abs(transform.localScale.z));


                // If you right click on the floor (name = "NavMeshPlane") don't move the camera 
                if (hitInfo.transform.gameObject.name != "NavMeshPlane")
                    canCameraMove = true;


                // Clicking interactions, if tap you walk, if you hold you run
                if (ctx.interaction is TapInteraction) // Walk animation
                {
                    navMeshAgent.speed = navAgentWalkSpeed;
                    animator.SetBool("IsRunning", false);
                    animator.SetBool("IsWalking", true);
                }
                else if (ctx.interaction is HoldInteraction) // Run animation
                {
                    navMeshAgent.speed = navAgentRunSpeed;
                    animator.SetBool("IsWalking", false);
                    animator.SetBool("IsRunning", true);
                }
            }
        }
    }

    /// <summary>
    /// Move selected player towards active destination point
    /// </summary>
    /// 
    private void Update()
    {
        // If the players distance to the waypoint position and the waypoint prefab is active in the hierarchy
        // 0.947f is the navMeshAgent.baseOffset and height from ground
        if (isWaypointActive) 
        {
            if (Vector3.Distance(navMeshAgent.destination, transform.position) < 0.947f)
            {
                ClearWaypoint();
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsRunning", false);
                meshRenderer.material = frontal;

                if (canCameraMove)
                {
                    canCameraMove = false;

                    playerBody.transform.localPosition = Vector3.zero;
                    playerBody.transform.localRotation = Quaternion.identity;
                    playerBody.SetActive(false);

                    OnMoveCamera.Raise(hitInfo);
                }
            }
        }
    
    }

    #region Actions -  Set/Clear destination and waypoint

    /// <summary>
    /// Sets navmesh target
    /// </summary>
    /// <param name="target"></param>
    /// 
    private void SetDestination(Vector3 target)
    {
        navMeshAgent.SetDestination(target);
    }


    /// <summary>
    /// Set the next naviagable waypoint
    /// </summary>
    /// 
    private void SetWaypoint()
    {
        isWaypointActive = true;
        waypointPrefab.SetActive(true);
        waypointPrefab.transform.SetParent(sceneAnchor.transform);
        waypointPrefab.transform.position = navMeshAgent.destination;
    }

    /// <summary>
    /// Disable waypoint indicator and set waypoint transform back to attached player
    /// </summary>
    /// 
    private void ClearWaypoint()
    {
        isWaypointActive = false;
        waypointPrefab.SetActive(false);
        waypointPrefab.transform.SetParent(transform);
    }

    /// <summary>
    /// Set selected and show selection indicator around the player
    /// </summary>
    /// <param name="isSelected"></param>
    /// 
    public void SetSelected(bool isSelected)
    {
        this.isSelected = isSelected;
        selectionPrefab.SetActive(isSelected);
    }

    #endregion Actions -  Set/Clear destination and waypoint

}