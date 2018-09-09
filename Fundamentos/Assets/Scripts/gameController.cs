using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameController : MonoBehaviour {

    [Header("Player Configuration")]
    public float playerSpeed;

    [Header("Bridge Configuration")]
    public Transform bridgeMax;
    public Transform bridgeMin;
    public GameObject bridge;
    private float bridgeWidth;

    // Permite que um script acesse a propriedade privada
    public float BridgeWidth {
        get {
            return bridgeWidth;
        }
        private set {
            bridgeWidth = value;
        }
    }

    [Header("Barrel Configuration")]
    public int barrelOrderTop;
    public int barrelOrderDown;
    public Transform barrelInstatiateTop; 
    public Transform barrelInstatiateBottom; 
    public float barrelTimeToSpawn;
    public GameObject barrel;

    [Header("Player Configuration")]
    public playerBehaviour _playerBehaviour;
    public Vector3 playerCurrentPosition;

    [Header("Score Configuration")]
    private int score;
    public Text scoreText;

    public int Score {
        get {
            return score;
        }
    }
    
    [Header("Global Configuration")]
    public Transform destructionDistance;
    public Transform playableArea;
    public float objectSpeed;

	void Start () {
		BridgeWidth = bridge.gameObject.GetComponent<SpriteRenderer>().size.x;
    
        StartCoroutine("barrelSpawn");

        _playerBehaviour = FindObjectOfType(typeof(playerBehaviour)) as playerBehaviour;
	}

    private void LateUpdate() {
        playerCurrentPosition = _playerBehaviour.transform.position;
    }

    IEnumerator barrelSpawn () {
        yield return new WaitForSeconds(barrelTimeToSpawn);

        int rand = Random.Range(0, 100);
        GameObject newBarrel = Instantiate(barrel);

        if (rand < 50) {
            newBarrel.transform.position = barrelInstatiateTop.position;
            // Quando o componente será usado várias vezes, uma variável para contê-lo será mais rápida
            newBarrel.GetComponent<SpriteRenderer>().sortingOrder = barrelOrderTop;
        } else {
            newBarrel.transform.position = barrelInstatiateBottom.position;
            newBarrel.GetComponent<SpriteRenderer>().sortingOrder = barrelOrderDown;
        }

        StartCoroutine("barrelSpawn");
    }

    public void toScore (int addScore) {
        score += addScore;
        scoreText.text = "Score: " + score;
    }

    public void changeScene (string destinationScene) {
        SceneManager.LoadScene(destinationScene);
    }

}
