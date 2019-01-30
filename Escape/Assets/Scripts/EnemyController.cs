using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    #region Sub-Classes and Structs
    #endregion

    #region Public Members
    #endregion

    #region Private Members
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip alertSound;
    private Rigidbody2D enemyRB;
    private Animator enemyAnim;
    private SpanwCatController spawnCatController;
    private GameController gameController;
    private bool canDestroy = true;
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #region Unity Default Methods
    private void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        enemyAnim = GetComponent<Animator>();
        spawnCatController = FindObjectOfType<SpanwCatController>();
        gameController = FindObjectOfType<GameController>();
        gameController.PlaySound(alertSound);
    }

    private void OnBecameInvisible()
    {
        if (canDestroy)
        {
            spawnCatController.CanInstantiate = true;
            Destroy(gameObject);
        }
    }
    #endregion
    #region Unity Physics Methods
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canDestroy = false;
            Destroy(other.gameObject);
            StartCoroutine("EatRat");
        }
    }
    #endregion

    private IEnumerator EatRat()
    {
        gameController.PlaySound(attackSound);
        enemyAnim.SetTrigger("runRat");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("GameOver");
    }

    //private IEnumerator CatAttack()
    //{
    //    yield return new WaitForSeconds(timeBtwAttacks);
    //    transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    //    enemyRB.velocity = new Vector2(speed * Time.deltaTime * toRight, 0);
    //    yield return new WaitForEndOfFrame();
    //    toRight = -toRight;
    //}
    #endregion
}
