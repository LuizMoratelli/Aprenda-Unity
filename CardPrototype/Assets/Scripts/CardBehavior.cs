using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehavior : MonoBehaviour
{
    public Card card;
    public bool isPlayer;

    private float currentExp;
    public int attack { get; private set; }
    public int defense { get; private set; }
    public int level { get; private set; }

    private CardController cardController;

    private void Awake()
    {
        cardController = GetComponent<CardController>();
    }

    private void UpdateCard()
    {
        cardController.UpdateCard(card.art, level, card.cardName, card.description, attack, defense);
    }

    public void Attack(CardBehavior target)
    {
        Debug.Log("Atacou" + target.card.cardName);
        target.Hit(attack);
    }

    public void Hit(int damage)
    {
        Debug.Log("Hit: " + damage.ToString());
        defense -= damage;

        if (defense <= 0)
        {
            Die();
        }

        UpdateCard();
    }

    public void Die()
    {
        if (isPlayer)
        {
            //Restart ...
        }
        else
        {
            Debug.Log("Die");
        }
    }

    public void SetCard(Card newCard)
    {
        card = newCard;
        level = newCard.level;
        attack = newCard.attack;
        defense = newCard.defense;

        UpdateCard();
    }
}
