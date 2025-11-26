using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshController : MonoBehaviour
{
    [SerializeField]
    List<Destination> positions;
    int positionIndex = 0;
    Destination currentDestination;
    NavMeshAgent agent;
    float startingSpeed;

    Animator animator;
    bool stopping;

    void Start()
    {
        TryGetComponent<Animator>(out animator);
        agent = GetComponent<NavMeshAgent>();
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
        else if(agent.remainingDistance < agent.stoppingDistance && currentDestination.stopping == false)
        {
            positionIndex = (positionIndex + 1 + positions.Count) % positions.Count;
            currentDestination = positions[positionIndex];
            agent.SetDestination(currentDestination.transform.position);
        }
    }

    IEnumerator Wait()
    {
        stopping = true;
        if (animator != null)
            animator.SetBool("AtDestination", true);
        agent.speed = 0f;
        yield return new WaitForSeconds(3f);
        Debug.Log("Done");
        agent.speed = startingSpeed;
        if (animator != null)
            animator.SetBool("AtDestination", false);
        positionIndex = (positionIndex + 1 + positions.Count) % positions.Count;
        currentDestination = positions[positionIndex];
        agent.SetDestination(currentDestination.transform.position);
        stopping = false;
    }

    public void Stop()
    {
        agent.speed = 0f;
    }

    public void Resume()
    {
        agent.speed = startingSpeed;
    }

}