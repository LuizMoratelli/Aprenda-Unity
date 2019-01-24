using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public class BulletController : MonoBehaviour
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
    private void Start()
    {

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    #endregion
}
