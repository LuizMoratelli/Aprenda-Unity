using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    #region Sub-Classes and Structs
    private enum Direcao
    {
        Cima,
        Baixo,
        Direita,
        Esquerda
    }
	#endregion

	#region Public Members
	#endregion

	#region Private Members
    [SerializeField] private float velocidadeMovimento;
    [SerializeField] private Direcao direcaoMovimento;
    [SerializeField] private float pontoInicialCurva;
    [SerializeField] private float grausCurva;
    [SerializeField] private float grausIncrementar;
    private float grausCurvados;
    private bool isCurva;
    private float rotacaoZ;
    [SerializeField] private Transform arma;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float timeBtwShots;
    private GameController gameController;
    [SerializeField] private int idBullet;
    [SerializeField] private BulletTag bulletTag;
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void Start()
    {
        rotacaoZ = transform.rotation.eulerAngles.z;
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
    }

    private void Update()
    {
        ControlarCurva();
    }

    private void OnBecameVisible()
    {
        StartCoroutine(ControlarTiro());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerShot"))
        {
            Die();
            Destroy(other.gameObject);
        }
    }

    private void Die()
    {
        GameObject _explosion = Instantiate(
            gameController.ExplosionPrefabs,
            transform.position,
            gameController.ExplosionPrefabs.transform.rotation
        );
        Destroy(_explosion, 0.5f);
        Destroy(gameObject);
    }

    private void Atirar()
    {
        GameObject _bullet = Instantiate(gameController.BulletPrefabs[idBullet], arma.position, transform.localRotation);
        _bullet.transform.tag = bulletTag.ToString();
        _bullet.GetComponent<Rigidbody2D>().velocity = -transform.up * bulletSpeed;
    }

    private void ControlarCurva()
    {
        switch (direcaoMovimento)
        {
            case (Direcao.Cima):
                {
                    if (transform.position.y >= pontoInicialCurva && !isCurva)
                    {
                        isCurva = true;
                    }
                    break;
                }
            case (Direcao.Baixo):
                {
                    if (transform.position.y <= pontoInicialCurva && !isCurva)
                    {
                        isCurva = true;
                    }
                    break;
                }
            case (Direcao.Direita):
                {
                    if (transform.position.x >= pontoInicialCurva && !isCurva)
                    {
                        isCurva = true;
                    }
                    break;
                }
            case (Direcao.Esquerda):
                {
                    if (transform.position.x <= pontoInicialCurva && !isCurva)
                    {
                        isCurva = true;
                    }
                    break;
                }
        }

        if (isCurva && grausCurvados < grausCurva)
        {
            rotacaoZ += grausIncrementar;
            transform.rotation = Quaternion.Euler(0, 0, rotacaoZ);
            grausCurvados += Mathf.Abs(grausIncrementar);
        }

        transform.Translate(Vector3.down * velocidadeMovimento * Time.deltaTime);
    }

    private IEnumerator ControlarTiro()
    {
        yield return new WaitForSeconds(timeBtwShots);
        Atirar();
        StartCoroutine(ControlarTiro());
    }
    #endregion
}
