using UnityEngine;


public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] Vector3 offset;
    [Range(1, 10)] [SerializeField] private float smoothFactor;



    private void Start()
    {
        offset = new Vector3(2, 0, -10);
    }


    private void FixedUpdate()
    {
        if (playerTransform != null)
        {
            Vector3 targetPosition = playerTransform.position + offset;
            Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);
            transform.position = smoothPosition;

            //transform.position = Vector3.Lerp(playerTransform.position, transform.position, 0.1f * Time.fixedDeltaTime) + offset;
        }
    }
}