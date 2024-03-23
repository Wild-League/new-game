using _Project.Scripts.Core.Base;
using _Project.Scripts.Core.Cards;
using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public class SpawnedBuilding : MonoBehaviour
    {
        public Building card;
        [SerializeField] private SpriteRenderer renderer;
    }
}