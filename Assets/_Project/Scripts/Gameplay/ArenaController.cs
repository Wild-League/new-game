using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public class ArenaController : MonoBehaviour
    {
        public static ArenaController Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        [SerializeField]
        private GameObject leftArena, rightArena;


        public void ShowArenaLimit(PlayerSide side)
        {
            switch (side)
            {
                case PlayerSide.Left:
                    rightArena.SetActive(true);
                    leftArena.SetActive(false);
                    break;
                case PlayerSide.Right:
                    leftArena.SetActive(true);
                    rightArena.SetActive(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void HideArenaLimit()
        {
            rightArena.SetActive(false);
            leftArena.SetActive(false);
        }
    }
}