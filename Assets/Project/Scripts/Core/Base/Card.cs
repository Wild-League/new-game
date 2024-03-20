using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Core.Base
{
    public class Card
    {
        public int id;
        public string name;
        public string type;
        public float cooldown;
        public float damage;
        public float attack_range;
        public float speed;
        public float life;
        public Sprite img_card;
        public Sprite img_preview;
        public Sprite[] img_attack;
        public Sprite[] img_death;
        public Sprite[] img_walk;
    }

    [Serializable]
    public class CardJSON
    {
        public int id;
        public string name;
        public string type;
        public float cooldown;
        public float damage;
        public float attack_range;
        public float speed;
        public float life;
        public string img_card;
        public string img_preview;
        public string img_attack;
        public string img_death;
        public string img_walk;
    }

    [Serializable]
    public class DeckJSON
    {
        public int id;
        public string name;
        public List<CardJSON> cards;
    }

    public enum CardType
    {
        Spell
        , Building
        , Char
    }
}