using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Roomba : BaseSoundEmitter
{
    [SerializeField]
    List<Destination> positions;
    int positionIndex = 0;
    Destination currentDestination;
    NavMeshAgent agent;
    float startingSpeed;
    bool stopping;

    bool increasing = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false;
        agent.updateRotation = false;
        currentDestination = positions[positionIndex];
        agent.SetDestination(currentDestination.transform.position);
        startingSpeed = agent.speed;
    }

        void Update()
    {
        if (agent.remainingDistance < agent.stoppingDistance && stopping == false && currentDestination.stopping)
        {
            StartCoroutine(Wait());
        }
        // else if(agent.remainingDistance < agent.stoppingDistance)
        // {
        //     if ((positionIndex == 0 && increasing == false )|| (positionIndex == positions.Count-1 && increasing == true))
        //     {
        //         increasing = !increasing;
        //     }
        //     positionIndex = increasing ? positionIndex + 1 : positionIndex - 1;
        //     currentDestination = positions[positionIndex];
        //     agent.SetDestination(currentDestination.transform.position);

        // }
    }

    IEnumerator Wait()
    {
        stopping = true;
        agent.speed = 0f;
        yield return new WaitForSeconds(3f);
        agent.speed = startingSpeed;
        if ((positionIndex == 0 && increasing == false )|| (positionIndex == positions.Count-1 && increasing == true))
        {
            increasing = !increasing;
        }
        positionIndex = increasing ? positionIndex + 1 : positionIndex - 1;
        currentDestination = positions[positionIndex];
        agent.SetDestination(currentDestination.transform.position);
        stopping = false;
    }

    // void Start()
    // {
    //     rb = GetComponent<Rigidbody>();
    //     rb.maxLinearVelocity = 2f;
    //     velocity = new Vector3(Random.Range(-1f,1f), 0f, Random.Range(-1f,1f));
    //     velocity = velocity.normalized;
    //     velocity = velocity * speed;
    //     rb.AddForce(velocity, ForceMode.VelocityChange);
    // }
    // void OnCollisionEnter(Collision collision)
    // {
    //     if(collision.collider.CompareTag("Floor"))
    //     {
    //         return;
    //     }
    //     Vector3 normal = collision.contacts[0].normal;
    //     normal.y = 0;
    //     normal = normal.normalized;
    //     velocity = Vector3.Reflect(velocity, normal);
    //     velocity.y = 0;
    //     velocity.x += Random.Range(-0.1f,0.1f);
    //     velocity.z += Random.Range(-0.1f,0.1f);
    //     velocity = velocity.normalized;
    //     velocity = velocity * speed;

    //     rb.linearVelocity = velocity;
    // }
}
