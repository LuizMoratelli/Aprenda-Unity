using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TMItem
{
    public string name;
    public Sprite sprite;
    public int price;
    public string description;
}

public class TMLojaController : MonoBehaviour
{
    #region Sub-Classes and Structs
    #endregion

    #region Public Members
    public TMItem[] Itens { get => itens; set => itens = value; }
    #endregion

    #region Private Members
    [SerializeField] private GameObject painelLoja;
    [SerializeField] private TMItem[] itens;
    [SerializeField] private Image displayIcoItem;
    [SerializeField] private Text displayNameItem;
    [SerializeField] private Text displayDescriptionItem;
    [SerializeField] private Text displayPriceItem;

    private int currentIdItem;
    private float originalTimeScale;
    private TMPlayerController player;
    #endregion

    #region Public Methods
    public void AbrirLoja(int idItem)
    {
        currentIdItem = idItem;
        displayIcoItem.sprite = Itens[currentIdItem].sprite;
        displayNameItem.text = Itens[currentIdItem].name;
        displayDescriptionItem.text = Itens[currentIdItem].description;
        displayPriceItem.text = Itens[currentIdItem].price.ToString();

        Time.timeScale = 0;
        painelLoja.SetActive(true);
    }

    public void FecharLoja()
    {
        Time.timeScale = originalTimeScale;
        painelLoja.SetActive(false);
    }

    public void ComprarItem()
    {
        if (player)
        {
            player.AtualizarItens(currentIdItem);
            FecharLoja();
        }
    }
    #endregion

    #region Private Methods
    #region Unity Default Methods
    private void Start()
    {
        originalTimeScale = Time.timeScale;
        FecharLoja();

        player = FindObjectOfType(typeof(TMPlayerController)) as TMPlayerController;
    }
    #endregion
    #endregion
}
