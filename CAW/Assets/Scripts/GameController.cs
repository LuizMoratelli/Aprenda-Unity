using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BulletTag
{
    PlayerShot,
    Enemy
}

public enum GameState
{
    intro,
    gameplay
}

public class GameController : MonoBehaviour
{
    #region Sub-Classes and Structs
    #endregion

    #region Public Members
    public GameObject[] BulletPrefabs { get => bulletPrefabs; private set => bulletPrefabs = value; }
    public GameObject ExplosionPrefabs { get => explosionPrefab; private set => explosionPrefab = value; }
    public PlayerController Player { get; set; }
    public int ExtraLifes { 
        get => extraLifes; 
        set {
            extraLifes = value;
            txtVida.text = "x" + value.ToString();
        }
    }
    public int Score {
        get {
            return score;
        }
        set {
            score += value;
            txtScore.text = score.ToString();
        }
    }
    public float IndestructibleTime { get => indestructibleTime; private set => indestructibleTime = value; }
    public Transform Cenario { get => cenario; private set => cenario = value; }
    public GameState CurrentGameState { get; private set; }
    #endregion

    #region Private Members
    [Header("Limites de Movimento")]
    [SerializeField] private Transform limiteSuperior;
    [SerializeField] private Transform limiteInferior;
    [SerializeField] private Transform limiteDireito;
    [SerializeField] private Transform limiteEsquerdo;
    [SerializeField] private Transform spawnPlayer;
    [SerializeField] private Transform posFinalFase;
    //[SerializeField] private Camera mainCamera;
    [SerializeField] private Transform cenario;
    [SerializeField] private float velocidadeFase;

    [Header("Prefabs")]
    [SerializeField] private GameObject[] bulletPrefabs;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private int extraLifes;
    [SerializeField] private float delayToRespawn;
    [SerializeField] private float indestructibleTime;

    [Header("Intro Configs")]
    [SerializeField] private float tamanhoInicial;
    [SerializeField] private float tamanhoFinal;
    [SerializeField] private Transform posicaoInicial;
    [SerializeField] private Transform posicaoDecolagem;
    [SerializeField] private float velocidadeDecolagem;
    private float velocidadeDecolagemAtual;
    private bool isDecolar;
    [SerializeField] private Color corInicialFumaca;
    [SerializeField] private Color corFinalFumaca;
    [SerializeField] private GameObject inicializaoInimigos;

    [Header("UI")]
    [SerializeField] private Text txtScore;
    [SerializeField] private Text txtVida;
    private int score;
    #endregion

    #region Public Methods
    public void Die()
    {
        GameObject _explosion = Instantiate(
            ExplosionPrefabs,
            Player.transform.position,
            ExplosionPrefabs.transform.rotation
        );
        Destroy(_explosion, 0.5f);
        Destroy(Player.gameObject);
        StartCoroutine(NextLife());
    }

    public IEnumerator NextLife()
    {
        yield return new WaitForSeconds(delayToRespawn);

        if (ExtraLifes > 0)
        {
            ExtraLifes--;
            Instantiate(playerPrefab, spawnPlayer.position, spawnPlayer.rotation);
            yield return new WaitForEndOfFrame();
            Player.GetComponent<PlayerController>().StartCoroutine("Indestructible");
        } else
        {
            SceneManager.LoadScene("GameOver");
        }
    }
    #endregion

    #region Private Methods
    private void Start()
    {
        Player = FindObjectOfType(typeof(PlayerController)) as PlayerController;
        StartCoroutine("IntroFase");
        txtScore.text = "0";
        txtVida.text = "x" + ExtraLifes.ToString();
        StartCoroutine("AtivarInimigos");
    }

