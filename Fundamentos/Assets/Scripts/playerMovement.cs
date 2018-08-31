using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

    private Rigidbody2D rb;
    public float speed;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        /**
         * A movimentação pode ser efetuada no Update pois é feito através de um 
         * componente de física ao invés do transform, não sendo afetada pelo FPS.
         */
        // GetAxisRaw retorna um valor inteiro (-1, 0, 1)
        // Horizontal e Vertical são Input Axes padrões da Unity
        // Como estar sendo utilizado velocity não é necessário utilizar Time.deltaTime
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed);
        rb.velocity = movement;

	}
}
