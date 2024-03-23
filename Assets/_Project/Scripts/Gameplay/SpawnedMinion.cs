using _Project.Scripts.Core.Base;
using _Project.Scripts.Core.Cards;
using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public class SpawnedMinion : MonoBehaviour
    {
        public Minion card;
        [SerializeField] private SpriteRenderer renderer;
    }
}