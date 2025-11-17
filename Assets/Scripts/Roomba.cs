using UnityEngine;

public class Roomba : MonoBehaviour
{
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 velocity = new Vector3(Random.Range(0f,1f), 0f, Random.Range(0f,1f));
        rb.linearVelocity = velocity.normalized * 2f;
    }
    void OnCollisionEnter(Collision collision)
    {
        Vector3 velocity = rb.linearVelocity;
            Debug.DrawRay(collision.contacts[0].point, collision.contacts[0].normal, Color.red, 4f);
            Vector3 normal = collision.contacts[0].normal;
            normal.y = 0;
            normal.Normalize();
            velocity = velocity - 2.0f * Vector3.Dot(velocity, normal) * normal;

            velocity.y = 0f;
            velocity = velocity.normalized * 2f;

            rb.linearVelocity = velocity;
            
    }
}
