using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMDontDestroy : MonoBehaviour
{
    #region Sub-Classes and Structs
    #endregion

    #region Public Members
    #endregion

    #region Private Members
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #region Unity Default Methods
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    #endregion
    #endregion
}
