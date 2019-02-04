using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnerController : MonoBehaviour
{
    #region Sub-Classes and Structs
	#endregion

	#region Public Members
	#endregion

	#region Private Members
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject currentBall;
	#endregion

	#region Public Methods
    public void SpawnBall()
    {
        if (currentBall)
        {
            Destroy(currentBall);
        }

        currentBall = Instantiate(ballPrefab, spawnPoint.transform.position, spawnPoint.transform.localRotation);
        GameController.Instance.UpdateBalls(GameController.Instance.IdMusic);
    }
	#endregion

	#region Private Methods
	#endregion
}
