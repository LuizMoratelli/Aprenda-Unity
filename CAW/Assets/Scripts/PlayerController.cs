using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    #region Sub-Classes and Structs

    #endregion

    #region Public Members

    #endregion

    #region Private Members
    private Rigidbody2D playerRigidbody;
    private Transform armaPosition;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float speed;
    [SerializeField] private float bulletSpeed;
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        armaPosition = transform.GetChild(0);
    }

    private void Update()
    {
        Vector2 _moveInput = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        ).normalized;

        playerRigidbody.velocity = _moveInput * speed;

        if (Input.GetButtonDown("Fire1"))
        {
            Shot();
        }
    }

    private void Shot()
    {
        GameObject _bullet = Instantiate(bulletPrefab, armaPosition.position, Quaternion.identity);
        _bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);
    }
    #endregion
}
