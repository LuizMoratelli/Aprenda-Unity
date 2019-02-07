using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #region Unity Default Methods
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
