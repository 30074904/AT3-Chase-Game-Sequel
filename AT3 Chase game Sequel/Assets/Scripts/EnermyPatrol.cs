using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FiniteStateMachine;

public class EnermyPatrol : MonoBehaviour
{
    public enum EnermyState { patroling, waiting, stopped };
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

    public GameObject player;
    // were i end

    // Start is called before the first frame update
    private void Awake()
    {

        StateMachine = new StateMachine(new Patroling(this));

        chaseState = EnermyState.patroling;
        if (!TryGetComponent<NavMeshAgent>(out agent))
        {
            Debug.LogError("you didnt attach a mesh agent");
            gameObject.SetActive(false);
        }
    }
    void Start()
    {
        StateMachine.SetState(new Patroling(this));
        currWayPoint = 0;
        agent.isStopped = false;
        agent.SetDestination(wayPoints[currWayPoint].position);
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayer();

        if (chaseState == EnermyState.patroling)
        {
            CheckArrival();
            
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
    private void CheckPlayer()
    {
        RaycastHit hit;
        playerDirection = player.transform.position - transform.position;
        playerDirection = playerDirection.normalized;
        if (Physics.Raycast(transform.position, transform.TransformDirection(playerDirection), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(playerDirection) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        
    }
    private void GoToIdle()
    {

    }
    public enum EnermyID { patrol = 0, wait = 1, chase = 2 }
    public abstract class EnermyAttackState : IState
    {
        public EnermyID ID { get; protected set; }
        public EnermyAttackState(EnermyPatrol _instance)
        {
            instance = _instance;
        }
        protected EnermyPatrol instance;
        public virtual void OnEnter()
        {

        }
        public virtual void OnExit()
        {

        }
        public virtual void OnUpdate()
        {

        }
    }
    public class Patroling : EnermyAttackState
    {
        private NavMeshAgent agent;
        // list of waypoints to drive between
        [SerializeField] List<Transform> wayPoints = new List<Transform>();

        public GameObject enermy;
        private int currWayPoint;

        // were i start

        public StateMachine StateMachine { get; private set; }

        public GameObject player;
        public Patroling(EnermyPatrol _instance) : base(_instance)
        {
            ID = EnermyID.patrol;
        }

        public override void OnEnter()
        {
            Debug.Log("eatin");
        }
        public override void OnUpdate()
        {
            
        }
        public override void OnExit()
        {
            
        }
    }
    //[System.Serializable]

}
