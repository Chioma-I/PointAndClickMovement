using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement Settings")] //Kiara told me that this doesnt actually do anything, its for visual organisation in the inspector! Good to know
    public float speed = 5f; // Movement speed

    private Vector3 targetPosition; // Where the player is moving toward
    private bool isMoving = false;  // Whether movement is active
    //public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // --- Detect left mouse click ---
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray;

            if (Camera.main != null)
            {
                // Use main camera if it exists
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            }
            else
            {
                // Fallback ray if no camera is available
                ray = new Ray(transform.position + Vector3.up * 5f, Vector3.forward);
            }

            // Flat XZ plane at player's current height
            Plane plane = new Plane(Vector3.up, transform.position.y);

            float distance;
            if (plane.Raycast(ray, out distance))
            {
                // Calculate the point on the plane
                targetPosition = ray.GetPoint(distance);
                targetPosition.y = transform.position.y; // keep flat movement
                targetPosition.z = transform.position.z;
                isMoving = true;
            }
        }
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Stop moving when close enough
            if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
                isMoving = false;
        }
    }
}
