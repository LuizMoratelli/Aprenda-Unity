using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMItemLoja : MonoBehaviour
{
    #region Sub-Classes and Structs
    #endregion

    #region Public Members
    #endregion

    #region Private Members
    [SerializeField] private int idItem;

    private TMLojaController loja;
    private SpriteRenderer spriteRenderer;
    #endregion

    #region Public Methods
    public void AbrirLoja()
    {
        loja.AbrirLoja(idItem);
    }
    #endregion

    #region Private Methods
    #region Unity Default Methods
    private void Start()
    {
        loja = FindObjectOfType(typeof(TMLojaController)) as TMLojaController;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = loja.Itens[idItem].sprite;
    }
    #endregion
    #endregion
}
