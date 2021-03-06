﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class ActorController : MonoBehaviour
{

    private Animator animator;
    private Rigidbody rigidbody;


    private float runSpeed = 5f;
    private bool isJump = true;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        animator.SetBool("isRun", false);
        animator.SetBool("isJump", false);
    }

    void FixedUpdate()
    {
        if(transform.position.y >= 0.06) {
            isJump = false;
        }
        if(transform.position.y < 0.06) {
            animator.SetBool("isJump", false);
            isJump = true;
        }
        if (!animator.GetBool("isAlive"))
        {
            return;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float translationX = z * runSpeed * Time.fixedDeltaTime;
        
        Debug.Log(translationX);
        Debug.Log(z);

        animator.SetFloat("Speed", Mathf.Max(Mathf.Abs(x), Mathf.Abs(z)));

        animator.speed = 1 + animator.GetFloat("Speed") / 3;
       

        if (z != 0)
        {

            animator.SetBool("isRun", true);
            

        }
        else {
            animator.SetBool("isRun", false);
        }
        float mouseX = Input.GetAxis("Mouse X") * 5.0f;
        transform.Rotate(new Vector3(0, mouseX, 0));
        transform.Translate(0, 0, translationX);
        if (transform.localEulerAngles.x != 0 || transform.localEulerAngles.z != 0)
        {
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        }
        if (transform.position.y != 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        if(Input.GetKeyDown(KeyCode.Space)) {
            if(isJump) {
                animator.SetBool("isJump", true);
                rigidbody.AddForce (Vector3.up * 10, ForceMode.VelocityChange); 
            }
        }

       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Area"))
        {
            Debug.Log("enter area");
            Publish publish = Publisher.getInstance();
            int patrolType = other.gameObject.name[other.gameObject.name.Length - 1] - '0';
            publish.notify(ActorState.ENTER_AREA, patrolType, this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("death");
        if (collision.gameObject.CompareTag("Patrol") && animator.GetBool("isAlive"))
        {
            Debug.Log("death");
            animator.SetBool("isAlive", false);
            animator.SetTrigger("toDie");
            Publisher publisher = Publisher.getInstance();
            publisher.notify(ActorState.DEATH, 0, null);
        }
    }

    // Update is called once per frame

}
