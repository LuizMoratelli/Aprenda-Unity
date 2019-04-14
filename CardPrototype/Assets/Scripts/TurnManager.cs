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

    private const float MAX_CHANCE_TO_ATTACK = 0.7f;

    private const float BASE_CHANCE_TO_HIT_EVENT = 0.7f;
    private const float MAX_CHANCE_TO_HIT_EVENT = 0.95f;
    private const float BASE_CHANCE_TO_DEFEND_EVENT = 0.7f;
    private const float MAX_CHANCE_TO_DEFEND_EVENT = 0.95f;

    private const float BASE_CHANCE_TO_HIT_PLAYER = 0.5f;
    private const float MAX_CHANCE_TO_HIT_PLAYER = 0.90f;
    private const float BASE_CHANCE_TO_DEFEND_PLAYER = 0.5f;
    private const float MAX_CHANCE_TO_DEFEND_PLAYER = 0.90f;

    private bool playerHitEvent {
        get {
            return chanceToHitEvent() > getChance();
        }
    }

    private bool eventHitPlayer {
        get {
            return chanceToHitPlayer() > getChance();
        }
    }

    private bool playerDefendEvent {
        get {
            return chanceToDefendEvent() > getChance();
        }
    }

    private bool eventDefendPlayer {
        get {
            return chanceToDefendPlayer() > getChance();
        }
    }

    private bool eventActionisAttack {
        get {
            return chanceToEventAttack() > getChance();
        }
    }

    public void Start()
    {
        player.SetCard(playerCard);
        GenerateNewEvent();
    }

    public void Attack()
    {
        if (eventActionisAttack)
        {
            if (playerHitEvent) player.Attack(cardEvent);
            if (eventHitPlayer) cardEvent.Attack(player);
            else Debug.Log("Inimigo errou");
        }
        else
        {
            if (playerHitEvent && !eventDefendPlayer) player.Attack(cardEvent);
            else Debug.Log("Errou!!!");
        }

        if (cardEvent.defense <= 0) WaitNewEvent();
    }

    public void Defend()
    {
        if (eventActionisAttack)
        {
            if (eventHitPlayer && !playerDefendEvent) cardEvent.Attack(player);
            else Debug.Log("Player Defendeu");
        }
    }

    public void Explore()
    {
        GenerateNewEvent();
    }

    public void Escape()
    {

    }

    private float getChance()
    {
        return Random.Range(0f, 1f);
    }

    private float chanceToEventAttack()
    {
        float chance = (float)cardEvent.defense / (cardEvent.defense + cardEvent.attack);

        return chance < MAX_CHANCE_TO_ATTACK ? chance : MAX_CHANCE_TO_ATTACK;
    }

    private float chanceToDefendPlayer()
    {
        float chance = BASE_CHANCE_TO_DEFEND_PLAYER + (cardEvent.level - player.level) * 5;

        return chance < MAX_CHANCE_TO_DEFEND_PLAYER ? chance : MAX_CHANCE_TO_HIT_PLAYER;
    }

    private float chanceToDefendEvent()
    {
        float chance = BASE_CHANCE_TO_DEFEND_EVENT + (player.level - cardEvent.level) * 5;

        return chance < MAX_CHANCE_TO_DEFEND_EVENT ? chance : MAX_CHANCE_TO_DEFEND_EVENT;
    }

    private float chanceToHitPlayer()
    {
        float chance = BASE_CHANCE_TO_HIT_PLAYER + (cardEvent.level - player.level) * 5;

        return chance < MAX_CHANCE_TO_HIT_PLAYER ? chance : MAX_CHANCE_TO_HIT_PLAYER;
    }

    private float chanceToHitEvent()
    {
        float chance = BASE_CHANCE_TO_HIT_EVENT + (player.level - cardEvent.level) * 5;

        return chance < MAX_CHANCE_TO_HIT_EVENT ? chance : MAX_CHANCE_TO_HIT_EVENT;
    }

    public void WaitNewEvent()
    {
        waitingCard.SetActive(true);
    }

    private void GenerateNewEvent()
    {
        waitingCard.SetActive(false);
        int randomCard = Random.Range(0, eventCards.Length);
        cardEvent.SetCard(eventCards[randomCard]);
    }
}
