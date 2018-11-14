using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

	public void changeScene(string destinationScene) {
        SceneManager.LoadScene(destinationScene);
    }
}
