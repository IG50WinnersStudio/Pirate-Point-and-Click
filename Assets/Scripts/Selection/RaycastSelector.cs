using UnityEngine;


public class RaycastSelector : MonoBehaviour, ISelector
{
    private Transform selection;
    private RaycastHit hitInfo;
    
    [SerializeField] [Range(0, 100)] private float maxDistance = 100;
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private LayerMask layerMask;


    public Transform GetSelection()
    {
        return selection;
    }


    public RaycastHit GetHitInfo()
    {
        return hitInfo;
    }


    public void Check(Ray ray)
    {
        selection = null;
        if (Physics.Raycast(ray, out hitInfo, maxDistance, layerMask.value))
        {
            selection = hitInfo.transform;

            //var currentSelection = hitInfo.transform;

            //if (currentSelection.CompareTag(selectableTag))
            //{
            //    selection = currentSelection;
            //}
        }
    }
}