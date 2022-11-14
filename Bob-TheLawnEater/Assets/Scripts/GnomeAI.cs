using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GnomeAI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private enum State {
        Patrol,
        Find,
        Place,
    }
    private State state;
    private float gnomePlaceTimer;
    private bool placeGrass;
    public Transform grassParent;
    private Vector3 target;
    private GameObject chosenGrass;
    private bool find;
    
    // Start is called before the first frame update
    void Start()
    {
        state = State.Patrol;
    }

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        List<GameObject> goneGrass = FindCutGrass();
        if(find)
        {
           state = State.Place; 
        }
        else if(goneGrass.Count > 0 && !find)
        {
            state = State.Find;
        }
        else
        {
            state = State.Patrol;
        }

        switch(state) {
            case State.Patrol:
                Patrol();
                break;
            case State.Find:
                Find(goneGrass);
                break;
            case State.Place:
                Place();
                break;
        }
    }

    public void Patrol(){

    }

    public void Find(List<GameObject> goneGrass){
        for(int i = 0; i < goneGrass.Count; i++)
        {
            if(i == 0)
            {
                chosenGrass = goneGrass[i];
            }
            if(Vector3.Distance(transform.position, goneGrass[i].transform.GetChild(0).gameObject.transform.position) < Vector3.Distance(transform.position, chosenGrass.transform.GetChild(0).gameObject.transform.position))
            {
                chosenGrass = goneGrass[i];
            }
        }
        chosenGrass.transform.GetChild(1).gameObject.SetActive(false);
        target = chosenGrass.transform.GetChild(0).gameObject.transform.position;
        if(Vector3.Distance(transform.position, target) != 0)
        {
            navMeshAgent.destination = target;
        }
        find = true;
    }

    public void Place(){
        if(Vector3.Distance(transform.position, target) < 0.1f)
        {
            if(gnomePlaceTimer == 0f)
            {
                gnomePlaceTimer = Time.realtimeSinceStartup;
            }
            else if(Time.realtimeSinceStartup - gnomePlaceTimer > 0.1f)
            {
                chosenGrass.SetActive(true);
                chosenGrass.transform.GetChild(1).gameObject.SetActive(true);
                gnomePlaceTimer = 0;
                find = false;
            }
        }
    }

    public List<GameObject> FindCutGrass()
    {
        List<GameObject> goneGrass = new List<GameObject>();
        foreach(Transform grass in grassParent)
        {
            if(!grass.gameObject.activeSelf && grass.GetChild(1).gameObject.activeSelf)
            {
                goneGrass.Add(grass.gameObject);
            }
        }
        return goneGrass;
    }
}
