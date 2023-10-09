using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnermyPatrol : MonoBehaviour
{
    public enum DrivingState { driving, waiting, stopped };
    public DrivingState drivingState;

    private NavMeshAgent agent;
    // list of waypoints to drive between
    [SerializeField] List<Transform> wayPoints = new List<Transform>();

    private int currWayPoint;
    // Start is called before the first frame update
    private void Awake()
    {
        drivingState = DrivingState.driving;
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
        if (drivingState == DrivingState.driving)
        {
            CheckArrival();
        }

    }
    private void CheckArrival()
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
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ICarCollision>(out ICarCollision carCollision))
        {
            Debug.Log("car has stopped due to somthing in front of it.");
            carCollision.Activate(this);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<ICarCollision>(out ICarCollision carCollision))
        {
            Debug.Log("car has stopped due to somthing in front of it.");
            carCollision.DeActivate(this);
        }
    }
    public void SwitchToWaiting(Transform waitingPos)
    {
        agent.SetDestination(waitingPos.position);
    }
    public void LeaveWaiting()
    {
        agent.SetDestination(wayPoints[currWayPoint].position);
        drivingState = DrivingState.driving;
    }
}
public interface ICarCollision
{
    void Activate(EnermyPatrol car);

    void DeActivate(EnermyPatrol car);
}