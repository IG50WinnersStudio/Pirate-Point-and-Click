using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;


public class InputManager : MonoBehaviour
{
    private Rigidbody2D rb;

    private bool isGamePaused;
    
    private PlayerControls controls;
    private PlayerControls.PlayerActions playerActions;

    [SerializeField] private GameEventSO OnLoadPauseScene;
    [SerializeField] private GameEventSO OnUnloadPauseSceneWithKey;
    [SerializeField] private GameEventSO OnCheckForSelectee;
    [SerializeField] private InputContextEventSO OnPlayerRightClicked;
    //[SerializeField] private GameEventSO MakeCameraRoll;

    //[SerializeField] private Movement movement;

    // Reference to Audio Source
    [SerializeField] private AudioSource audioSource;

   
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Set time scale to normal speed
        Time.timeScale = 1;


        #region PLAYER ACTIONS INITIALIZATION

        controls = new PlayerControls();
        playerActions = controls.Player;

        // Pause pressed
        playerActions.Pause.performed += _ => OnPausePressed();

        // Left Mouse button pressed
        playerActions.Select.performed += _ => OnLeftMouseBtnClicked();

        // Right Mouse button pressed
        playerActions.Move.performed += ctx => OnRightMouseBtnClicked(ctx);

        #endregion PLAYER ACTIONS INITIALIZATION
    }


    #region PAUSE

    private void OnPausePressed()
    {
        Debug.Log("Pause key pressed to Pause!!!!!!!!!");

        // If the game is not paused
        if (isGamePaused == false)
        {
            isGamePaused = true;

            Debug.Log("Pause key pressed to Pause!!!!!!!!!");

            // Pause the ingame music
            audioSource.Pause();

            // Show mouse cursor when in menus
            Cursor.lockState = CursorLockMode.None;

            // Set time scale to zero speed which stops any player movement
            Time.timeScale = 0;

            // Raise event to the LevelManager method 'LoadPauseMenu'
            OnLoadPauseScene.Raise();

            // Stop controls of player
            controls.Player.Disable();
            controls.Player.Pause.Enable();
        }
        else
        {
            OnUnloadPauseSceneWithKey.Raise();
            OnUnloadPauseScene();
        }
    }


    public void OnUnloadPauseScene()
    {
        isGamePaused = false;

        // Resume the ingame music
        audioSource.Play();

        // Set time scale back to normal speed
        Time.timeScale = 1;

        // Enable controls of player
        controls.Player.Enable();
    }

    #endregion PAUSE


    #region MOUSE CONTROLS

    public void OnLeftMouseBtnClicked()
    {
        OnCheckForSelectee.Raise();
    }


    public void OnRightMouseBtnClicked(InputAction.CallbackContext ctx)
    {
        OnPlayerRightClicked.Raise(ctx);
    }

    #endregion MOUSE CONTROLS


    #region ENABLING

    private void OnEnable()
    {
        controls.Enable();
    }


    private void OnDisable()
    {
        controls.Disable();
    }

    #endregion ENABLING
}