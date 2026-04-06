using System.Collections;
using UnityEngine;

public class ResetOnFall : MonoBehaviour
{
    [Header("Default Transform")]
    public Vector3 defaultPosition;
    public Quaternion defaultRotation;

    [Header("Fall Detection")]
    [Tooltip("If object goes below this Y value, it will reset")]
    public float fallThresholdY = -1f;

    [Header("Collision Reset")]
    [Tooltip("Tag used to identify the floor")]
    public string floorTag = "Floor";

    [Tooltip("Enable reset when object collides with floor")]
    public bool useCollisionReset = false;

    [Header("Reset Settings")]
    [Tooltip("Delay before resetting (seconds)")]
    public float resetDelay = 0.5f;

    private Rigidbody rb;
    private bool isResetting = false;

    void Start()
    {
        // Store initial position & rotation
        defaultPosition = transform.position;
        defaultRotation = transform.rotation;

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 🔹 Reset if object falls below threshold
        if (!isResetting && transform.position.y < fallThresholdY)
        {
            StartCoroutine(ResetAfterDelay());
        }
    }

  

    IEnumerator ResetAfterDelay()
    {
        isResetting = true;

        yield return new WaitForSeconds(resetDelay);

        ResetObject();

        isResetting = false;
    }

    public void ResetObject()
    {
        if (rb != null)
        {
            // Stop movement
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Optional: stabilize physics
            rb.isKinematic = true;
        }

        // Reset position & rotation
        transform.position = defaultPosition;
        transform.rotation = defaultRotation;

        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }
}