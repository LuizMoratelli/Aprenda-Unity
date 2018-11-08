using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bunnyController : MonoBehaviour {

    [Header("Componentes do Objeto")]
    private Rigidbody2D _rigidbody;

    [Header("GameObjects Auxiliares")]
    public Transform groundCheck;
    private gameController gameController;

    [Header("Atributos do Objeto")]
    public float jumpForce;
    private bool grounded;

	void Start () {
		_rigidbody = GetComponent<Rigidbody2D>();
        gameController = FindObjectOfType(typeof(gameController)) as gameController;
	}

    void FixedUpdate() {
        // Cria um sensor lógico que retorna um valor na colisão
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f);
    }
	
	void Update () {
	    if (Input.GetButtonDown("Jump") && grounded) { 
            _rigidbody.AddForce(new Vector2(0, jumpForce));
        }
	}

    void OnTriggerEnter2D(Collider2D col) {
        switch(col.gameObject.tag) {
            case "collectible":
                gameController.toScore(1);
                Destroy(col.gameObject);
                break;
            case "obstacle":
                gameController.changeScene("Gameover");
                break;
        }
    }
}
