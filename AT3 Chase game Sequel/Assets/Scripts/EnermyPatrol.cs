using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FiniteStateMachine;

public class EnermyPatrol : MonoBehaviour
{
    public enum EnermyState { patroling, waiting, hunting, stun, hunt };
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

    public int idleAngle = 180;

    bool arrive = false;

    public float currstun;

    private float maxStun;

    private bool firstIdle;

    private bool keyStolen;
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
        EventManager.ArtifactStolenEvent += KeyStolen;
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
            if (keyStolen == true && chaseState != EnermyState.stun)
            {
                angle = Vector3.Angle(transformPlayer, transform.forward);
                agent.SetDestination(player.transform.position);
                EventManager.updateAnimationEvent(4);
                agent.speed = 8f;
                
                chaseState = EnermyState.hunt;
            }
            else if (chaseState == EnermyState.patroling)
            {
                EventManager.updateAnimationEvent(2);
                CheckArrival(false);
                angle = Vector3.Angle(transformPlayer, transform.forward);
                CheckPlayer(transformPlayer);
            }
            else if (chaseState == EnermyState.hunting)
            {
                EventManager.updateAnimationEvent(4);
                agent.speed = 8f;
                angle = Vector3.Angle(transformPlayer, transform.forward);
                HuntPlayer(transformPlayer);
            }
            else if (chaseState == EnermyState.waiting)
            {
                EventManager.updateAnimationEvent(0);
                angle = Vector3.Angle(transformPlayer, transform.forward);
                GoToIdle(transformPlayer);
            }
            else if (chaseState == EnermyState.stun)
            {
                GoStun(maxStun);
            }
        }
        if (Input.GetKey(KeyCode.E))
        {
            agent.SetDestination(wayPoints[currWayPoint].position);
        }


    }
    private void CheckArrival(bool doneWaiting)
    {
        agent.speed = 3.5f;
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
        

        if (firstIdle == true)
        {
            firstIdle = false;
            timerMax = Random.Range(3, 10);
        }

        EventManager.updateAnimationEvent(0);

        timer += Time.deltaTime;

        RaycastHit contact;

        if (angle <= idleAngle)
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
                    firstIdle = true;
                    timer = 0;
                }
            }
            
        }
        else if (timer > timerMax)
        {
            chaseState = EnermyState.patroling;
            CheckArrival(true);
            firstIdle = true;
            timer = 0;
        }

    }

    public void GoStun(float stunTime)
    {
        EventManager.updateAnimationEvent(3);
        agent.SetDestination(transform.position);

        chaseState = EnermyState.stun;
        Debug.Log("hit Enermy");

        maxStun = stunTime;

        currstun += Time.deltaTime;

        if (currstun >= maxStun)
        {
            agent.SetDestination(wayPoints[currWayPoint].position);
            chaseState = EnermyState.patroling;
            currstun = 0f;

        }

    }
    private void KeyStolen(bool stolen)
    {
        
        if (stolen == true)
        {
            Debug.Log("???");
            keyStolen = true;
        }
    }

    
    

}
