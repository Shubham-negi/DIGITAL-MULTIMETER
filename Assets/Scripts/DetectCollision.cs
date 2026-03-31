using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    // 🔹 Trigger detection
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("[TRIGGER] " + gameObject.name + " hit " + other.gameObject.name);
    }

    // 🔹 Physical collision detection
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("[COLLISION] " + gameObject.name + " hit " + collision.gameObject.name);
    }
}