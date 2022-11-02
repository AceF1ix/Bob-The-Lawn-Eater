using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassClipper : MonoBehaviour
{
    // Start is called before the first frame update
    public float grassClipped = 0;
    private float capacity = 50;
    public Transform player;
    public float trashDistance = 2f;
    public Collider[] hitColliders;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hitColliders = Physics.OverlapSphere(player.position, trashDistance);
        for (var i = 0; i < hitColliders.Length; i++){
            if (hitColliders[i].tag == "TrashCan"){
                Debug.Log("Can Throw");
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
}
