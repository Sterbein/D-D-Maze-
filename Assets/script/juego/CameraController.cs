
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // The target object that the camera will follow
      public Transform target;
      // The speed at which the camera will move 
    public float smoothSpeed = 0.3F; 
    // The offset of the camera from the target object
    public Vector3 offset = new Vector3(0, 0, -10); 

    private void LateUpdate()
    {
         // Find the TurnManager object in the scene
        TurnManager turnManager = FindAnyObjectByType<TurnManager>();
        if (turnManager != null)
        {
        target = FindAnyObjectByType<LaberintoPrim>().playerTag[turnManager.index].transform;
        }

        // Follow the player
       Vector3 newPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed * Time.deltaTime);
    
    }
}
