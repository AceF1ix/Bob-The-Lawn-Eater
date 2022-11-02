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




    // Update is called once per frame
    void Update()
    {

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(x, 0f, z).normalized;

        // Vertical Movement

        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
        {
            speed = generalSpeed / 1.5f;
        }

        if(Mathf.Abs(direction.x) < Mathf.Abs(direction.z))
        {
            speed = generalSpeed * 1f;
        }

        if(Mathf.Abs(direction.x) == Mathf.Abs(direction.z))
        {
            speed = generalSpeed / 1.3f;
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
