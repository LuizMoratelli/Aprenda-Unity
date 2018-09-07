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

    public float BridgeWidth {
        get {
            return bridgeWidth;
        }
    }
    
    [Header("Global Configuration")]
    public Transform destructionDistance;
    public Transform playableArea;

	void Start () {
		bridgeWidth = bridge.gameObject.GetComponent<SpriteRenderer>().size.x;
	}

}
