using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputContextEventSO", menuName = "Scriptable Objects/CustomEvents/InputContext"), System.Serializable]
public class InputContextEventSO : ScriptableObject
{
    [SerializeField]
    [ContextMenuItem("Reset Name", "ResetName")]
    private string Name;

    private List<InputContextGameEventListenerSO> listeners = new List<InputContextGameEventListenerSO>();


    public void Raise(InputAction.CallbackContext ctx)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnInputContextEventRaised(ctx);
        }
    }


    public void RegisterListener(InputContextGameEventListenerSO gameEventListener)
    {
        if (!listeners.Contains(gameEventListener))
            listeners.Add(gameEventListener);
    }


    public void UnregisterListener(InputContextGameEventListenerSO gameEventListener)
    {
        if (listeners.Contains(gameEventListener))
            listeners.Remove(gameEventListener);
    }
}