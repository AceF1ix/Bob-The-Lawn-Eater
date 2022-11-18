using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MenuScene : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public Transform grassParent;
    public Transform waypointParent;
    public AudioManager audio;
    private List<Transform> waypointsReal = new List<Transform>();
    private Transform[] waypoints;
    private Animator animator;
    int wayPointIndex;
    Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        audio.Play("Background music");
        animator = GetComponent<Animator>();
        for (int i = 0; i < waypointParent.childCount; i++)
        {
            waypointsReal.Add(waypointParent.GetChild(i));
        }
        waypoints = waypointsReal.ToArray();
        UpdateDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target) < 1f)
        {
            UpdateWayPointIndex();
            UpdateDestination();
        }
        RespawnWhenGone();
    }

    private void Awake() 
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void UpdateDestination()
    {
        target = waypoints[wayPointIndex].position;
        navMeshAgent.destination = target;
    }

    void UpdateWayPointIndex()
    {
        wayPointIndex += 1;
        if (wayPointIndex == waypoints.Length)
        {
            wayPointIndex = 0;
        }
    }

    private void RespawnWhenGone()
    {
        int activeChildren = 0;
        int children = grassParent.childCount;
        for (int i = 0; i < children; ++i)
        {
            if (grassParent.GetChild(i).gameObject.activeSelf)
            {
                activeChildren += 1;
            }
        }
        if (activeChildren <= 0)
        {
            for (int i = 0; i < children; ++i)
            {
                grassParent.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
