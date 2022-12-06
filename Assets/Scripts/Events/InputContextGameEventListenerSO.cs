using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[System.Serializable]
public class InputContextUnityResponse : UnityEvent<InputAction.CallbackContext>
{
}


public class InputContextGameEventListenerSO : MonoBehaviour
{
    [SerializeField] private string description;
    [Tooltip("Specify the game event (scriptable object) which will raise the event")]
    [SerializeField] private InputContextEventSO gameEventInputContext;
    [SerializeField] private InputContextUnityResponse ResponseInputContext;


    private void OnEnable() => gameEventInputContext.RegisterListener(this);


    private void OnDisable() => gameEventInputContext.UnregisterListener(this);


    public void OnInputContextEventRaised(InputAction.CallbackContext ctx) => ResponseInputContext?.Invoke(ctx);
}