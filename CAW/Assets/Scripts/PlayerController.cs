using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    #region Sub-Classes and Structs

    #endregion

    #region Public Members
    public SpriteRenderer Fumaca { get => fumaca; set => fumaca = value; }
    public GameObject Sombra { get => sombra; set => sombra = value; }
    #endregion

    #region Private Members
    private Rigidbody2D playerRigidbody;
    private Transform armaPosition;
    [SerializeField] private float speed;
    [SerializeField] private float bulletSpeed;
    private GameController gameController;
    [SerializeField] private int idBullet;
    private BulletTag bulletTag;
    private SpriteRenderer spriteRender;
    [SerializeField] private Color indestructibleColor;
    [SerializeField] private float delayPiscar;
    [SerializeField] private SpriteRenderer fumaca;
    [SerializeField] private GameObject sombra;
    #endregion

    #region Public Methods
    public IEnumerator Indestructible()
    {
        Collider2D col = GetComponent<Collider2D>();
        col.enabled = false;

        spriteRender.color = indestructibleColor;
        StartCoroutine("PiscarPlayer");

        yield return new WaitForSeconds(gameController.IndestructibleTime);

        spriteRender.color = Color.white;
        StopCoroutine("PiscarPlayer");
        spriteRender.enabled = true;
        col.enabled = true;
    }
    #endregion

    #region Private Methods
    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        armaPosition = transform.GetChild(0);
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
        gameController.Player = this;
        spriteRender = GetComponent<SpriteRenderer>();
        //gameController = FindObjectOfType<GameController>();
    }

    private void Update()
    {
        if (gameController.CurrentGameState == GameState.gameplay)
        {
            Vector2 _moveInput = new Vector2(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical")
            ).normalized;

            playerRigidbody.velocity = _moveInput * speed;

            if (Input.GetButtonDown("Fire1"))
            {
                Shot();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            gameController.Die();
            Destroy(other.gameObject);
        }
    }

    private void Shot()
    {
        GameObject _bullet = Instantiate(gameController.BulletPrefabs[idBullet], armaPosition.position, Quaternion.identity);
        _bullet.transform.tag = bulletTag.ToString();
        _bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);
    }

    private IEnumerator PiscarPlayer()
    {
        yield return new WaitForSeconds(delayPiscar);
        spriteRender.enabled = !spriteRender.enabled; 
        StartCoroutine("PiscarPlayer");
    }
    #endregion
}
