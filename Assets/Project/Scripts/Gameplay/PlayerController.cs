using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Core.Base;
using Project.Scripts.Gameplay;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    public List<Card> OwnedCards { get; private set; }
    public List<Card> UsedCards { get; private set; }
    public List<Card> AvailableCards { get; private set; }
    public string playerName { get; private set; }
    public string playerMail { get; private set; }
    public int playerTrophies { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CreateFakeDeck();
        GetDeck();
    }

    private void CreateFakeDeck()
    {
        var card1 = new Minion(null, "Carta 1", 2.5f, 10);
        var card2 = new Minion(null, "Carta 2", 5.5f, 10);
        var card3 = new Minion(null, "Carta 3", 1.5f, 10);
        var card4 = new Minion(null, "Carta 4", 4.5f, 10);
        var card5 = new Minion(null, "Carta 5", 7.5f, 10);
        var card6 = new Minion(null, "Carta 6", 8.5f, 10);
        var card7 = new Minion(null, "Carta 7", 10.5f, 10);

        OwnedCards = new List<Card>()
        {
            card1, card2, card3, card4, card5, card6, card7
        };
    }

    public void GetDeck()
    {
        AvailableCards = OwnedCards;
        UsedCards = new List<Card>();

        DeckController.Instance.CardFour.cardInfo = GetNextCard();
        DeckController.Instance.CardThree.cardInfo = GetNextCard();
        DeckController.Instance.CardTwo.cardInfo = GetNextCard();
        DeckController.Instance.CardOne.cardInfo = GetNextCard();
        DeckController.Instance.NextCard.cardInfo = GetNextCard();
    }

    public Card GetNextCard(Card discarded)
    {
        var card = AvailableCards.First();
        AvailableCards.RemoveAt(0);
        UsedCards.Add(discarded);

        if (AvailableCards.Count != 0) return card;

        AvailableCards = new List<Card>(UsedCards);
        UsedCards = new List<Card>();

        return card;
    }

    private Card GetNextCard()
    {
        var card = AvailableCards.First();
        AvailableCards.RemoveAt(0);

        if (AvailableCards.Count != 0) return card;

        AvailableCards = new List<Card>(UsedCards);
        UsedCards = new List<Card>();

        return card;
    }
}