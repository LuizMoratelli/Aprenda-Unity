using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    #region Sub-Classes and Structs
    [System.Serializable] private struct Loot
    {
        // Objeto que será instanciado
        public GameObject box;
        // Multiplicador de changes para ser instaciado
        [Range(1, 100)] public int lootMultiplier;
    }
    #endregion

    #region Public Members
    #endregion

    #region Private Members
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private Loot[] loots;
    [Range(0, 100)] [SerializeField] private float lootPercent;
    private PlayerController playerController;
    private Transform armaPosition;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float timeBtwShots;
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void Start()
    {
        playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;
        armaPosition = transform.GetChild(0);
        StartCoroutine(Shot());
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerShot"))
        {
            Destroy(other);
            Die();
        }
    }

    private void SpawnLoot()
    {
        if (loots.Length > 0) 
        { 
            if (lootPercent > Random.Range(0, 100))
            {
                Instantiate(loots[GetRandomLootId()].box, transform.position, transform.rotation);
            }
        }
    }

    private void Die()
    {
        GameObject _explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(_explosion, 0.5f);
        Destroy(gameObject);
        SpawnLoot();
    }

    private int TotalMultiplierLoots()
    {
        //int _totalMultiplier = 0;
        //foreach (Loot _loot in loots)
        //{
        //    _totalMultiplier += _loot.lootMultiplier;
        //}

        //return _totalMultiplier;

        return loots.Sum(_loot => _loot.lootMultiplier);
    }

    private int GetRandomLootId()
    {
        int _currentMultiplier = 0;
        int _lootRandomMultiplier = Random.Range(1, TotalMultiplierLoots() + 1);

        // Pega o item que tiver o numero de multiplicador (+ anteriores) >= ao lootRandom
        for (int i = 0; i < loots.Length; i++)
        {
            _currentMultiplier += loots[i].lootMultiplier;
            if (_currentMultiplier >= _lootRandomMultiplier)
            {
                return i;
            }
        }

        return 0;
    }

    private IEnumerator Shot()
    {
        yield return new WaitForSeconds(Random.Range(timeBtwShots * 0.9f, timeBtwShots * 1.1f));
        armaPosition.right = playerController.transform.position - transform.position;

        GameObject _bullet =  Instantiate(bulletPrefab, armaPosition.position, armaPosition.localRotation);
        _bullet.GetComponent<Rigidbody2D>().velocity = armaPosition.right * bulletSpeed;

        StartCoroutine(Shot());
    }
    #endregion
}
