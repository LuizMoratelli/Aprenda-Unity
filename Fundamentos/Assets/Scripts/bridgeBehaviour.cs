using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridgeBehaviour : MonoBehaviour {

    private gameController _gameController;
    private Rigidbody2D _rigidBody;

    private bool instantiated;

	void Start () {
        _rigidBody = GetComponent<Rigidbody2D>();
		_gameController = FindObjectOfType(typeof(gameController)) as gameController;

        // Velocity utiliza física, então não é necessário utilizar Time.DeltaTime()
		Vector2 movement = new Vector2(_gameController.objectSpeed, 0);
        _rigidBody.velocity = movement;
	}
	
	void Update () {
        // Destrói o objeto quando ele sair da cena
        if (transform.position.x < _gameController.destructionDistance.position.x) {
            Destroy(this.gameObject);
        }

        // Instancia a nova ponte caso exista apenas uma ponte
        if (!instantiated) {
            int idBridge = Random.Range(0, 2);

            if (transform.position.x < _gameController.bridge[idBridge].transform.position.x) {
                instantiated = true;
                GameObject newBridge = Instantiate(_gameController.bridge[idBridge]); // Instancia a nova ponte
                // Define a posiação da nova ponte como a posição da ponte atual mais a largura do prefab da ponte
                newBridge.transform.position = new Vector2 (transform.position.x + _gameController.BridgeWidth, transform.position.y);
                // Define que a nova ponte será filha da Área jogável
                newBridge.transform.SetParent(_gameController.playableArea.transform);
            }
        }
	}
}