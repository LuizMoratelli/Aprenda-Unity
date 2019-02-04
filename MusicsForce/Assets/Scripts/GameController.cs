using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public enum MusicTags
{
    PreviousMusic,
    PauseMusic,
    NextMusic
}

public enum MusicState
{
    start,
    playing,
    paused,
    ended
}

[System.Serializable]
public class Level
{
    public string idLevel;
    public GameObject nextLevelPortal;
    public GameObject nextLevelPosition;
    public GameObject previousLevelPortal;
    public GameObject previousLevelPosition;
    //public GameObject cameraPosition;
}

[System.Serializable]
public class Music
{
    public AudioClip audioClip;
    public Tilemap tileMap;
    public CompositeCollider2D tileMapCollider;
    public Color tileMapDisableColor = Color.white;
    public Color tileMapEnableColor = Color.white;
    public Sprite ballSprite;
    public PhysicsMaterial2D ballMaterial;
    public float ballGravity;
    public Color startColor;
    public Color endColor;
}

public class GameController : MonoBehaviour
{
    #region Sub-Classes and Structs
    #endregion

    #region Public Members
    public static GameController Instance { get; private set; }
    public MusicState CurrentMusicState { get; set; }
    //public AudioClip[] AudioClips { get => audioClips; private set => audioClips = value; }
    public int IdMusic { get; private set; }
    public int CurrentIdLevel {
        get => currentIdLevel;
        private set {
            currentIdLevel = value;
        }
    }
    #endregion

    #region Private Members
    [SerializeField] private Music[] musics;
    [SerializeField] private Sprite pauseSprite;
    [SerializeField] private Sprite playSprite;
    private AudioSource audioSource;
    private GameObject[] pausePlay;

    private int currentIdLevel;
    [SerializeField] private Level[] levels;
    private CutsceneController cutsceneController;

    [Header("Default Ball Configuration")]
    private GameObject[] balls;
    [SerializeField] private Sprite ballSprite;
    [SerializeField] private PhysicsMaterial2D ballMaterial;
    [SerializeField] private float ballGravity;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    #endregion

    #region Public Methods
    public void PauseMusic()
    {
        if (CurrentMusicState.Equals(MusicState.playing) || CurrentMusicState.Equals(MusicState.ended))
        {
            audioSource.Pause();
            CurrentMusicState = MusicState.paused;

            foreach (GameObject _pausePlay in pausePlay)
            {
                _pausePlay.GetComponent<SpriteRenderer>().sprite = playSprite;
            }

            DisableAllTileMaps();
            UpdateBalls();
        }
        else
        {
            audioSource.Play();
            CurrentMusicState = MusicState.playing;
            EnableTileMap(IdMusic);
            UpdateBalls(IdMusic);
        }
    }

    public void NextMusic(int increaseId = 1)
    {
        //IdMusic = (IdMusic + increaseId) % musics.Length;
        IdMusic += increaseId;
        if (IdMusic < 0)
        {
            IdMusic = 0;
        }
        else if (IdMusic >= musics.Length)
        {
            IdMusic = musics.Length - 1;
        }

        SetMusic(IdMusic);
    }

    public void PreviousMusic()
    {
        NextMusic(-1);
    }

    public void SetMusic(int idMusic = 0)
    {
        audioSource.clip = musics[IdMusic].audioClip;
        EnableTileMap(IdMusic);
        UpdateBalls(IdMusic);
        audioSource.Play();
        StartCoroutine("SetupAudioSource");
    }

