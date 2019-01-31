using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TMAudioController : MonoBehaviour
{
    #region Sub-Classes and Structs
    #endregion

    #region Public Members
    public AudioClip MusicaTitulo { get => musicaTitulo; private set => musicaTitulo = value; }
    public AudioClip MusicaStart { get => musicaStart; private set => musicaStart = value; }
    public AudioClip MusicaGrass { get => musicaGrass; private set => musicaGrass = value; }
    public AudioClip MusicaWater { get => musicaWater; private set => musicaWater = value; }
    public AudioSource AudioSourceMusic { get => audioSourceMusic; private set => audioSourceMusic = value; }
    public AudioSource AudioSourceFX { get => audioSourceFX; private set => audioSourceFX = value; }
    #endregion

    #region Private Members
    [SerializeField] private AudioSource audioSourceMusic;
    [SerializeField] private AudioSource audioSourceFX;
    [SerializeField] private AudioClip musicaTitulo;
    [SerializeField] private AudioClip musicaStart;
    [SerializeField] private AudioClip musicaGrass;
    [SerializeField] private AudioClip musicaWater;
    #endregion

    #region Public Methods
    public IEnumerator ChangeMusic(AudioClip audioClip)
    {
        float _volumeAtual = AudioSourceMusic.volume;
        for (float v = _volumeAtual; v > 0; v -= .02f)
        {
            AudioSourceMusic.volume = v;
            yield return new WaitForEndOfFrame();
        }

        AudioSourceMusic.clip = audioClip;
        AudioSourceMusic.loop = true;
        AudioSourceMusic.Play();

        for (float v = 0; v < _volumeAtual; v += .02f)
        {
            AudioSourceMusic.volume = v;
            yield return new WaitForEndOfFrame();
        }
    }
    #endregion

    #region Private Methods
    #region Unity Default Methods
    private void Start()
    {
        GetAudioSourceConfiguration();
        SceneManager.LoadScene("TrocaMusicaMenu");
    }

    #endregion
    private void GetAudioSourceConfiguration()
    {
        if (PlayerPrefs.GetInt("ValoresIniciais") == 0)
        {
            PlayerPrefs.SetInt("ValoresIniciais", 1);
            PlayerPrefs.SetFloat("VolumeMusica", 1);
            PlayerPrefs.SetFloat("VolumeFX", .8f);
        }

        AudioSourceMusic.volume = PlayerPrefs.GetFloat("VolumeMusica");
        AudioSourceFX.volume = PlayerPrefs.GetFloat("VolumeFX");
        AudioSourceMusic.clip = MusicaTitulo;
        AudioSourceMusic.Play();
        AudioSourceMusic.loop = true;
    }
    #endregion
}
