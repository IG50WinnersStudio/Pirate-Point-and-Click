using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "RaycastHitEventSO", menuName = "Scriptable Objects/CustomEvents/RaycastHit"), System.Serializable]
public class RaycastHitEventSO : ScriptableObject
{
    [SerializeField]
    [ContextMenuItem("Reset Name", "ResetName")]
    private string Name;

    private List<RaycastHitGameEventListenerSO> listeners = new List<RaycastHitGameEventListenerSO>();


    public void Raise(RaycastHit hitInfo)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnRaycastHitEventRaised(hitInfo);
        }
    }


    public void RegisterListener(RaycastHitGameEventListenerSO gameEventListener)
    {
        if (!listeners.Contains(gameEventListener))
            listeners.Add(gameEventListener);
    }


    public void UnregisterListener(RaycastHitGameEventListenerSO gameEventListener)
    {
        if (listeners.Contains(gameEventListener))
            listeners.Remove(gameEventListener);
    }
}