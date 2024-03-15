using System;
using JetBrains.Annotations;
using Project.Scripts.Core.Base;
using TMPro;
using UnityEngine;

public class CardRepresentation : MonoBehaviour
{
    [field: SerializeField] public int CardIndex { get; private set; }

    [SerializeField] private GameObject cardViewContainer;

    [SerializeField] private SpriteRenderer cardPreview;

    private Card _card;

    public Card cardInfo
    {
        get => _card;
        set
        {
            if (value == null)
            {
                if (gameObject.activeSelf) gameObject.SetActive(false);
                _card = value;
                cardNameComp.SetText(String.Empty);
                cardCostComp.SetText(string.Empty);
                cardImageComp.sprite = null;

                return;
            }

            if (!gameObject.activeSelf) gameObject.SetActive(true);

            _card = value;
            // cardImageComp.sprite = value.;
            cardPreview.sprite = value.img_preview;
            cardNameComp.SetText(value.name);
            cardCostComp.SetText(value.cooldown.ToString("F1"));
            cardImageComp.sprite = value.img_card;

            cardPreview.flipX = PlayerController.Instance.side switch
            {
                PlayerSide.Left => true, PlayerSide.Right => false, _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    [SerializeField] private SpriteRenderer cardImageComp;

    [SerializeField] private TMP_Text cardNameComp;

    [SerializeField] private TMP_Text cardCostComp;

    public void SelectCard(bool selected)
    {
        cardViewContainer.SetActive(!selected);
        cardPreview.gameObject.SetActive(selected);
        gameObject.layer = selected ? LayerMask.NameToLayer("Ignore Raycast") : LayerMask.NameToLayer("Card");
    }

    public void UseCard()
    {
        // switch (_card.type)
        // {
        //     case "spell":
        //         var spell = (Spell)_card;
        //         spell.Use();
        //         break;
        //     // case :
        //     //     var build = (Building)_card;
        //     //     build.Spawn();
        //     //     break;
        //     case "char":
        //         var minion = (Minion)_card;
        //         minion.Spawn();
        //
        //         break;
        //     default:
        //         throw new ArgumentOutOfRangeException();
        // }
    }
}