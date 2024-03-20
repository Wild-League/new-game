using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Scripts.Gameplay
{
    public class DeckController : MonoBehaviour
    {
        private GameObject cardPreview;
        private static Vector3 CardOffset = new Vector3(.1f, -.2f, 0);


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
            cardHold.started += CardInteraction;

            cardHold.Enable();
        }

        private void CardInteraction(InputAction.CallbackContext act)
        {
            if (SelectedCard != null)
            {
                if (Physics.Raycast(Camera.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, out var hitCard,
                        Mathf.Infinity, cardsLayer))
                {
                    Debug.Log("select another card");
                    SelectedCard.transform.position = CardInitialPosition;
                    UnselectCard();
                    SelectCard(hitCard);
                }
                else
                {
                    if (!Physics.Raycast(Camera.ScreenToWorldPoint(Input.mousePosition - CalculateOffset()),
                            Vector3.forward, out var hitArena, Mathf.Infinity, arenaLayer))
                    {
                        Debug.Log("reset card");
                        SelectedCard.transform.position = CardInitialPosition;
                        UnselectCard();
                    }
                    else
                    {
                        PlaceCard(hitArena);
                        Debug.Log("place card");
                    }
                }
            }
            else
            {
                if (Physics.Raycast(Camera.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, out var hitCard,
                        Mathf.Infinity, cardsLayer))
                {
                    SelectCard(hitCard);
                    Debug.Log("select card");
                }
            }
        }

        public void OnDisable()
        {
            cardHold.Disable();
        }

        #endregion

        [SerializeField] private Camera Camera;

        private static CardRepresentation SelectedCard;
        private Vector3 CardInitialPosition;

        [SerializeField] private LayerMask arenaLayer, cardsLayer;

        [field: SerializeField] public CardRepresentation CardOne { get; private set; }

        [field: SerializeField] public CardRepresentation CardTwo { get; private set; }

        [field: SerializeField] public CardRepresentation CardThree { get; private set; }

        [field: SerializeField] public CardRepresentation CardFour { get; private set; }

        [field: SerializeField] public CardRepresentation NextCard { get; private set; }

        private void Update()
        {
            if (SelectedCard == null) return;

            if (SelectedCard.cardInfo.type == "spell")
                SelectedCard.gameObject.transform.position = new Vector3(
                    Camera.ScreenToWorldPoint(Input.mousePosition).x
                    , Camera.ScreenToWorldPoint(Input.mousePosition).y
                    , -5);
            else
                SelectedCard.gameObject.transform.position = new Vector3(
                    Camera.ScreenToWorldPoint(Input.mousePosition).x
                    , Camera.ScreenToWorldPoint(Input.mousePosition).y
                    , -5) - CalculateOffset();
        }


        private void SelectCard(RaycastHit hit)
        {
            if (!hit.collider.CompareTag("ClickableCard")) return;
            SelectedCard = hit.collider.GetComponent<CardRepresentation>();
            if (!SelectedCard.ReadyToPlay)
            {
                Debug.Log("card cooldown, ignore it");
                return;
            }

            CardInitialPosition = SelectedCard.transform.position;
            SelectedCard.SelectCard(true);
            ArenaController.Instance.ShowArenaLimit(PlayerController.Instance.side);
        }

        private void UnselectCard()
        {
            SelectedCard.SelectCard(false);
            SelectedCard = null;
        }

        private void PlaceCard(RaycastHit hit)
        {
            if (SelectedCard == null) return;
            SelectedCard.SelectCard(false);
            ArenaController.Instance.HideArenaLimit();

            if (hit.collider.CompareTag($"{PlayerController.Instance.side.ToString()} Arena"))
            {
                SelectedCard.UseCard();
                SelectedCard.transform.position = CardInitialPosition;
                DeckCicle();
                SelectedCard = null;
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

        private void OnDrawGizmos()
        {
            if (SelectedCard == null) return;

            if (SelectedCard.cardInfo.type == "spell")
                Gizmos.DrawWireSphere(
                    SelectedCard.gameObject.transform.position
                    , SelectedCard.cardInfo.attack_range / 40);
            else
                Gizmos.DrawWireSphere(
                    SelectedCard.gameObject.transform.position + CalculateOffset()
                    , SelectedCard.cardInfo.attack_range / 40);
        }

        private static Vector3 CalculateOffset()
        {
            return PlayerController.Instance.side == PlayerSide.Left ? -CardOffset : CardOffset;
        }
    }
}