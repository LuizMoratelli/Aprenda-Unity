using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrelBehaviour : MonoBehaviour {

    private gameController _gameController;
    private Rigidbody2D _rigidBody;
    private bool punctuated; // Garante que caba barril só dará um ponto
    public int scoreWon = 1;

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

    void LateUpdate() {
        if (!punctuated) {
            if (transform.position.x < _gameController.playerCurrentPosition.x) {
                punctuated = true;
                _gameController.toScore(scoreWon);
            }
        }    
    }
}
