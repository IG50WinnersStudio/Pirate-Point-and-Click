using UnityEngine;


public interface ISelector
{
    Transform GetSelection();
    RaycastHit GetHitInfo();

    void Check(Ray ray);
}