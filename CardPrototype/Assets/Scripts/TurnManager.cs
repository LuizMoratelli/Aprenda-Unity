using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public CardBehavior player;
    public CardBehavior cardEvent;
    public Card[] eventCards;
    public Card playerCard;
    public GameObject waitingCard;

    public void Start()
    {
        player.SetCard(playerCard);
        GenerateNewEvent();
    }

    public void Attack()
    {
        player.Attack(cardEvent);

        if (cardEvent.defense <= 0)
        {
            WaitNewEvent();
        }
        else
        {
            cardEvent.Attack(player);
        }
    }

    public void Defend()
    {

    }

    public void Escape()
    {

    }

    public void WaitNewEvent()
    {
        waitingCard.SetActive(true);
    }

    public void Explore()
    {
        GenerateNewEvent();
    }

    private void GenerateNewEvent()
    {
        waitingCard.SetActive(false);
        int randomCard = Random.Range(0, eventCards.Length);
        cardEvent.SetCard(eventCards[randomCard]);
    }
}
