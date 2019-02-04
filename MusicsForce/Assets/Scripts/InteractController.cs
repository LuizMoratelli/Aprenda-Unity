using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class InteractController : MonoBehaviour
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
    #region Untiy Default Methods
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(MusicTags.NextMusic.ToString()))
        {
            GameController.Instance.NextMusic();
        }
        else if (other.CompareTag(MusicTags.PreviousMusic.ToString()))
        {
            GameController.Instance.PreviousMusic();
        }
        else if (other.CompareTag(MusicTags.PauseMusic.ToString()))
        {
            GameController.Instance.PauseMusic();
        }
        else if (other.CompareTag("NextLevel"))
        {
            GameController.Instance.NextLevel(gameObject);
        }
        else if (other.CompareTag("PreviousLevel"))
        {
            GameController.Instance.PreviousLevel(gameObject);
        } else if (other.CompareTag("BallSpawner"))
        {
            other.GetComponent<BallSpawnerController>().SpawnBall();
        }
    }
    #endregion
    #endregion
}
