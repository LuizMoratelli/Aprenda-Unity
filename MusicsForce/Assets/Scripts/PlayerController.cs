using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState
{
    playing,
    teleporting
}

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
//[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    #region Sub-Classes and Structs
    #endregion

    #region Public Members
    public float Speed { get => speed; private set => speed = value; }
    public float JumpForce { get => jumpForce; private set => jumpForce = value; }
    public PlayerState CurrentPlayerState { get; set; }
    #endregion

    #region Private Members
    private Rigidbody2D playerRB;
    [SerializeField] private float speed;
    [SerializeField] private float rotation;
    [SerializeField] private float jumpForce;
    [SerializeField] private GameObject groundCheck;
    [SerializeField] private GameObject playerSprite;
    private bool inGround;
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #region Unity Default Methods
    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        CurrentPlayerState = PlayerState.playing;
    }

    private void Update()
    {
        if (CurrentPlayerState.Equals(PlayerState.playing))
        {
            if (Input.GetButtonDown("Jump") && inGround)
            {
                playerRB.AddForce(new Vector2(0, JumpForce));
            }
        }
    }

    private void FixedUpdate()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        if (CurrentPlayerState.Equals(PlayerState.playing))
        {
            playerRB.velocity = new Vector2(moveInput * Speed * Time.deltaTime, playerRB.velocity.y);

            inGround = Physics2D.OverlapCircle(groundCheck.transform.position, 0.15f);

        }
        else if (CurrentPlayerState.Equals(PlayerState.teleporting))
        {
            playerRB.velocity = Vector2.zero;
        }

        playerSprite.transform.Rotate(Vector3.forward * -(playerRB.velocity.x + playerRB.velocity.y) * rotation * Time.deltaTime);
    }
    #endregion
    #endregion
}
