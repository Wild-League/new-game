using System;
using JetBrains.Annotations;
using Project.Scripts.Core.Base;
using TMPro;
using UnityEngine;

public class CardRepresentation : MonoBehaviour
{
    [field: SerializeField]
    public int CardIndex { get; private set; }

    private Card _card;

    public Card cardInfo
    {
        get => _card;
        set
        {
            _card = value;
            cardImageComp.sprite = value.CardImage;
            cardNameComp.SetText(value.CardName);
            cardCostComp.SetText(value.CardCost.ToString("F1"));
        }
    }

    [SerializeField]
    private SpriteRenderer cardImageComp;

    [SerializeField]
    private TMP_Text cardNameComp;

    [SerializeField]
    private TMP_Text cardCostComp;

    [SerializeField, CanBeNull]
    private BoxCollider Detector;

    public void UseCard()
    {
        switch (_card.CardType)
        {
            case CardType.Spell:
                var spell = (Spell)_card;
                spell.Use();
                break;
            case CardType.Building:
                var build = (Building)_card;
                build.Spawn();
                break;
            case CardType.Minion:
                var minion = (Minion)_card;
                minion.Spawn();

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void CastSpell()
    {
        // ()
    }

    private void PlaceBuilding()
    {
    }

    private void SummonMinion()
    {
    }
}