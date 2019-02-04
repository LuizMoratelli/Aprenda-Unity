using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMPlayerController : MonoBehaviour
{
    #region Sub-Classes and Structs
    #endregion

    #region Public Members
    #endregion

    #region Private Members
    private Rigidbody2D playerRB;
    private Animator playerAnim;

    private bool lookLeft;
    private bool isGrounded;
    private Vector2 moveInput;

    [SerializeField] private Transform groundCheckRight;
    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #region Unity Default Methods
    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckRight.position, groundCheckLeft.position, whatIsGround);
    }

    private void Update()
    {
        moveInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            0
        );

        if ((moveInput.x < 0 && !lookLeft)
            || (moveInput.x > 0 && lookLeft))
        {
            Flip();
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerRB.AddForce(new Vector2(0, jumpForce));
        }

        playerRB.velocity = new Vector2(moveInput.x * speed * Time.deltaTime, playerRB.velocity.y);
        UpdateAnimator();
    }
    #endregion

    private void Flip()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        lookLeft = !lookLeft;
    }

    private void UpdateAnimator()
    {
        playerAnim.SetInteger("moveInputX", (int) moveInput.x);
        playerAnim.SetBool("isGrounded", isGrounded);
        playerAnim.SetFloat("speedY", (int) playerRB.velocity.y);
    }
    #endregion
}
