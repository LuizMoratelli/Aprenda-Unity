using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColetavelType
{
    pontuacao,
    tempo
}

public class ColetavelController : MonoBehaviour
{
    #region Sub-Classes and Structs
	#endregion

	#region Public Members
    public int ScoreAmount { get => scoreAmount; set => scoreAmount = value; }
    public ColetavelType Tipo { get => tipo; set => tipo = value; }
    #endregion

    #region Private Members
    [SerializeField] private int scoreAmount;
    [SerializeField] private ColetavelType tipo;
    [SerializeField] private AudioClip pickupSound;

    private GameController gameController;
    #endregion

    #region Public Methods
    public void Pickup()
    {
        gameController.PlaySound(pickupSound);
        Destroy(gameObject);
    }
    #endregion

    #region Private Methods
    #region Unity Default Methods
    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }
    #endregion
    #endregion
}
