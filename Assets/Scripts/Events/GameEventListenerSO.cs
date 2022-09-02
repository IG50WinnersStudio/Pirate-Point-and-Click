using UnityEngine;
using UnityEngine.Events;


public class GameEventListenerSO : MonoBehaviour
{
    [SerializeField] private string description;
    [SerializeField] private UnityEvent Response;

    [SerializeField]
    [Tooltip("Specify the game event (scriptable object) which will raise the event")]
    private GameEventSO Event;


    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public virtual void OnEventRaised()
    {
        Response?.Invoke();
    }
}