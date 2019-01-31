using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TMLoadController : MonoBehaviour
{
    #region Sub-Classes and Structs
	#endregion

	#region Public Members
	#endregion

	#region Private Members
    private TMAudioController audioController;
    private TMSceneController sceneController;
    private bool mudandoCena;
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #region Unity Default Methods
    private void Start()
    {
        audioController = FindObjectOfType(typeof(TMAudioController)) as TMAudioController;
        sceneController = FindObjectOfType(typeof(TMSceneController)) as TMSceneController;
        audioController.AudioSourceMusic.loop = false;
    }

    private void Update()
    {
        if (!mudandoCena && !audioController.AudioSourceMusic.isPlaying)
        {
            mudandoCena = true;
            audioController.StartCoroutine("ChangeMusic", audioController.MusicaGrass);
            sceneController.SceneTransition("TrocaMusicaGameplay");
        }
    }
    #endregion
    #endregion
}
