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
    public bool isFacingRight = true;

    public float knockbackForce = 5f;
    public float KBCounter;
    public float KBTotalTime;
    public bool noPlayerControl;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public Animator animator;

    public HealthManagerScript healthManager;

    void Update()
    {
        //handleMovementAndJumping();

        flipSprite();
    }

    private void handleMovementAndJumping()
    {
        if (noPlayerControl == false)
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
    }

    void Awake()
    {
        noPlayerControl = false;
        KBTotalTime = 1f;
    }

    private void FixedUpdate()
    {
        if (KBCounter <= 0)
        {
            rb.velocity = new Vector2(horizontalMovement * speedValue, rb.velocity.y);
            handleMovementAndJumping();
        }
        else
        {
            checkKnockback();
            KBCounter -= Time.deltaTime;
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            healthManager.applyDamage(10);
            KBCounter = KBTotalTime;
        }
        healthManager.checkIfDead();
    }

    private void checkKnockback()
    {
        float currentKnockbackForce = knockbackForce * (KBCounter / KBTotalTime);
        Vector2 knockbackVelocity = new Vector2(-currentKnockbackForce, currentKnockbackForce);

        if (!isFacingRight)
        {
            knockbackVelocity.x *= -1f;
        }

        knockbackVelocity.y = rb.velocity.y; // Preserve the current vertical velocity

        rb.velocity = knockbackVelocity;
        StartCoroutine(setNoPlayerControl());
    }

    private IEnumerator setNoPlayerControl()
    {
        noPlayerControl = true;
        animator.SetBool("isHurt", true);
        yield return new WaitForSeconds(1f);
        noPlayerControl = false;
        animator.SetBool("isHurt", false);
    }
}