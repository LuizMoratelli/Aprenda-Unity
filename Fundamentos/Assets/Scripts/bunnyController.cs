using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bunnyController : MonoBehaviour {

    [Header("Componentes do Objeto")]
    private Rigidbody2D _rigidbody;

    [Header("Atributos do Objeto")]
    public float jumpForce;

	void Start () {
		_rigidbody = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		if (Input.GetButtonDown("Jump")) { 
            _rigidbody.AddForce(new Vector2(0, jumpForce));
        }
	}
}
