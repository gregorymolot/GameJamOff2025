using UnityEngine;

public class BaseSoundEmitter : MonoBehaviour
{
    public Room assignedRoom;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Findable"))
        {
            Dissolve dissolve = other.GetComponentInParent<Dissolve>();
            if (dissolve.room == assignedRoom)
            {
                dissolve.TryStartOutline(transform);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Findable"))
        {
            Dissolve dissolve = other.GetComponentInParent<Dissolve>();
            if (dissolve.room == assignedRoom)
            {
                dissolve.TryStartDissolve(transform);
            }
        }
    }
}
