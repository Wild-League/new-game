using System;
using Project.Scripts.Core.Base;
using UnityEngine;

[Serializable]
public class Spell : Card
{
    public Spell(Sprite cardImage, string cardName, float cardCost)
    {
        CardType = CardType.Spell;
        CardImage = cardImage;
        CardName = cardName;
        CardCost = cardCost;
    }

    public virtual void Use()
    {
    }
}