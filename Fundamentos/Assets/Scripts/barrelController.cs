using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrelController : MonoBehaviour {

    private gameController _gameController;
    private Rigidbody2D _rigidBody;

	void Start () {
		_gameController = FindObjectOfType(typeof(gameController)) as gameController;
        _rigidBody = GetComponent<Rigidbody2D>();

        _rigidBody.velocity = new Vector2(_gameController.objectSpeed, 0);
	}

    void Update () {
        if (transform.position.x < _gameController.destructionDistance.position.x) {
            Destroy(this.gameObject);
        }
    }
}
