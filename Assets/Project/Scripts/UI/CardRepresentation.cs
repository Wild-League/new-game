using System;
using System.Collections;
using JetBrains.Annotations;
using Project.Scripts.Core.Base;
using Project.Scripts.Core.Cards;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class CardRepresentation : MonoBehaviour
{
    [field: SerializeField] private bool next;
    [field: SerializeField] public int CardIndex { get; private set; }
    [SerializeField] private GameObject cardViewContainer;
    [SerializeField] private SpriteRenderer cardPreview;
    [SerializeField] private CardCooldownObject cardCooldown;

    private bool _ready;

    public bool ReadyToPlay
    {
        get => _ready;
        private set
        {
            _ready = value;
            gameObject.layer = value ? LayerMask.NameToLayer("Card") : LayerMask.NameToLayer("Ignore Raycast");
        }
    }

    private Card _card;

    public Card cardInfo
    {
        get => _card;
        set
        {
            if (value == null)
            {
                if (gameObject.activeSelf) gameObject.SetActive(false);
                _card = null;
                cardNameComp.SetText(String.Empty);
                cardCostComp.SetText(string.Empty);
                cardImageComp.sprite = null;

                return;
            }

            if (!gameObject.activeSelf) gameObject.SetActive(true);

            _card = value;
            ReadyToPlay = false;
            cardPreview.sprite = value.img_preview;
            cardNameComp.SetText(value.name);
            cardCostComp.SetText(value.cooldown.ToString("F1"));
            cardImageComp.sprite = value.img_card;

            StartCoroutine(CardCooldown());

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
        switch (_card.type)
        {
            case "spell":
                var spell = (Spell)_card;
                spell.Use();
                break;
            case "building":
                var build = (Building)_card;
                build.Spawn();
                break;
            case "char":
                var minion = (Minion)_card;
                minion.Spawn();

                break;
            default:
                throw new ArgumentOutOfRangeException(_card.type, "Card Type Doesn't Exist :P ");
        }
    }

    private IEnumerator CardCooldown()
    {
        if (next) yield break;

        var actualTime = cardInfo.cooldown;
        while (true)
        {
            yield return null;
            actualTime -= Time.deltaTime;
            if (Math.Round(actualTime) <= 0)
            {
                ReadyToPlay = true;
                cardCooldown.DisableCooldown();
                yield break;
            }
            else
            {
                ReadyToPlay = false;
                cardCooldown.ChangeTime(actualTime);
            }
        }
    }
}

[Serializable]
public class CardCooldownObject
{
    [SerializeField] private GameObject cooldownObject;
    [SerializeField] private TMP_Text cooldownCounter;

    public void ChangeTime(float time)
    {
        if (!cooldownObject.activeSelf) cooldownObject.SetActive(true);
        cooldownCounter.SetText(time.ToString("N0"));
    }

    public void DisableCooldown()
    {
        cooldownObject.SetActive(false);
    }
}