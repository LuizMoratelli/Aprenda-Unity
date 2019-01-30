using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Sub-Classes and Structs
	#endregion

	#region Public Members
	#endregion

	#region Private Members
    private PlayerController player;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float speed;
    [SerializeField] private float maxDistanceOfPlayer;
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #region Unity Default Methods
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();    
    }

    private void Update()
    {
        if (player)
        {
            if (Vector2.Distance(transform.position, player.transform.position) > maxDistanceOfPlayer)
            {
                if (transform.position.x > minX && transform.position.x < maxX
                    || transform.position.x <= minX && player.transform.position.x > transform.position.x + maxDistanceOfPlayer
                    || transform.position.x >= maxX && player.transform.position.x < transform.position.x - maxDistanceOfPlayer)
                {
                    transform.position = Vector3.Lerp(
                        transform.position,
                        new Vector3(
                            player.transform.position.x,
                            transform.position.y,
                            transform.position.z
                        ), 
                        speed * Time.deltaTime
                    );
                }
            }
        }
    }
    #endregion
    #endregion
}
