using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    #region Sub-Classes and Structs
	#endregion

	#region Public Members
	#endregion

	#region Private Members
    private GameController gameController;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform groundCheck2;
    [SerializeField] private AudioClip jumpSound;
    private bool isGrounded;
    private int speedX;
    private float speedY;
    private bool isRight;

    private Rigidbody2D playerRB;
    private Animator playerAnim;
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #region Unity Default Methods
    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        isRight = true;

        gameController = FindObjectOfType<GameController>();
    }

    private void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        speedX = (int) Mathf.Abs(moveInput);
        speedY = playerRB.velocity.y;
        playerRB.velocity = new Vector2(moveInput * speed * Time.deltaTime, speedY);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerRB.AddForce(new Vector2(0, jumpForce));
            gameController.PlaySound(jumpSound);
        }

        if ((moveInput > 0 && !isRight) || (moveInput < 0 && isRight))
        {
            InvertRotation();
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f) || Physics2D.OverlapCircle(groundCheck2.position, 0.02f);
    }

    private void LateUpdate()
    {
        playerAnim.SetInteger("speedX", speedX);
        playerAnim.SetFloat("speedY", speedY);
        playerAnim.SetBool("isGrounded", isGrounded);
    }
    #endregion
    #region Unity Physics Methods
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coletavel"))
        {
            ColetavelController _coletavel = other.GetComponent<ColetavelController>();
            if (_coletavel.Tipo == ColetavelType.pontuacao)
            {
                gameController.AddScore(_coletavel.ScoreAmount);
            }

            _coletavel.Pickup();
        } else if (other.CompareTag("Finish"))
        {
            gameController.LoadScene("Complete");
        }
    }
    #endregion

    private void InvertRotation()
    {
        isRight = !isRight;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }
    #endregion
}
