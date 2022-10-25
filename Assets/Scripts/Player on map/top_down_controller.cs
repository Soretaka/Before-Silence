using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class top_down_controller : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;
    public VectorValue startingPosition;
    public FixedJoint2D joint;

    Vector2 startDir;

    Vector2 movement;

    private void Awake() {
        transform.parent.position = startingPosition.initialValue;
    }

    private void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if(Math.Abs(movement.x) > 0.01 || Math.Abs(movement.y) > 0.01)
        {
            animator.SetFloat("lastMoveX", movement.x);
            animator.SetFloat("lastMoveY", movement.y);
        }
        
    }
    
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

}


