using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour {

    [Header("Atributos do Terreno")]
    private float terrainWidth;
    private bool terrainInstantiated;
    private float terrainSpeed;

    [Header("Componentes do Terreno")]
    private SpriteRenderer _spriteRenderer;
    private GameObject[] _terrainPrefabs;
    private Rigidbody2D _rigidbody;
    private GameController _gameController;

	private void Start () {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        terrainSpeed = _gameController.terrainSpeed;
        _terrainPrefabs = _gameController._terrainPrefabs;

        _rigidbody = GetComponent<Rigidbody2D>();		
        _rigidbody.velocity = new Vector2(terrainSpeed, 0);

        _spriteRenderer = GetComponent<SpriteRenderer>();
        terrainWidth = _spriteRenderer.size.x;
	}
	
	private void Update () {

        if (gameObject.transform.position.x <= -terrainWidth) {
            Destroy(this.gameObject);
        } else if (gameObject.transform.position.x <= terrainWidth/2) {
            if (!terrainInstantiated) {
                terrainInstantiated = true;
                int rand = Random.Range(0, _terrainPrefabs.Length);

                GameObject newTerrain = Instantiate(_terrainPrefabs[rand]);
                newTerrain.transform.position = new Vector2 (gameObject.transform.position.x + terrainWidth, gameObject.transform.position.y);
            }
        }
	}
}
