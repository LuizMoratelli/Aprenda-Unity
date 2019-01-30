using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpanwCatController : MonoBehaviour
{
    #region Sub-Classes and Structs
	#endregion

	#region Public Members
    public bool CanInstantiate { get; set; }
	#endregion

	#region Private Members
    [SerializeField] private float timeBtwAttacks;
    [SerializeField] private float speed;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private int idSpawnPoint = 0;
    [SerializeField] private GameObject catPrefab;
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void Start()
    {
        CanInstantiate = true;
    }

    private void Update()
    {
        if (CanInstantiate)
        {
            StartCoroutine("SpawnCat");
        }
    }

    private IEnumerator SpawnCat()
    {
        CanInstantiate = false; 
        yield return new WaitForSeconds(timeBtwAttacks);
        GameObject _cat = Instantiate(
            catPrefab, 
            new Vector2(
                spawnPoints[idSpawnPoint].transform.position.x,
                spawnPoints[idSpawnPoint].transform.localPosition.y
            ),
            spawnPoints[idSpawnPoint].transform.localRotation
        );

        if (idSpawnPoint == 0)
        {
            _cat.GetComponent<Rigidbody2D>().velocity = new Vector2(speed * Time.deltaTime, 0);
            idSpawnPoint++;
        } else
        {
            _cat.transform.localScale = new Vector2(-_cat.transform.localScale.x, _cat.transform.localScale.y);
            _cat.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed * Time.deltaTime, 0);
            idSpawnPoint--;
        }

    }
    #endregion
}
