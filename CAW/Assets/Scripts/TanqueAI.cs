using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanqueAI : MonoBehaviour
{
    #region Sub-Classes and Structs
	#endregion

	#region Public Members
	#endregion

	#region Private Members
    private GameController gameController;
    [SerializeField] private float delayTiro;
    [SerializeField] private BulletTag bulletTag;
    [SerializeField] private float velocidadeTiro;
    [SerializeField] private int idBullet;
    [SerializeField] private Transform arma;
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        // gameController = FindObjectOfType(typeof(GameController)) as GameController;
    }

    private void OnBecameVisible()
    {
        StartCoroutine("ControlarTiro");
    }

    private IEnumerator ControlarTiro()
    {
        yield return new WaitForSeconds(delayTiro);
        Atirar();
        StartCoroutine("ControlarTiro");
    }

    private void Atirar()
    {
        if (gameController.Player)
        {
            arma.up = gameController.Player.transform.position - transform.position;
            GameObject _bullet = Instantiate(gameController.BulletPrefabs[idBullet], arma.position, arma.localRotation);
            _bullet.transform.tag = bulletTag.ToString();
            _bullet.GetComponent<Rigidbody2D>().velocity = arma.up * velocidadeTiro;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerShot"))
        {
            gameController.Score = 1;
            GameObject _explosion = Instantiate(gameController.ExplosionPrefabs, transform.position, gameController.ExplosionPrefabs.transform.localRotation);
            _explosion.transform.parent = gameController.Cenario;
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
    #endregion
}
