using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMAIBasica : MonoBehaviour
{
    #region Sub-Classes and Structs
    #endregion

    #region Public Members
    #endregion

    #region Private Members
    [SerializeField] private Transform enemy;
    [SerializeField] private Transform[] pontos;
    [SerializeField] private float speed;

    private int nextTarget;
    private bool lookLeft;
    private SpriteRenderer spriteRenderer;
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #region Unity Default Methods
    private void Start()
    {
        enemy.position = pontos[0].position;
        nextTarget = 1;
        lookLeft = true;
        spriteRenderer = enemy.gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (enemy)
        {
            enemy.position = Vector2.MoveTowards(enemy.position, pontos[nextTarget].position, speed * Time.deltaTime);

            if (enemy.position == pontos[nextTarget].position)
            {
                nextTarget = (nextTarget + 1) % pontos.Length;
            }

            if (enemy.position.x < pontos[nextTarget].position.x && lookLeft)
            {
                Flip();
            }
            else if (enemy.position.x > pontos[nextTarget].position.x && !lookLeft)
            {
                Flip();
            }
        }
    }
    #endregion
    private void Flip()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
        lookLeft = !lookLeft;
    }
    #endregion
}
