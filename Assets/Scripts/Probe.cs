using UnityEngine;

public class Probe : MonoBehaviour
{
    public enum ProbeType { Red, Black }
    public ProbeType probeType;

    public string currentTouch;

    private void OnTriggerEnter(Collider other)
    {
        currentTouch = other.gameObject.name;
    print($"{probeType} probe touched {currentTouch} Collider is - {other.name}");
        InteractionManager.Instance.UpdateProbe(this, other);
    }

    private void OnTriggerExit(Collider other)
    {
        currentTouch = null;

        InteractionManager.Instance.UpdateProbe(this, null);
    }

    // void OnCollisionEnter(Collision collision)
    // {
    //     currentTouch = collision.gameObject.name;
    //     print($"{probeType} probe touched {currentTouch} Collider is - {collision.collider.name}");
    //      InteractionManager.Instance.UpdateProbe(this, collision.collider);
    // }

    // void OnCollisionExit(Collision collision)
    //      {
    //        currentTouch = null;

    //      InteractionManager.Instance.UpdateProbe(this, null);
    // }
}