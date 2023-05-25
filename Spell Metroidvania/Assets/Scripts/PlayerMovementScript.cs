using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class that deals with player movement and communicating to animator which animations to play when.
 */
public class PlayerMovementScript : MonoBehaviour
{
    //field declarations
    private float horizontalMovement;
    private float verticalMovement;
    private float speedValue = 10f;
    private float jumpingValue = 14f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public Animator animator;

    void Update()
    {
        handleMovementAndJumping();

        flipSprite();
    }

    private void handleMovementAndJumping()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = rb.velocity.y;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));
        animator.SetFloat("Jump", verticalMovement);

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingValue);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

    }


    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMovement * speedValue, rb.velocity.y);
    }


    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void flipSprite()
    {
        if (isFacingRight && horizontalMovement < 0f || !isFacingRight && horizontalMovement > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}