using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Core.API;
using _Project.Scripts.Core.Base;
using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }

        public List<Card> OwnedCards;

        private List<Card> UsedCards { get; set; }
        private List<Card> AvailableCards { get; set; }
        public string playerName { get; private set; }
        public string playerMail { get; private set; }
        public int playerTrophies { get; private set; }
        public PlayerSide side = PlayerSide.Left;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            ApiController.Instance.GetDeck(2);
        }

        public void GetDeck()
        {
            AvailableCards = new List<Card>(OwnedCards);
            UsedCards = new List<Card>();

            DeckController.Instance.CardFour.cardInfo = GetNextCard();
            DeckController.Instance.CardThree.cardInfo = GetNextCard();
            DeckController.Instance.CardTwo.cardInfo = GetNextCard();
            DeckController.Instance.CardOne.cardInfo = GetNextCard();
            DeckController.Instance.NextCard.cardInfo = GetNextCard();
        }

        public Card GetNextCard(Card discarded)
        {
            Card card;
            UsedCards.Add(discarded);
            if (AvailableCards.Count != 0)
            {
                card = AvailableCards.First();
                AvailableCards.RemoveAt(0);
                UsedCards.Add(discarded);
                return card;
            }

            AvailableCards = new List<Card>(UsedCards);
            UsedCards = new List<Card>();
            card = AvailableCards.First();
            AvailableCards.RemoveAt(0);


            return card;
        }

        private Card GetNextCard()
        {
            if (AvailableCards.Count == 0) return null;
            var card = AvailableCards.First();
            AvailableCards.RemoveAt(0);
            return card;
        }

        public void ResetDeck()
        {
            DeckController.Instance.CardFour.cardInfo = null;
            DeckController.Instance.CardThree.cardInfo = null;
            DeckController.Instance.CardTwo.cardInfo = null;
            DeckController.Instance.CardOne.cardInfo = null;
            DeckController.Instance.NextCard.cardInfo = null;
        }
    }

    public enum PlayerSide
    {
        Left
        , Right
    }
}