using UnityEngine;

public class Probe : MonoBehaviour
{
    public enum ProbeType { Red, Black }
    public ProbeType probeType;

    public string currentTouch;

    private void OnTriggerEnter(Collider other)
    {
        currentTouch = other.gameObject.name;

        InteractionManager.Instance.UpdateProbe(this, other);
    }

    private void OnTriggerExit(Collider other)
    {
        currentTouch = null;

        InteractionManager.Instance.UpdateProbe(this, null);
    }

    // void OnCollisionEnter(Collision collision)
    // {
    //      currentTouch = collision.gameObject.name;

    //     InteractionManager.Instance.UpdateProbe(this, collision);
    // }

    // void OnCollisionExit(Collision collision)
    // {
    //       currentTouch = null;

    //     InteractionManager.Instance.UpdateProbe(this, null);
    // }
}