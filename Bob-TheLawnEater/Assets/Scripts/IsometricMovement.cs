using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricMovement : MonoBehaviour
{
    
    public CharacterController controller;
    public float generalSpeed = 10f;
    public float speed = 10f;
    public float turnSmoothTime = 0.1f;
    public float friction = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;
    public AudioManager audio;
    public Shop2 shop2;
    private float[] speedUpgrade = new float[]{0.6f, 0.8f, 1f, 1.2f, 1.4f};
    private float[] sizeUpgrade = new float[]{0f, 0.04f, 0.08f, 0.12f, 0.5f};


    void Start()
    {
        audio.Play("Background music");
        audio.Play("Lawn-mower-not cutting grass");
        transform.localScale += new Vector3(sizeUpgrade[shop2.typeRank[3]], sizeUpgrade[shop2.typeRank[3]], sizeUpgrade[shop2.typeRank[3]]);
    }

    // Update is called once per frame
    void Update()
    {
        float realSpeed = generalSpeed * speedUpgrade[shop2.typeRank[1]];
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(x, 0f, z).normalized;

        // Vertical Movement

        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
        {
            speed = realSpeed / 1.5f;
        }

        if(Mathf.Abs(direction.x) < Mathf.Abs(direction.z))
        {
            speed = realSpeed * 1f;
        }

        if(Mathf.Abs(direction.x) == Mathf.Abs(direction.z))
        {
            speed = realSpeed / 1.3f;
        }


        // Horizontal Movement

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + 45f;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            Vector3 slide = moveDir * speed;

            controller.Move(slide * Time.deltaTime);
        }
    }

}
