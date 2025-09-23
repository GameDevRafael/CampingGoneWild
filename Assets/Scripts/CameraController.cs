using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target; // Assign in Inspector
    public float speed = 5f;

    void Start()
    {
        
    }


    
    void Update()
    {
        // Get the target position but keep the follower's original Z position
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

        // Move towards that position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
