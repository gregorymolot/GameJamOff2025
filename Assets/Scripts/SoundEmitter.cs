using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class SoundEmitter : MonoBehaviour
{
    SphereCollider sphere;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sphere = GetComponent<SphereCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Findable"))
        {
            other.GetComponent<Dissolve>().TryStartOutline(transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        other.GetComponent<Dissolve>().TryStartDissolve(transform);
    }

    void OnDrawGizmos()
    {
        if (sphere != null)
            Gizmos.DrawSphere(transform.position, sphere.radius);
    }
}
