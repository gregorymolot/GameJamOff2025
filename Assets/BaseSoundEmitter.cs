using System;
using UnityEngine;

public class BaseSoundEmitter : MonoBehaviour
{
    public Room assignedRoom;

    public Action<Transform> EndAction;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Findable") || other.CompareTag("Door"))
        {
            Dissolve dissolve = other.GetComponentInParent<Dissolve>();
            if (dissolve.room == assignedRoom)
            {
                EndAction += dissolve.TryStartDissolve;
                dissolve.TryStartOutline(transform);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Findable") || other.CompareTag("Door"))
        {
            Dissolve dissolve = other.GetComponentInParent<Dissolve>();
            if (dissolve.room == assignedRoom)
            {
                EndAction -= dissolve.TryStartDissolve;
                dissolve.TryStartDissolve(transform);
            }
        }
    }
}