    private void Update()
    {
        if (Player)
        {
            LimitarMovimentoPlayer();
        }

        if (isDecolar && CurrentGameState == GameState.intro)
        {
            Player.transform.position = Vector2.MoveTowards(Player.transform.position, posicaoDecolagem.position, velocidadeDecolagemAtual * Time.deltaTime);

            if (Player.transform.position == posicaoDecolagem.position)
            {
                StartCoroutine("Subir");
                CurrentGameState = GameState.gameplay;
            }

            Player.Fumaca.color = Color.Lerp(Player.Fumaca.color, corFinalFumaca, velocidadeDecolagemAtual * Time.deltaTime);
        }

        if (Cenario.transform.position.y <= -54)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private void LateUpdate()
    {
        //mainCamera.transform.position = Vector3.MoveTowards(
        //    mainCamera.transform.position,
        //    new Vector3(
        //        mainCamera.transform.position.x,
        //        posFinalCam.transform.position.y,
        //        mainCamera.transform.position.z
        //    ),
        //    velocidadeFase * Time.deltaTime
        //);
        //ControlarPosicaoCamera();

        if (CurrentGameState == GameState.gameplay)
        {
            Cenario.position = Vector2.MoveTowards(
                Cenario.position,
                new Vector2(
                    Cenario.position.x, 
                    posFinalFase.position.y
                ),
                velocidadeFase * Time.deltaTime
            );
        }
    }

    //private void ControlarPosicaoCamera()
    //{
    //    float _cameraX = mainCamera.transform.position.x;
    //    float _playerX = PlayerController.transform.position.x;
    //    float _limiteCamEsquerdo = limiteEsquerdo.position.x + LIMITE_CAMERA_OFFSET;
    //    float _limiteCamDireito = limiteDireito.transform.position.x - LIMITE_CAMERA_OFFSET;

    //    if ((_cameraX > _limiteCamEsquerdo && _cameraX < _limiteCamDireito)
    //        || (_cameraX <= _limiteCamEsquerdo && _playerX > _limiteCamEsquerdo)
    //        || (_cameraX >= _limiteCamDireito && _playerX < _limiteCamDireito))
    //    {
    //        MovimentarCameraHorizontal();
    //    }
        
    //}

    //private void MovimentarCameraHorizontal()
    //{
    //    mainCamera.transform.position = Vector3.Lerp(
    //        mainCamera.transform.position,
    //        new Vector3(Player.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z),
    //        velocidadeLateralCamera * Time.deltaTime
    //    );
    //}

    private void LimitarMovimentoPlayer()
    {
        float clampedX = Mathf.Clamp(
            Player.transform.position.x, 
            limiteEsquerdo.position.x, 
            limiteDireito.position.x
        );

        float clampedY = Mathf.Clamp(
            Player.transform.position.y,
            limiteInferior.position.y,
            limiteSuperior.position.y
        );

        Player.transform.position = new Vector2(clampedX, clampedY);
    }

    private IEnumerator IntroFase()
    {
        Player.Fumaca.color = corInicialFumaca;
        Player.Sombra.SetActive(false);
        Player.transform.localScale = GerarTamanhoCubico(tamanhoInicial);
        Player.transform.position = posicaoInicial.position;
        yield return new WaitForSeconds(.3f);
        isDecolar = true;

        for (velocidadeDecolagemAtual = 0; velocidadeDecolagemAtual < velocidadeDecolagem; velocidadeDecolagemAtual += 0.2f)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator Subir()
    {
        Player.Sombra.SetActive(true);
        for (float s = tamanhoInicial; s < tamanhoFinal; s += 0.025f)
        {
            Player.transform.localScale = GerarTamanhoCubico(s);
            Player.Sombra.transform.localScale = GerarTamanhoCubico(s);
            yield return new WaitForEndOfFrame();
        }
    }

    private Vector3 GerarTamanhoCubico(float tamanho)
    {
        return new Vector3(tamanho, tamanho, tamanho);
    }

    private IEnumerator AtivarInimigos()
    {
        yield return new WaitForSeconds(5);
        inicializaoInimigos.SetActive(true);
    }
    #endregion
}
