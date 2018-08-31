using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class componentes : MonoBehaviour {

	// Use this for initialization
	void Start () {
        /*
         * gameObject = objeto/classe
         * GameObject = tipo de variável
         */
        
        // Define e exibe o nome do objeto
        this.gameObject.name = "Nome do Objeto";
		print(this.gameObject.name);

        // Exibe se o objeto estiver ativo
		print(this.gameObject.activeSelf);

        // Exibe e define a posição do objeto atual
        print(this.transform.position);
        this.transform.position = new Vector3(10, 0, 0);

        // Define a escala do objeto atual
        this.transform.localScale = new Vector3(2,2,2);

        // Exibe a tag atual do objeto
        print(this.gameObject.tag);

        // Desativa o objeto
        this.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
