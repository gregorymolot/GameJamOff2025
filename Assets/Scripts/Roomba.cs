using UnityEngine;

public class Roomba : MonoBehaviour
{
    Rigidbody rb;
    Vector3 velocity;
    float speed = 5f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxLinearVelocity = 5f;
        velocity = new Vector3(Random.Range(-1f,1f), 0f, Random.Range(-1f,1f));
        velocity = velocity.normalized;
        velocity = velocity * speed;
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
        velocity.y = 0;
        velocity.x += Random.Range(-0.1f,0.1f);
        velocity.z += Random.Range(-0.1f,0.1f);
        velocity = velocity.normalized;
        velocity = velocity * speed;

        rb.linearVelocity = velocity;
    }
    
    public void Reflect(Vector3 normal)
    {
        
    }
}
