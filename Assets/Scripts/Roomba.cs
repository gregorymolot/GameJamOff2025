using UnityEngine;

public class Roomba : MonoBehaviour
{
    Rigidbody rb;
    Vector3 velocity;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocity = new Vector3(Random.Range(-1f,1f), 0f, Random.Range(-1f,1f));
        rb.AddForce(velocity, ForceMode.VelocityChange);
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Floor"))
        {
            return;
        }
        Vector3 normal = collision.contacts[0].normal;
        normal.y = 0;
        normal = normal.normalized;
        velocity = Vector3.Reflect(velocity, normal);
        rb.linearVelocity = velocity;        
    }
    public void Reflect(Vector3 normal)
    {
        
    }
}
