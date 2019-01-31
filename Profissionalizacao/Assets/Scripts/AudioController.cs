using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    #region Sub-Classes and Structs
	#endregion

	#region Public Members
	#endregion

	#region Private Members
    [SerializeField] private Slider musicSlider;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private GameObject painelFume;
    #endregion

    #region Public Methods
    public void SetVolumeMusic()
    {
        musicSource.volume = musicSlider.value;
    }

    public void SaveVolumeConfiguration()
    {
        PlayerPrefs.SetFloat("volumeMusic", musicSlider.value);
    }
    #endregion

    #region Private Methods
    #region Unity Default Methods
    private void Start()
    {
        if (PlayerPrefs.GetInt("Jogatina") == 0)
        {
            PlayerPrefs.SetFloat("volumeMusic", 0.5f);
            PlayerPrefs.SetInt("Jogatina", 1);
        }

        float _musicVolume = PlayerPrefs.GetFloat("volumeMusic");
        musicSource.volume = _musicVolume;
        musicSlider.value = _musicVolume;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!painelFume.activeInHierarchy)
            {
                painelFume.SetActive(true);
            } else
            {
                painelFume.SetActive(false);
            }
        }
    }
    #endregion
    #endregion
}
