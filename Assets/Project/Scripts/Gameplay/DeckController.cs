using System;
using System.Collections;
using System.Linq;
using Fusion;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Project.Scripts.Gameplay
{
    public class DeckController : NetworkBehaviour
    {
        public static DeckController Instance { get; private set; }

        #region Player Input

        private CardControl playerInput;
        private InputAction cardHold;

        private void Awake()
        {
            Instance = this;
            playerInput = new CardControl();
        }

        public void OnEnable()
        {
            cardHold = playerInput.Default.CardInteraction;
            cardHold.started += SelectCard;

            cardHold.canceled += PlaceCard;

            cardHold.Enable();
        }

        public void OnDisable()
        {
            cardHold.Disable();
        }

        #endregion

        [SerializeField]
        private Camera Camera;

        private Vector3 initialDragMousePos;
        private Vector3 endDragMousePos;

        private static CardRepresentation SelectedCard;
        private Vector3 CardInitialPosition;

        [SerializeField]
        private LayerMask ignoredOnPlace;

        [field: SerializeField]
        public CardRepresentation CardOne { get; private set; }

        [field: SerializeField]
        public CardRepresentation CardTwo { get; private set; }

        [field: SerializeField]
        public CardRepresentation CardThree { get; private set; }

        [field: SerializeField]
        public CardRepresentation CardFour { get; private set; }

        [field: SerializeField]
        public CardRepresentation NextCard { get; private set; }

        private void Update()
        {
            if (SelectedCard == null) return;
            if (cardHold.IsInProgress())
            {
                SelectedCard.gameObject.transform.position = new Vector3(
                    Camera.ScreenToWorldPoint(Input.mousePosition).x, Camera.ScreenToWorldPoint(Input.mousePosition).y
                    , -5);
            }
        }

        private void SelectCard(InputAction.CallbackContext context)
        {
            initialDragMousePos = Input.mousePosition;
            if (!Physics.Raycast(Camera.ScreenToWorldPoint(initialDragMousePos), Vector3.forward, out var hit
                    , Mathf.Infinity))
            {
                return;
            }

            if (!hit.collider.CompareTag("ClickableCard")) return;
            SelectedCard = hit.collider.GetComponent<CardRepresentation>();

            CardInitialPosition = SelectedCard.transform.position;
        }

        private void PlaceCard(InputAction.CallbackContext context)
        {
            if (SelectedCard == null) return;
            endDragMousePos = Input.mousePosition;

            if (!Physics.Raycast(Camera.ScreenToWorldPoint(endDragMousePos), Vector3.forward, out var hit
                    , Mathf.Infinity
                    , ignoredOnPlace
                ))
            {
                SelectedCard.transform.position = CardInitialPosition;
                SelectedCard = null;
                return;
            }


            if (hit.collider.CompareTag("Right Arena"))
            {
                SelectedCard.UseCard();
                SelectedCard.transform.position = CardInitialPosition;
                DeckCicle();
            }
            else
            {
                SelectedCard.transform.position = CardInitialPosition;
                SelectedCard = null;
            }
        }

        private void DeckCicle()
        {
            var player = PlayerController.Instance;
            var oldCard = SelectedCard.cardInfo;
            SelectedCard.cardInfo = NextCard.cardInfo;
            NextCard.cardInfo = player.GetNextCard(oldCard);
        }
    }
}