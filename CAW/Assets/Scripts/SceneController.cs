using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    #region Sub-Classes and Structs
	#endregion

	#region Public Members
	#endregion

	#region Private Members
	#endregion

	#region Public Methods
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
	#endregion

	#region Private Methods
	#endregion
}
