﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    [Header("Atributos do Personagem")]
    public float jumpForce;
    private bool isGrounded;
    public float movementSpeed;
    public int score;
    private int speedX;
    private float speedY;
    public bool isLeft;
    public LayerMask whatIsGround;
    public int maxExtraJumps;
    private int extraJumps;
    public float shotForce;
    public float ttlAmmo;
    public float timeBtwShots;
    private bool canShot = true;
    
    [Header("Componentes do Personagem")]
    private Rigidbody2D _rigidbody;
    public Transform _groundCheck;
    public Text _pontuacao;
    private Animator _animator;
    public GameObject _ammo;
    public GameObject _shotPoint;
    private GameController _gameController;

	private void Start () {
		_rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        extraJumps = maxExtraJumps;
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
	}

    private void FixedUpdate() {
        isGrounded = Physics2D.OverlapCircle(_groundCheck.position, 0.02f, whatIsGround);
    }

    private void Update () {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (isGrounded) {
            extraJumps = maxExtraJumps;
        }

        if (Input.GetButtonDown("Jump") && ((extraJumps > 0) || isGrounded )) {
            Jump();
        }

        speedY = _rigidbody.velocity.y;

        _rigidbody.velocity = new Vector2(horizontal * movementSpeed, speedY);

        if (horizontal == 0) {
            speedX = 0;
        } else {
            speedX = 1;
        }

        if ((horizontal < 0 && !isLeft) || (horizontal > 0 && isLeft)) {
            Flip();
        }

        if (Input.GetButtonDown("Fire1") && _gameController.ammoQuantity > 0 && canShot) {
            Shot();
        }
	}

    private void LateUpdate() {
       _animator.SetInteger("speedX", speedX);
       _animator.SetFloat("speedY", speedY);
       _animator.SetBool("isGrounded", isGrounded);
    }

    private void OnTriggerEnter2D(Collider2D col) {
        switch(col.tag) {
            case "Obstacle":
                SceneManager.LoadScene("Gameover");
                break;
            case "Collectible":
                score += 1;
                _pontuacao.text = "Score: " + score;
                _gameController.changeAmmoQuantity(+1);
                Destroy(col.gameObject);
                break;
        }
    }

    public void Jump() {
        _rigidbody.AddForce(new Vector2(0, jumpForce));
        extraJumps--;
    }

    private void Flip() {
        isLeft = !isLeft;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        shotForce *= -1;
    }

    private void Shot () {
        canShot = false;
        StartCoroutine("ShotDelay");

        _gameController.changeAmmoQuantity(-1);

        GameObject ammo = Instantiate(_ammo);
        ammo.transform.position = _shotPoint.transform.position;
        ammo.GetComponent<Rigidbody2D>().velocity = new Vector2(shotForce, 0);

        Destroy(ammo, ttlAmmo);
    }

    IEnumerator ShotDelay() {
        yield return new WaitForSeconds(timeBtwShots);
        canShot = true;
    }
}
