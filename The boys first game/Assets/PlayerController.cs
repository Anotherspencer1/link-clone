using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public Animator animator;

    public LayerMask whatStopsMovement;
    public enum PrioritisedInput
    {
        None,
        XAxis,
        YAxis
    }
    public PrioritisedInput prioInput = PrioritisedInput.None;

    public Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (movement.x == 0 && movement.y == 0)
            {
                prioInput = PrioritisedInput.None;
            }
            else if (Mathf.Abs(movement.x) == 1f && Mathf.Abs(movement.y) == 0f)
            {
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", 0);
                animator.SetFloat("Direction_x", movement.x);
                animator.SetFloat("Direction_y", 0);
                
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(movement.x, 0f, 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(movement.x, 0f, 0f);
                    prioInput = PrioritisedInput.XAxis;
                }
            }
            else if (Mathf.Abs(movement.y) == 1f && Mathf.Abs(movement.x) == 0f)
            {
                animator.SetFloat("Vertical", movement.y);
                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Direction_y", movement.y);
                animator.SetFloat("Direction_x", 0);
                
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, movement.y, 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(0f, movement.y, 0f);
                    prioInput = PrioritisedInput.YAxis;
                }
            }
            else if (Mathf.Abs(movement.y) == 1f && Mathf.Abs(movement.x) == 1f && prioInput == PrioritisedInput.XAxis)
            {
                animator.SetFloat("Vertical", movement.y);
                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Direction_y", movement.y);
                animator.SetFloat("Direction_x", 0);
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, movement.y, 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(0f, movement.y, 0f);

                }
            }
            else if (Mathf.Abs(movement.y) == 1f && Mathf.Abs(movement.x) == 1f && prioInput == PrioritisedInput.YAxis)
            {
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", 0);
                animator.SetFloat("Direction_x", movement.x);
                animator.SetFloat("Direction_y", 0);
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(movement.x, 0f , 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(movement.x, 0f, 0f);

                }
            }
        }
    }
}
