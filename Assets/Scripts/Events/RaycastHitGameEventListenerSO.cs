using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class MyGameObjectEvent : UnityEvent<RaycastHit>
{
}


public class RaycastHitGameEventListenerSO : MonoBehaviour
{
    [SerializeField] private string description;
    [Tooltip("Specify the game event (scriptable object) which will raise the event")]
    [SerializeField] private RaycastHitEventSO gameEventHitInfo;
    [SerializeField] private MyGameObjectEvent ResponseHitInfo;


    private void OnEnable() => gameEventHitInfo.RegisterListener(this);


    private void OnDisable() => gameEventHitInfo.UnregisterListener(this);


    public void OnRaycastHitEventRaised(RaycastHit hitInfo) => ResponseHitInfo?.Invoke(hitInfo);
}