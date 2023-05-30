using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    public float speedValue;
    private float horizontalMovement;

    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator animator;
    private Transform currentPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        horizontalMovement = rb.velocity.x;
        animator = GetComponent<Animator>();
        currentPoint = pointB.transform;
        animator.SetBool("isRunning", true);
    }

    // Update is called once per frame
    void Update() 
    { 
        Vector2 point = currentPoint.position - transform.position;
        
        if(currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speedValue, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speedValue, 0);
        }


        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            flipSprite();
            currentPoint = pointA.transform;
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            flipSprite();
            currentPoint = pointB.transform;
        }

        
    }

    private void flipSprite()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

}
