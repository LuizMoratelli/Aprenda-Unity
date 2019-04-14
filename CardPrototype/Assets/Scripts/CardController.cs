using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    public Image art;
    public Text level;
    public Text cardName;
    public Text description;
    public Text attack;
    public Text defense;

    public void UpdateCard(Sprite art, int level, string cardName, string description, int attack, int defense)
    {
        this.art.sprite = art;
        this.level.text = level.ToString();
        this.cardName.text = cardName;
        this.description.text = description;
        this.attack.text = attack.ToString();
        this.defense.text = defense.ToString();
    }
}
