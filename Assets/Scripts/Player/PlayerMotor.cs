using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour {


    Transform target;
    NavMeshAgent agent;
    Animator playerAnim;



	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        playerAnim = GetComponent<Animator>();
	}


    private void Update()
    {
        if(target != null)
        {
            agent.SetDestination(target.position);
            FaceTarget();
        }


        playerAnim.SetFloat("velocidade", agent.velocity.magnitude);
        //Debug.Log(agent.velocity.magnitude);
    }


    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }


    public void FollowTarget(InteractableOdin newTarget)
    {
        agent.stoppingDistance = newTarget.radius * 0.5f;
        agent.updateRotation = false;
        target = newTarget.interactionTransform;
    }

    public void StopFollowTarget()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
        target = null;
    }

    void FaceTarget()
    {

        Vector3 direction = (target.position - transform.position).normalized;

        if (direction.x != 0 && direction.z != 0)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
         
    }

}
