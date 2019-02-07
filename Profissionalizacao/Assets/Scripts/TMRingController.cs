using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMRingController : MonoBehaviour, ITMColetavelController
{
    #region Sub-Classes and Structs
    #endregion

    #region Public Members
    #endregion

    #region Private Members
    #endregion

    #region Public Methods
    public void Coletavel()
    {
        TMGameController.Instance.ColetarRings();
        Destroy(gameObject);
    }
    #endregion

    #region Private Methods
    #region Unity Default Methods
    private void Start()
    {
        
    }
    #endregion
    #endregion
}
