using System;
using JetBrains.Annotations;
using Project.Scripts.Core.Base;
using UnityEngine;

[Serializable]
public class Minion : Card, ISpawnable
{
    public int CardActualHealth { get; set; }
    public int CardMaxHealth { get; }

    public Minion([CanBeNull] Sprite cardImage, string cardName, float cardCost, int cardMaxHealth)
    {
        CardType = CardType.Minion;
        CardImage = cardImage;
        CardName = cardName;
        CardCost = cardCost;
        CardActualHealth = cardMaxHealth;
        CardMaxHealth = cardMaxHealth;
    }

    public void Spawn()
    {
        Debug.Log("Spawned the minion");
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