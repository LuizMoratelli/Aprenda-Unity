using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TMMenuController : MonoBehaviour
{
    #region Sub-Classes and Structs
    #endregion

    #region Public Members
    #endregion

    #region Private Members
    private TMAudioController audioController;
    private TMSceneController sceneController;
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #region Unity Default Methods
    private void Start()
    {
        audioController = FindObjectOfType(typeof(TMAudioController)) as TMAudioController;
        sceneController = FindObjectOfType(typeof(TMSceneController)) as TMSceneController;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            audioController.StartCoroutine("ChangeMusic", audioController.MusicaStart);
            sceneController.SceneTransition("TrocaMusicaLoad");
        }
    }
    #endregion
    #endregion
}
