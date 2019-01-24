using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolController : MonoBehaviour
{
    #region Sub-Classes and Structs
	#endregion

	#region Public Members
	#endregion

	#region Private Members
    [SerializeField] private Transform[] checkPoints;
    [SerializeField] private float speed;
    [SerializeField] private float stayDelay;
    [SerializeField] private GameObject enemy;
    private int idCheckPoint;
    private bool movimentar;
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void Start()
    {
        StartCoroutine(StartMove());
    }

    private void Update()
    {
        if (movimentar)
        {
            enemy.transform.localPosition = Vector2.MoveTowards(enemy.transform.position, checkPoints[idCheckPoint].position, speed * Time.deltaTime);

            if (enemy.transform.localPosition == checkPoints[idCheckPoint].position)
            {
                movimentar = false;
                StartCoroutine(StartMove());
            }
        }
    }

    private IEnumerator StartMove()
    {
        yield return new WaitForSeconds(stayDelay);

        idCheckPoint++;
        if (idCheckPoint == checkPoints.Length)
        {
            idCheckPoint = 0;
        }
        Debug.Log(checkPoints[idCheckPoint].position);

        movimentar = true;
    }
    #endregion
}
