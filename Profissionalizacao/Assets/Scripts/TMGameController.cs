﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TMGameController : MonoBehaviour
{
    #region Sub-Classes and Structs
    #endregion

    #region Public Members
    public static TMGameController Instance { get; set; }
    public GameObject HitPrefab { get => hitPrefab; set => hitPrefab = value; }
    public int HammerDamage { get => hammerDamage; set => hammerDamage = value; }
    public int BallDamage { get => ballDamage; set => ballDamage = value; }
    #endregion

    #region Private Members
    [Header("Damage Configuration")]
    [SerializeField] private GameObject hitPrefab;
    [SerializeField] private int hammerDamage;
    [SerializeField] private int ballDamage;

    [Header("UI")]
    [SerializeField] private Text extraLeftTxt;
    [SerializeField] private Text coinsTxt;
    [SerializeField] private Text scoreTxt;
    [SerializeField] private Text languageTxt;
    private LoadXMLFile loadXMLFile;

    private int rings;
    private int score;
    #endregion

    #region Public Methods
    public void ColetarRings()
    {
        rings++;
        coinsTxt.text = rings.ToString();
        AddScore(10);
    }
    public void AddScore(int scoreAmount)
    {
        score += scoreAmount;
        scoreTxt.text = score.ToString();
    }
    public void TrocarIdioma(string idioma)
    {
        PlayerPrefs.SetString("idioma", idioma);
        loadXMLFile.LoadXMLData();
        languageTxt.text = loadXMLFile.InterfaceTitulo[0];
    }
    #endregion

    #region Private Methods
    #region Unity Default Methods
    private void Start()
    {
        loadXMLFile = FindObjectOfType(typeof(LoadXMLFile)) as LoadXMLFile;
        languageTxt.text = loadXMLFile.InterfaceTitulo[0];
    }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }
    #endregion
    #endregion
}
