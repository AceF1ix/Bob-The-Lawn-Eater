using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GnomeAI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private enum State {
        Patrol,
        Place,
    }
    private State state;
    public Transform grassParent;
    public bool pleaseContinue;
    
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
        if(goneGrass.Count == 0)
        {
            state = State.Patrol;
        }
        else if(goneGrass.Count > 0 && !pleaseContinue)
        {
            state = State.Place;
            pleaseContinue = true;
        }

        switch(state) {
            case State.Patrol:
                Patrol();
                break;
            case State.Place:
                Place(goneGrass);
                break;
        }
    }

    public void Patrol(){

    }

    public void Place(List<GameObject> goneGrass){
        Vector3 target = goneGrass[Random.Range(0, goneGrass.Count)].transform.position;
        if(Vector3.Distance(transform.position, target) > 1f)
        {
            navMeshAgent.destination = target;
        }
        else
        {
            StartCoroutine(PlaceGrass(goneGrass[Random.Range(0, goneGrass.Count)]));
        }
    }

    private IEnumerator PlaceGrass(GameObject grassPatch)
    {
        WaitForSeconds wait = new WaitForSeconds(1f);

        while(!grassPatch.activeSelf)
        {
            yield return wait;
            grassPatch.SetActive(true);
        }

        pleaseContinue = false;
    }

    public List<GameObject> FindCutGrass()
    {
        List<GameObject> goneGrass = new List<GameObject>();
        foreach(Transform grass in grassParent)
        {
            if(!grass.gameObject.activeSelf)
            {
                goneGrass.Add(grass.gameObject);
            }
        }
        return goneGrass;
    }
}
