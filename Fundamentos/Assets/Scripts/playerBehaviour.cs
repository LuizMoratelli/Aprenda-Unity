using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehaviour : MonoBehaviour {

    private Rigidbody2D _rigidBody;
    private gameController _gameController;

	void Start () {
		_rigidBody = GetComponent<Rigidbody2D>();
        _gameController = FindObjectOfType(typeof(gameController)) as gameController;
	}
	
	void Update () {
        /**
         * A movimentação pode ser efetuada no Update pois é feito através de um 
         * componente de física ao invés do transform, não sendo afetada pelo FPS.
         */
        // GetAxisRaw retorna um valor inteiro (-1, 0, 1)
        // Horizontal e Vertical são Input Axes padrões da Unity
        // Como estar sendo utilizado velocity não é necessário utilizar Time.deltaTime
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * _gameController.playerSpeed;
        _rigidBody.velocity = movement;

        // Verifica limite de altura
        Vector3 newPosition = transform.position;

        if (transform.position.y > _gameController.bridgeMax.position.y) {
            newPosition.y = _gameController.bridgeMax.position.y;
        } else if (transform.position.y < _gameController.bridgeMin.position.y) {
            newPosition.y = _gameController.bridgeMin.position.y;
        }

        if (transform.position.x > _gameController.bridgeMax.position.x) {
            newPosition.x = _gameController.bridgeMax.position.x;
        } else if (transform.position.x < _gameController.bridgeMin.position.x) {
            newPosition.x = _gameController.bridgeMin.position.x;
        }

        transform.position = newPosition;
	}
}
