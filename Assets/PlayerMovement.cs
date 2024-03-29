using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {

    public CharacterController controller;
    public Rigidbody rig;
    public float moveSpeed = 15f;
    public float gravity = -25f;
    public float jump = 3f;


    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool grounded;


    void Start(){
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
 

        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (grounded && velocity.y < 0) {

            velocity.y = -2f;

        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        //boost
        if (Input.GetKey("left shift"))
        {

            rig.AddForce(rig.mass * transform.forward * 30000f, ForceMode.Impulse);

        }

        controller.Move(move * moveSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = Mathf.Sqrt(jump * -2f * gravity);
        }

        

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }


}
