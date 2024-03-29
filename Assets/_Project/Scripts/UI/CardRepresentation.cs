﻿using System;
using System.Collections;
using _Project.Scripts.Core.Base;
using _Project.Scripts.Core.Cards;
using _Project.Scripts.Gameplay;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI
{
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

        [SerializeField] private Card _card;

        public Card cardInfo
        {
            get => _card;
            set
            {
                if (value == null)
                {
                    if (gameObject.activeSelf) gameObject.SetActive(false);
                    _card = null;
                    cardNameComp.SetText(string.Empty);
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

        public void UseCard(Vector3 place)
        {
            switch (_card.type)
            {
                case "spell":
                    var spell = (Spell)_card;
                    spell.Use();
                    break;
                case "building":
                    var buildObj = Instantiate(ObjectHandler.Instance.Building);
                    buildObj.card = _card as Building;
                    buildObj.card.CardActualHealth = _card.life;
                    break;
                case "char":
                    var minionObj = Instantiate(ObjectHandler.Instance.Minion);
                    minionObj.card = _card as Minion;
                    minionObj.card.CardActualHealth = _card.life;

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
}