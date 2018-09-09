using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class uiBehaviour : MonoBehaviour {

	public void changeScene (string destinationScene) {
        SceneManager.LoadScene(destinationScene);
    }
}
