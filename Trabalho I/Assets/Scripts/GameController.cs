using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [Header("Global Variables")]
    public float terrainSpeed;
    public GameObject[] _terrainPrefabs;
    public PlayerController _player;
    public float _playerPosY;

	void Start () {
		_player = FindObjectOfType(typeof(PlayerController)) as PlayerController;
	}
	
	void Update () {
		_playerPosY = _player.transform.position.y;
	}
}
