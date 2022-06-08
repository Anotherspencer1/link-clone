using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;

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

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (movement.x == 0 && movement.y == 0)
            {
                prioInput = PrioritisedInput.None;
            }
            else if (Mathf.Abs(movement.x) == 1f && Mathf.Abs(movement.y) == 0f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(movement.x, 0f, 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(movement.x, 0f, 0f);
                    prioInput = PrioritisedInput.XAxis;
                }
            }
            else if (Mathf.Abs(movement.y) == 1f && Mathf.Abs(movement.x) == 0f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, movement.y, 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(0f, movement.y, 0f);
                    prioInput = PrioritisedInput.YAxis;
                }
            }
            else if (Mathf.Abs(movement.y) == 1f && Mathf.Abs(movement.x) == 1f && prioInput == PrioritisedInput.XAxis)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, movement.y, 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(0f, movement.y, 0f);
                }
            }
            else if (Mathf.Abs(movement.y) == 1f && Mathf.Abs(movement.x) == 1f && prioInput == PrioritisedInput.YAxis)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(movement.x, 0f , 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(movement.x, 0f, 0f);
                }
            }
            else if (Mathf.Abs(movement.x) == 1f && Mathf.Abs(movement.y) == 1f && prioInput == PrioritisedInput.XAxis)
            {
                prioInput = PrioritisedInput.XAxis;
            }
        }
    }
}
