using UnityEngine;

namespace Project.Scripts.Core.Base
{
    public abstract class Card
    {
        public int CardID { get; protected set; }
        public string CardName { get; protected set; }
        public CardType CardType { get; protected set; }
        public float CardCost { get; protected set; }
        public float CardDamage { get; protected set; }
        public float CardRange { get; protected set; }
        public float CardSpeed { get; protected set; }
        public float CardLife { get; protected set; }

        public Sprite CardImage { get; protected set; }
        public Vector2 CardDimensions { get; protected set; }
    }

    public struct CardBase
    {
        
    }
    
    public enum CardType
    {
        Spell
        , Building
        , Minion
    }
}