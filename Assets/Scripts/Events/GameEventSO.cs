using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameEvent", menuName = "Scriptable Objects/CustomEvents/GameEvent")]
public class GameEventSO : ScriptableObject
{
    [SerializeField]
    [ContextMenuItem("Reset Name", "ResetName")]
    private string Name;

    private List<GameEventListenerSO> listeners = new List<GameEventListenerSO>();

    [ContextMenu("Raise Event")]
    public virtual void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(GameEventListenerSO listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListenerSO listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}