using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Horse_Move : MonoBehaviour
{
    public Transform targetDestination;
    public GameObject myHorse_Model;
    Animator_Controller my_horseModel_Script;

    NavMeshAgent agent;

    float horseSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("The horse is ready to move");
        agent = this.GetComponent<NavMeshAgent>();

        horseSpeed = agent.speed;
        my_horseModel_Script = myHorse_Model.GetComponent<Animator_Controller>();

        agent.SetDestination(targetDestination.position);
    }

    // Update is called once per frame
    void Update()
    {

        if (agent.remainingDistance == 0 && agent.remainingDistance != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            my_horseModel_Script.horseStandIdle();
        } else
        {
            
            Debug.Log(agent.remainingDistance);

            // If the remaining distance isn't big enough, slow the movement of the horse.
            if (agent.remainingDistance != Mathf.Infinity)
            {
                agent.speed = 5;
                my_horseModel_Script.horseWalk();
            }
            else
            {
                my_horseModel_Script.horseTrot();
            }
        } 
    }

}
