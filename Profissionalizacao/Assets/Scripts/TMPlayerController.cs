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
    private bool isIdle;
    private bool canAttack;
    private bool isAttacking;
    private bool isFlying;
    private bool isSnorkeling;
    private float gravityBase;
    private Vector2 moveInput;

    // Power Ups
    private bool haveHammer;
    private bool haveBall;
    private bool haveCape;
    private bool haveDiving = true;

    [Header("GroundCheck Configuration")]
    [SerializeField] private Transform groundCheckRight;
    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private LayerMask whatIsGround;

    [Header("General Configuraion")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float timeBtwAttacks;
    [SerializeField] private float timeToIdle;
    [SerializeField] private float jumpForceOnFlying;
    [SerializeField] private float jumpForceOnSnorkeling;
    [SerializeField] private float gravityOnFlying;
    [SerializeField] private float gravityOnSnorkeling;

    [Header("Ball Configuration")]
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform ballSpawn;
    [SerializeField] private float ballSpeed;

    [Header("Colliders Configuration")]
    [SerializeField] private Collider2D hammerHit;
    [SerializeField] private Collider2D colDefault;
    [SerializeField] private Collider2D colOnFlying;
    [SerializeField] private Collider2D colOnSnorkeling;
    #endregion

    #region Public Methods
    public void OnAttackCompleted()
    {
        isAttacking = false;
        hammerHit.enabled = false;
        StartCoroutine("EnableNextAttack");
    }

    public void ShotBall()
    {
        GameObject _ball = Instantiate(ballPrefab, ballSpawn.position, transform.localRotation);
        _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(ballSpeed, 0);
    }

    public void AtualizarItens(int idItem) 
    {
        switch (idItem)
        {
            case 0:
                haveHammer = true;
                break;
            case 1:
                haveBall = true;
                break;
            case 2:
                haveCape = true;
                break;
            case 3:
                haveDiving = true;
                break;
            default:
                break;
        }
    }
    #endregion

    #region Private Methods
    #region Unity Collisions Methods
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Agua"))
        {
            isFlying = false;
        }
        if (other.CompareTag("SubMerso"))
        {
            if (!isSnorkeling && haveDiving)
            {
                isSnorkeling = true;
                playerRB.velocity = new Vector2(0, 0);
                playerRB.gravityScale = gravityOnSnorkeling;
            }
        }
        if (other.CompareTag("ItemLoja"))
        {
            // Caso não exista o método, erro é previnido
            other.SendMessage("AbrirLoja", SendMessageOptions.DontRequireReceiver);
        }
        if (other.CompareTag("Coletavel"))
        {
            other.SendMessage("Coletavel");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Agua"))
        {
            isSnorkeling = false;
            playerRB.gravityScale = gravityBase;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("PlataformaMovel"))
        {
            if (groundCheckLeft.position.y > other.transform.position.y)
            {
                transform.parent = other.transform;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("PlataformaMovel"))
        {
            transform.parent = null;
        }
    }
    #endregion
    #region Unity Default Methods
    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        gravityBase = playerRB.gravityScale;
        canAttack = true;
        hammerHit.enabled = false;
        EnableColliders(colDefault);
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

        if (!isSnorkeling)
        {
            if ((moveInput.Equals(Vector2.zero)) && (isGrounded) && !isIdle && !isAttacking && !isFlying)
            {
                isIdle = true;
                StartCoroutine("Idle2");
            }
            else if ((moveInput.x != 0) || (!isGrounded) || (isAttacking) || (isFlying))
            {
                StopIdle2Animation();
            }

            // Hammer Attack
            if (Input.GetButtonDown("Fire1") && canAttack && !isFlying && haveHammer)
            {
                playerAnim.SetTrigger("hammerAttack");
                canAttack = false;
                isAttacking = true;
            }

            // Ball Attack
            if (Input.GetButtonDown("Fire2") && canAttack && !isFlying && haveBall)
            {
                playerAnim.SetTrigger("ballAttack");
                canAttack = false;
                isAttacking = true;
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                playerRB.AddForce(new Vector2(0, jumpForce));
            }

            if (Input.GetButtonDown("Jump") && !isGrounded && !isFlying && haveCape)
            {
                isFlying = true;
                playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForceOnFlying);
                playerRB.gravityScale = gravityOnFlying;
            }

            if ((Input.GetButtonUp("Jump")) || (isFlying && isGrounded) || isAttacking)
            {
                isFlying = false;
                playerRB.gravityScale = gravityBase;
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump"))
            {
                playerRB.AddForce(new Vector2(0, jumpForceOnSnorkeling));
            }
        }

        if ((moveInput.x < 0 && !lookLeft)
                || (moveInput.x > 0 && lookLeft))
        {
            Flip();
        }

        playerRB.velocity = new Vector2(moveInput.x * speed * Time.deltaTime, playerRB.velocity.y);
        UpdateAnimator();
        UpdateColliders();
    }
    #endregion
    private void StopIdle2Animation()
    {
        isIdle = false;
        StopCoroutine("Idle2");
    }

    private void Flip()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        lookLeft = !lookLeft;
        ballSpeed = -ballSpeed;
    }

    private void UpdateAnimator()
    {
        playerAnim.SetInteger("moveInputX", (int)moveInput.x);
        playerAnim.SetBool("isGrounded", isGrounded);
        playerAnim.SetFloat("speedY", (int)playerRB.velocity.y);
        playerAnim.SetBool("isFlying", isFlying);
        playerAnim.SetBool("isAttacking", isAttacking);
        playerAnim.SetBool("isSnorkeling", isSnorkeling);
    }

    private void UpdateColliders()
    {
        if (isSnorkeling)
        {
            if (!colOnSnorkeling.enabled)
            {
                EnableColliders(colOnSnorkeling);
            }
        }
        else if (isFlying)
        {
            if (!colOnFlying.enabled)
            {
                EnableColliders(colOnFlying);
            }
        }
        else
        {
            if (!colDefault.enabled)
            {
                EnableColliders(colDefault);
            }
        }
    }
    #region IEnumartors
    private IEnumerator Idle2()
    {
        yield return new WaitForSeconds(timeToIdle);
        playerAnim.SetTrigger("isIdle");
    }

    private IEnumerator EnableNextAttack()
    {
        yield return new WaitForSeconds(timeBtwAttacks);
        canAttack = true;
    }

    private void EnableColliders(params Collider2D[] colliders)
    {
        colDefault.enabled = false;
        colOnFlying.enabled = false;
        colOnSnorkeling.enabled = false;

        foreach (Collider2D collider in colliders)
        {
            collider.enabled = true;
        }
    }
    #endregion
    #endregion
}
