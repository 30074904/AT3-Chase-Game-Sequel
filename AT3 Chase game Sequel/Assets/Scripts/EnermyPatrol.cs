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

    public int leniancy = 0;

    public float speed;

    bool arrive = false;

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
        
        if (Input.GetKey(KeyCode.Q))
        {
            agent.SetDestination(transform.position);
        }
        else
        {
            if (chaseState == EnermyState.patroling)
            {
                EventManager.updateAnimationEvent(1);
                CheckArrival(false);
                angle = Vector3.Angle(transformPlayer, transform.forward);
                CheckPlayer(transformPlayer);
            }
            else if (chaseState == EnermyState.hunting)
            {
                EventManager.updateAnimationEvent(2);
                agent.speed = 10f;
                angle = Vector3.Angle(transformPlayer, transform.forward);
                HuntPlayer(transformPlayer);
            }
            else if (chaseState == EnermyState.waiting)
            {
                EventManager.updateAnimationEvent(0);
                angle = Vector3.Angle(transformPlayer, transform.forward);
                GoToIdle(transformPlayer);
            }
        }
        if (Input.GetKey(KeyCode.E))
        {
            agent.SetDestination(wayPoints[currWayPoint].position);
        }


    }
    private void CheckArrival(bool doneWaiting)
    {
        if (Vector3.Distance(transform.position, wayPoints[currWayPoint].position) < agent.stoppingDistance)
        {
            Debug.Log("have arrivived?");
            timer = 0;
            if (doneWaiting == false)
            {
                chaseState = EnermyState.waiting;
                EventManager.updateAnimationEvent(0);
            }

            if (doneWaiting == true)
            {
                doneWaiting = false;
                
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
        }
        else
        {
        }
    }
    private void CheckPlayer(Vector3 tPlayer)
    {
        RaycastHit hit;


        if (angle <= 80)
        {
            if (Physics.Raycast(transform.position, tPlayer, out hit, Mathf.Infinity))
            {
                Debug.DrawRay(transform.position, tPlayer * hit.distance, Color.yellow);
                if (hit.collider.tag == "Player")
                {
                    Debug.DrawRay(transform.position, tPlayer * hit.distance, Color.red);
                    chaseState = EnermyState.hunting;
                    agent.SetDestination(player.transform.position);
                }


            }
        }
        
        
    }
    private void HuntPlayer(Vector3 tPlayer)
    {
        RaycastHit hit;

        if (leniancy > 0)
        {
            leniancy--;
        }
        if (angle <= 80)
        {
            if (Physics.Raycast(transform.position, tPlayer, out hit, Mathf.Infinity))
            {
                Debug.DrawRay(transform.position, tPlayer * hit.distance, Color.yellow);
                if (hit.collider.tag == "Player")
                {
                    leniancy = 50;
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
                else if (chaseState == EnermyState.hunting && leniancy > 0)
                {
                    agent.SetDestination(player.transform.position);
                }
                else
                {
                    agent.speed = 3.5f;
                    chaseState = EnermyState.patroling;
                    agent.SetDestination(wayPoints[currWayPoint].position);
                }

            }
        }
    }
    private void GoToIdle(Vector3 tPlayer)
    {
        EventManager.updateAnimationEvent(0);

        timer += Time.deltaTime;

        RaycastHit contact;

        if (angle <= 180)
        {
            if (Physics.Raycast(transform.position, tPlayer, out contact, Mathf.Infinity))
            {
                Debug.DrawRay(transform.position, tPlayer * contact.distance, Color.yellow);
                if (contact.collider.tag == "Player")
                {
                    Debug.DrawRay(transform.position, tPlayer * contact.distance, Color.red);
                    if (chaseState != EnermyState.hunting)
                    {
                        chaseState = EnermyState.hunting;
                        agent.SetDestination(player.transform.position);
                    }

                }
                else if (timer > timerMax)
                {
                    chaseState = EnermyState.patroling;
                    CheckArrival(true);
                    timer = 0;
                }
            }
            
        }
        else if (timer > timerMax)
        {
            chaseState = EnermyState.patroling;
            CheckArrival(true);
            timer = 0;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log("you lose");
        }
    }

}
