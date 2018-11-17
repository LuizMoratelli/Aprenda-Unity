using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWay : MonoBehaviour {

    public Transform collisionHeight;
    private GameController _gameController;
    private BoxCollider2D _collider;

	void Start () {
		_gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _collider = GetComponent<BoxCollider2D>();
	}
	
	void Update () {
		if (_gameController._playerPosY > collisionHeight.transform.position.y) {
            if (Input.GetKey(KeyCode.S)) {
                _collider.enabled = false;
            } else {
                _collider.enabled = true;
            }
        } else {
            _collider.enabled = false;
        }
	}
}
