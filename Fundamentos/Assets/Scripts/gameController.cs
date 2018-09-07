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
    
    [Header("Global Configuration")]
    public Transform destructionDistance;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
