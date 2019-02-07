using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMPlataformaMovel : MonoBehaviour
{
    #region Sub-Classes and Structs
    #endregion

    #region Public Members
    #endregion

    #region Private Members
    [SerializeField] private Transform plataforma;
    [SerializeField] private Transform[] pontos;
    [SerializeField] private float speed;

    private int nextTarget;
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #region Unity Default Methods
    private void Start()
    {
        plataforma.position = pontos[0].position;
        nextTarget = 1;
    }

    private void Update()
    {
        plataforma.position = Vector2.MoveTowards(plataforma.position, pontos[nextTarget].position, speed * Time.deltaTime);

        if (plataforma.position == pontos[nextTarget].position)
        {
            nextTarget = (nextTarget + 1) % pontos.Length;
        }
    }
    #endregion
    #endregion
}
