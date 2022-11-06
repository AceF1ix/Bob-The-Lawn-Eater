using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassClipper : MonoBehaviour
{
    // Start is called before the first frame update
    public float grassClipped = 0;
    public int capacity = 100;
    public Transform player;
    private float radius = 2.0f;
    public int numSegments = 500;
    public Collider[] hitColliders;
    public GameObject trashCan;
    
    void Start()
    {
        DoRenderer();
    }

    // Update is called once per frame
    void Update()
    {
        hitColliders = Physics.OverlapSphere(player.position, radius);
        for (var i = 0; i < hitColliders.Length; i++){
            if (hitColliders[i].tag == "TrashCan"){
                if(Input.GetKeyDown(KeyCode.F))
                {
                    ThrowTrash();
                }
            }
        }
    }

    public void ThrowTrash(){
        grassClipped = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Grass" && grassClipped < capacity)
        {
            grassClipped += 1;
            other.gameObject.SetActive(false);
        }
    }

    void DoRenderer()
    {
        LineRenderer lineRenderer = trashCan.GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = numSegments + 1;
        lineRenderer.useWorldSpace = false;

        float deltaTheta = (float) (2.0 * Mathf.PI) / numSegments;
        float theta = 0f;

        for (int i = 0; i < numSegments + 1; i++)
        {
            float x = radius * Mathf.Cos(theta);
            float z = radius * Mathf.Sin(theta);
            Vector3 pos = new Vector3(x, -1, z);
            lineRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }
    }
}
