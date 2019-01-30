using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region Sub-Classes and Structs
    #endregion

    #region Public Members
    public int Score {
        get {
            return score;
        }
        set {
            score = value;
            scoreText.text = score.ToString();
        }
    }
    public float TimeLeft { 
        get => timeLeft;
        set {
            timeLeft = value;

            if (timeLeft <= 0)
            {
                LoadScene("GameOver");
            }

            timeText.text = timeLeft.ToString();
        }
    }
    #endregion

    #region Private Members
    [SerializeField] private Text scoreText;
    [SerializeField] private float initialTimeLeft;
    [SerializeField] private Text timeText;
    private AudioSource gameAudioSource;

    private float timeLeft;
    private int score;
    #endregion

    #region Public Methods
    public void AddScore(int scoreAmount)
    {
        Score += scoreAmount;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void PlaySound(AudioClip sound)
    {
        gameAudioSource.PlayOneShot(sound);
    }
    #endregion

    #region Private Methods
    private void Start()
    {
        gameAudioSource = GetComponent<AudioSource>();
        TimeLeft = initialTimeLeft;
        Score = 0;
        StartCoroutine("UpdateTimeLeft");
    }

    private IEnumerator UpdateTimeLeft()
    {
        yield return new WaitForSeconds(1);
        TimeLeft -= 1;
        StartCoroutine("UpdateTimeLeft");
    }
    #endregion
}