    public void NextLevel(GameObject toTeleport)
    {
        //if (toTeleport.CompareTag("Player"))
        //{
        //    cutsceneController.DisableDialogue();
        //}

        //CurrentIdLevel++;
        //if (CurrentIdLevel >= levels.Length)
        //{
        //    CurrentIdLevel = levels.Length - 1;
        //}

        //toTeleport.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //toTeleport.transform.position = levels[CurrentIdLevel].previousLevelPosition.transform.position;

        if (toTeleport.CompareTag("Player"))
        {
            UpdateCameraPosition(CurrentIdLevel);
            cutsceneController.EnableDialogue();

            CurrentIdLevel++;
            if (CurrentIdLevel >= levels.Length)
            {
                CurrentIdLevel = levels.Length - 1;
            }

            UpdateCameraPosition(CurrentIdLevel);
            cutsceneController.EnableDialogue();

            toTeleport.transform.position = levels[CurrentIdLevel].previousLevelPosition.transform.position;
        }
        else
        {
            toTeleport.transform.position = new Vector2(
                levels[CurrentIdLevel + 1].previousLevelPosition.transform.position.x + 0.5f,
                levels[CurrentIdLevel + 1].previousLevelPosition.transform.position.y
            );
        }


        toTeleport.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void PreviousLevel(GameObject toTeleport)
    {
        //CurrentIdLevel--;
        //if (CurrentIdLevel < 0)
        //{
        //    CurrentIdLevel =  0;
        //}

        //toTeleport.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //toTeleport.transform.position = levels[CurrentIdLevel].nextLevelPosition.transform.position;

        //if (toTeleport.CompareTag("Player"))
        //{
        //    UpdateCameraPosition(CurrentIdLevel);
        //} else
        //{
        //    CurrentIdLevel++;
        //}


        if (toTeleport.CompareTag("Player"))
        {
            CurrentIdLevel--;
            if (CurrentIdLevel < 0)
            {
                CurrentIdLevel = 0;
            }

            UpdateCameraPosition(CurrentIdLevel);
            toTeleport.transform.position = levels[CurrentIdLevel].nextLevelPosition.transform.position;
        }
        else
        {
            toTeleport.transform.position = new Vector2(
                levels[CurrentIdLevel - 1].nextLevelPosition.transform.position.x - 0.5f,
                levels[CurrentIdLevel - 1].nextLevelPosition.transform.position.y
            );
        }

        toTeleport.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void ResetLevel()
    {
        GameObject _player = GameObject.FindGameObjectWithTag("Player");
        _player.transform.position = levels[CurrentIdLevel].previousLevelPosition.transform.position;
        UpdateCameraPosition(CurrentIdLevel);
    }

    public void UpdateBalls(int idMusic = -1)
    {
        balls = GameObject.FindGameObjectsWithTag("Ball");
        Sprite _ballSprite = ballSprite;
        PhysicsMaterial2D _ballMaterial = ballMaterial;
        float _ballGravity = ballGravity;
        Color _startColor = startColor;
        Color _endColor = endColor;

        if (idMusic >= 0)
        {
            _ballSprite = musics[idMusic].ballSprite;
            _ballMaterial = musics[idMusic].ballMaterial;
            _ballGravity = musics[idMusic].ballGravity;
            _startColor = musics[idMusic].startColor;
            _endColor = musics[idMusic].endColor;
        }

        foreach (GameObject ball in balls)
        {
            ball.GetComponent<SpriteRenderer>().sprite = _ballSprite;
            ball.GetComponent<Collider2D>().sharedMaterial = _ballMaterial;
            ball.GetComponent<Rigidbody2D>().gravityScale = _ballGravity;
            ball.transform.GetChild(0).GetComponent<TrailRenderer>().startColor = _startColor;
            ball.transform.GetChild(0).GetComponent<TrailRenderer>().endColor = _endColor;
        }
    }
    #endregion

    #region Private Methods
    #region Unity Default Methods 
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        CurrentIdLevel = 0;
        Cursor.visible = false;
        pausePlay = GameObject.FindGameObjectsWithTag(MusicTags.PauseMusic.ToString());
        cutsceneController = FindObjectOfType(typeof(CutsceneController)) as CutsceneController;
        audioSource = GetComponent<AudioSource>();
        SetMusic();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetLevel();
        }

        if (CurrentMusicState.Equals(MusicState.playing) && !audioSource.isPlaying)
        {
            CurrentMusicState = MusicState.ended;
            PauseMusic();
        }
    }
    #endregion

    private IEnumerator SetupAudioSource()
    {
        yield return new WaitForEndOfFrame();
        audioSource.volume = 0.5f;
        audioSource.loop = false;
        audioSource.Play();
        CurrentMusicState = MusicState.playing;
    }

    private void DisableAllTileMaps()
    {
        foreach (Music music in musics)
        {
            music.tileMapCollider.isTrigger = true;
            music.tileMap.color = music.tileMapDisableColor;
        }
    }

    private void EnableTileMap(int idMusic)
    {
        DisableAllTileMaps();

        foreach (GameObject _pausePlay in pausePlay)
        {
            _pausePlay.GetComponent<SpriteRenderer>().sprite = pauseSprite;
        }

        musics[idMusic].tileMapCollider.isTrigger = false;
        musics[idMusic].tileMap.color = musics[idMusic].tileMapEnableColor;
    }

    private void UpdateCameraPosition(int idLevel)
    {
        Vector3 _mainCameraPosition = Camera.main.transform.position;
        Camera.main.transform.position = new Vector3(idLevel * 18 - 18, _mainCameraPosition.y, _mainCameraPosition.z);
    }
    #endregion
}
