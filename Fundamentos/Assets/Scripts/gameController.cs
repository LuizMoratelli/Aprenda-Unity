using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour {

    [Header("Player Configuration")]
    public float playerSpeed;

    [Header("Bridge Configuration")]
    public Transform bridgeMax;
    public Transform bridgeMin;
    public float bridgeSpeed;
    public GameObject bridge;
    private float bridgeWidth;
    
    [Header("Global Configuration")]
    public Transform destructionDistance;
    public Transform playableArea;

	// Use this for initialization
	void Start () {
		bridgeWidth = bridge.gameObject.GetComponent<SpriteRenderer>().size.x;
	}
	
	// Update is called once per frame
	void Update () {
		// Instancia a nova ponte caso exista apenas uma ponte
        bridgeBehaviour[] bridges = FindObjectsOfType(typeof(bridgeBehaviour)) as bridgeBehaviour[];
        if (bridges.Length < 2) {
            GameObject newBridge = Instantiate(bridge); // Instancia a nova ponte
            // Define a posiação da nova ponte como a posição da ponte atual mais a largura do prefab da ponte
            newBridge.transform.position = new Vector2 (bridges[0].transform.position.x + bridgeWidth, bridges[0].transform.position.y);
            // Define que a nova ponte será filha da Área jogável
            newBridge.transform.SetParent(playableArea.transform);
        }
	}
}
