using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FiniteStateMachine;

public class EnermyPatrol : MonoBehaviour
{
    public enum EnermyState { patroling, waiting, hunting };
    public EnermyState chaseState;

    private NavMeshAgent agent;
    // list of waypoints to drive between
    [SerializeField] List<Transform> wayPoints = new List<Transform>();

    public Vector3 playerDirection;

    private int currWayPoint;

    // were i start
    public float timerMax = 5;
    public float timer;
    public StateMachine StateMachine { get; private set; }
    public Vector3 transformPlayer;
    public GameObject player;

    public float angle;

    private int leniancy = 0;

    public float speed;

    // were i end

    // Start is called before the first frame update
    private void Awake()
    {


        chaseState = EnermyState.patroling;
        if (!TryGetComponent<NavMeshAgent>(out agent))
        {
            Debug.LogError("you didnt attach a mesh agent");
            gameObject.SetActive(false);
        }

    }
    void Start()
    {

        currWayPoint = 0;
        agent.isStopped = false;
        agent.SetDestination(wayPoints[currWayPoint].position);
    }

    // Update is called once per frame
    void Update()
    {
        transformPlayer = player.transform.position - transform.position;
        

        if (chaseState == EnermyState.patroling)
        {
            CheckArrival();
            angle = Vector3.Angle(transformPlayer, transform.forward);
            CheckPlayer(transformPlayer);
        }
        else if (chaseState == EnermyState.hunting)
        {
            agent.speed = 10f;
            angle = Vector3.Angle(transformPlayer, transform.forward);
            CheckPlayer(transformPlayer);
        }

    }
    private void CheckArrival()
    {

        timer += Time.deltaTime;

        if (timer > timerMax)
        {
            if (Vector3.Distance(transform.position, wayPoints[currWayPoint].position) < agent.stoppingDistance)
            {
                if (currWayPoint < wayPoints.Count - 1)
                {
                    currWayPoint++;
                    agent.SetDestination(wayPoints[currWayPoint].position);
                }
                else
                {
                    currWayPoint = 0;
                    agent.SetDestination(wayPoints[currWayPoint].position);
                }
            }
            else
            {
                timer = 0;

            }
        }

        

        
    }
    private void CheckPlayer(Vector3 tPlayer)
    {
        RaycastHit hit;


        if (angle <= 60)
        {
            if (Physics.Raycast(transform.position, tPlayer, out hit, Mathf.Infinity))
            {
                Debug.DrawRay(transform.position, tPlayer * hit.distance, Color.yellow);
                if (hit.collider.tag == "Player")
                {
                    Debug.DrawRay(transform.position, tPlayer * hit.distance, Color.red);
                    if (chaseState != EnermyState.hunting)
                    {
                        chaseState = EnermyState.hunting;
                    }
                    else
                    {
                        agent.SetDestination(player.transform.position);
                    }
                    
                }
                else if (chaseState != EnermyState.patroling)
                {
                    leniancy++;
                    agent.SetDestination(player.transform.position);

                    if (leniancy >= 100)
                    {
                        agent.SetDestination(wayPoints[currWayPoint].position);
                        chaseState = EnermyState.patroling;
                        leniancy = 100;
                    }
                    
                }

            }
        }
        
        
    }
    private void GoToIdle()
    {

    }
   
}
