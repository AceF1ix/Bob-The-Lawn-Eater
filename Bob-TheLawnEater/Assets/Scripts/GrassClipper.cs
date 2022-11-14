using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassClipper : MonoBehaviour, IDataPersistence
{
    // Start is called before the first frame update
    public int grassClipped = 0;
    private float standardCapacity = 25;
    public float capacity;
    public Transform player;
    private float radius = 2.0f;
    public int numSegments = 200;
    public Collider[] hitColliders;
    public GameObject trashCan;
    public AudioManager audio;
    public float timer = 0f;
    public float money = 0;
    private float[] capacityUpgrade = new float[]{1f, 2f, 4f, 7f, 15f};
    private float[] moneyUpgrade = new float[]{1f, 2f, 3f, 4f, 5f};
    public Shop2 shop2;
    
    void Start()
    {
        DoRenderer();
        capacity = standardCapacity * capacityUpgrade[shop2.typeRank[2]];
    }

    // Update is called once per frame
    void Update()
    {
        capacity = standardCapacity * capacityUpgrade[shop2.typeRank[2]];
        hitColliders = Physics.OverlapSphere(player.position, radius);
        for (var i = 0; i < hitColliders.Length; i++){
            if (hitColliders[i].tag == "TrashCan"){
                if(Input.GetKeyDown(KeyCode.F))
                {
                    ThrowTrash();
                }
            }
        }

        if(Time.realtimeSinceStartup - timer > 0.2f)
        {
            audio.Stop("Lawn-mower-cutting grass");
        }
    }

    public void LoadData(GameData data)
    {
        this.money = data.moneyCount;
    }

    public void SaveData(ref GameData data)
    {
        data.moneyCount = this.money;
    }

    public void ThrowTrash(){
        money += grassClipped * moneyUpgrade[shop2.typeRank[0]];
        audio.Play("ThrowTrash");
        grassClipped = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Grass" && grassClipped < capacity)
        {
            timer = Time.realtimeSinceStartup;
            audio.Play("Lawn-mower-cutting grass");
            grassClipped += 1;
            other.transform.parent.gameObject.SetActive(false);
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
            Vector3 pos = new Vector3(x, -1f, z);
            lineRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }
    }
}
