using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

    private Rigidbody2D rb;
    public float speed;
    public Transform ponteMax;
    public Transform ponteMin;

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

        // Verifica limite de altura
        Vector3 newPosition = transform.position;

        if (transform.position.y > ponteMax.position.y) {
            newPosition.y = ponteMax.position.y;
        } else if (transform.position.y < ponteMin.position.y) {
            newPosition.y = ponteMin.position.y;
        }

        if (transform.position.x > ponteMax.position.x) {
            newPosition.x = ponteMax.position.x;
        } else if (transform.position.x < ponteMin.position.x) {
            newPosition.x = ponteMin.position.x;
        }

        transform.position = newPosition;
	}
}
