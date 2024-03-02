using System;
using Project.Scripts.Core.Base;
using UnityEngine;

[Serializable]
public class Building : Card, ISpawnable
{
    public int CardMaxHealth { get; }
    public int CardActualHealth { get; set; }

    public Building(string cardName, Sprite cardImage, float cardCost, int cardMaxHealth)
    {
        CardType = CardType.Building;
        CardName = cardName;
        CardImage = cardImage;
        CardCost = cardCost;
        CardMaxHealth = cardMaxHealth;
    }

    public void Spawn()
    {
        throw new System.NotImplementedException();
    }

    public void OnSpawn()
    {
        throw new System.NotImplementedException();
    }

    public void OnDie()
    {
        throw new System.NotImplementedException();
    }
}