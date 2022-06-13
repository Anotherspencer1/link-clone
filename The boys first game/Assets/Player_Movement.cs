using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;
    public Vector2 movement;
    public Rigidbody2D rigidBody;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        if(movement.x != 0)
        {
            animator.SetFloat("Direction_x", movement.x);
            animator.SetFloat("Direction_y", 0f);
        }
        if(movement.y != 0)
        {
            animator.SetFloat("Direction_y", movement.y);
            animator.SetFloat("Direction_x", 0f);
        }
        movement = movement.normalized;
        animator.SetFloat("Speed", movement.sqrMagnitude);

        rigidBody.MovePosition(rigidBody.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
