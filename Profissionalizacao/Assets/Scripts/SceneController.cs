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
    private Animator animator;
    private string sceneName;
    #endregion

    #region Public Methods
    public void SceneTransition(string sceneName)
    {
        animator.SetTrigger("FadeOut");
        this.sceneName = sceneName;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    #endregion

    #region Private Methods
    #region Unity Default Methods
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneTransition("Cena2");
        }
    }
    #endregion
    #endregion
}
