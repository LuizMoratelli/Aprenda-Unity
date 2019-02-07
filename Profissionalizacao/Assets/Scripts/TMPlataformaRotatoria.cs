using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMPlataformaRotatoria : MonoBehaviour
{
    #region Sub-Classes and Structs
	#endregion

	#region Public Members
	#endregion

	#region Private Members
    [SerializeField] private Transform eixo;
    [SerializeField] private Transform plataforma;
    [SerializeField] private float speed;
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #region Unity Default Methods
    private void Update()
    {
        eixo.Rotate(Vector3.forward * speed * Time.deltaTime);
        plataforma.Rotate(-Vector3.forward * speed * Time.deltaTime);
    }
    #endregion
    #endregion
}
