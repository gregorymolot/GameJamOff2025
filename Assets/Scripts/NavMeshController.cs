using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
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

    bool started;
    EventInstance whistle;
    [SerializeField]
    bool officer1;

    void Start()
    {
        TryGetComponent<Animator>(out animator);
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(StopWhistle());
    }

    IEnumerator StopWhistle()
    {
        yield return new WaitForSeconds(2f);
        whistle.stop(STOP_MODE.IMMEDIATE);
    }

    void OnEnable()
    {
        EventManager.Game.EndCutscene += StartRoute;
    }

    void OnDisable()
    {
        EventManager.Game.EndCutscene -= StartRoute;
    }

    void Update()
    {
        if (started)
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
    }

    void StartRoute()
    {
        started = true;
        currentDestination = positions[positionIndex];
        agent.SetDestination(currentDestination.transform.position);
        startingSpeed = agent.speed;
        whistle = officer1 ? GameAudioManager.Instance.CreateInstance(FMODEvents.Instance.officer1Whistle, gameObject, Room.OpenSpace, false) : GameAudioManager.Instance.CreateInstance(FMODEvents.Instance.officer2Whistle, gameObject, Room.OpenSpace, false);
        whistle.start();
    }

    IEnumerator Wait()
    {
        stopping = true;
        if (animator != null)
            animator.SetBool("AtDestination", true);
        agent.speed = 0f;
        yield return new WaitForSeconds(currentDestination.stopTime);
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
        whistle.stop(STOP_MODE.IMMEDIATE);
    }

    public void Resume()
    {
        agent.speed = startingSpeed;
        whistle.start();
    }

}