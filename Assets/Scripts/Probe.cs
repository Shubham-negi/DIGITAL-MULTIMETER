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
}