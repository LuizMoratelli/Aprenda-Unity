using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMLifeController : MonoBehaviour
{
    #region Sub-Classes and Structs
    #endregion

    #region Public Members
    public int CurrentLife {
        get => currentLife;
        set {
            currentLife = value;

            if (currentLife <= 0)
            {
                Destroy(gameObject);
                GameObject _effect = Instantiate(TMGameController.Instance.HitPrefab, transform.position, transform.rotation);
                Destroy(_effect, 0.5f);
            }
        }
    }
    #endregion

    #region Private Members
    [SerializeField] private int currentLife;

    private bool isHit;
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #region Unity Collision Methods
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HammerHit"))
        {
            DamageController(TMGameController.Instance.HammerDamage);
        }
        else if (other.CompareTag("BallHit"))
        {
            DamageController(TMGameController.Instance.BallDamage, other.gameObject);
        }
    }
    #endregion
    #region Unity Default Methods
    #endregion
    #region IEnumerators
    private IEnumerator WaitHit()
    {
        yield return new WaitForSeconds(0.5f);
        isHit = false;
    }
    #endregion
    private void DamageController(int amountOfDamage, GameObject attackerObject = null)
    {
        if (attackerObject)
        {
            Destroy(attackerObject);
        }

        if (!isHit)
        {
            isHit = true;
            StartCoroutine("WaitHit");
            CurrentLife = CurrentLife - amountOfDamage;
        }
    }
    #endregion
}
