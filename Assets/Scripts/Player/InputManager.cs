using UnityEngine;


public class InputManager : MonoBehaviour
{
    private Rigidbody2D rb;

    private bool isGamePaused;
    
    private PlayerControls controls;
    private PlayerControls.PlayerActions playerActions;

    [SerializeField] private GameEventSO OnLoadPauseScene;
    [SerializeField] private GameEventSO OnUnloadPauseSceneWithKey;

    [SerializeField] private Movement movement;

    // Reference to Audio Source
    [SerializeField] private AudioSource audioSource;

   
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Hide mouse cursor at the centre of screen when in game
        Cursor.lockState = CursorLockMode.Locked;

        // Set time scale to normal speed
        Time.timeScale = 1;


        #region PLAYER ACTIONS INITIALIZATION

        controls = new PlayerControls();
        playerActions = controls.Player;

        // Pause pressed
        playerActions.Pause.performed += _ => OnPausePressed();


        #endregion PLAYER ACTIONS INITIALIZATION
    }


    public void OnCrossedFinishLine()
    {    
        // Stop controls of player
        controls.Player.Disable();
    }


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

        // Hide mouse cursor at the centre of screen when go back to game
        Cursor.lockState = CursorLockMode.Locked;

        // Set time scale back to normal speed
        Time.timeScale = 1;

        // Enable controls of player
        controls.Player.Enable();
    }


    private void OnEnable()
    {
        controls.Enable();
    }


    private void OnDisable()
    {
        controls.Disable();
    }
}