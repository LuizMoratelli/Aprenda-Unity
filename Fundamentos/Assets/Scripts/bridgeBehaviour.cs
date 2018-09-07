using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridgeBehaviour : MonoBehaviour {

    private gameController _gameController;
    private Rigidbody2D _rigidBody;

	void Start () {
        _rigidBody = GetComponent<Rigidbody2D>();
		_gameController = FindObjectOfType(typeof(gameController)) as gameController;

        // Velocity utiliza física, então não é necessário utilizar Time.DeltaTime()
		Vector2 movement = new Vector2(_gameController.bridgeSpeed, 0);
        _rigidBody.velocity = movement;
	}
	
	void Update () {
        // Destrói o objeto quando ele sair da cena
        if (transform.position.x < _gameController.destructionDistance.position.x) {
            Destroy(this.gameObject);
        }

	}
}
