using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[System.Serializable]
public class DestinationEvent
{
    public int destinationID;
    public UnityEvent eventsToPlay;
    public bool stopPlayerMovement;

    private bool reachDestination;
    
    public bool ReachDestination
    {
        get
        {
            return reachDestination;
        }

        set
        {
            reachDestination = value;
        }
    }
}


[RequireComponent(typeof(NavMeshAgent))]
public class UnityEvent_GoToPosition : MonoBehaviour {

    NavMeshAgent agent;
    Animator anim;

    public List<Transform> targetToGo = new List<Transform>();

    public bool loop;

    public bool playEventWhenReachDestination;
    [ShowIf("playEventWhenReachDestination")]
    public List<DestinationEvent> destinationEvents = new List<DestinationEvent>();

    
    private bool canCheckDestination;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(anim != null)
            anim.SetFloat("velocidade", agent.velocity.magnitude);

        if (playEventWhenReachDestination)
        {
            foreach (DestinationEvent evt in destinationEvents)
            {
                if (evt.stopPlayerMovement && Vector3.Distance(agent.destination, targetToGo[evt.destinationID].position) < 2f && !evt.ReachDestination)
                {
                    Manager.instance.StopPlayerControlls(true);
                }

                if (agent.remainingDistance < 0.2f && !evt.ReachDestination && Vector3.Distance(agent.destination, targetToGo[evt.destinationID].position) < 2f && canCheckDestination)
                {
                    Debug.Log("Chegou!");

                    if (loop)
                    {
                        foreach (DestinationEvent evento in destinationEvents)
                        {
                            if (evento.ReachDestination)
                                evento.ReachDestination = false;
                        }
                    }
                    if (evt.stopPlayerMovement)
                    {
                       Manager.instance.StopPlayerControlls(false);
                    }
                       
                    evt.eventsToPlay.Invoke();
                    evt.ReachDestination = true;
                    
                }
            }
          
        }
    }

    public void PlayUnityEvent(int index)
    {

        Debug.Log("I'm Going to " + targetToGo[index].name);
        agent.SetDestination(targetToGo[index].position);
        StartCoroutine("CanCheckDestination");
    }

    IEnumerator CanCheckDestination()
    {
        canCheckDestination = false;
        yield return new WaitForEndOfFrame();
        canCheckDestination = true;

    }

    
}
