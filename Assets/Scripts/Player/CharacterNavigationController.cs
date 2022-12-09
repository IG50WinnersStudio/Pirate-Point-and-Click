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

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private IRayProvider rayProvider;
    private ISelector selector;
    private RaycastHit hitInfo;
    private bool isSelected;
    private float navAgentWalkSpeed = 1f;
    private float navAgentRunSpeed = 2f;


    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rayProvider = GetComponent<IRayProvider>();
        selector = GetComponent<ISelector>();
        animator = GameObject.FindGameObjectWithTag("Animator").GetComponent<Animator>();

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
        // If the players distance to the waypoint position. 0.95f is the navMeshAgent.baseOffset and height from ground
        if (Vector3.Distance(navMeshAgent.destination, transform.position) <= 0.952f) // (navMeshAgent.stoppingDistance + navMeshAgent.baseOffset)
        {
            ClearWaypoint();
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsRunning", false);
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
