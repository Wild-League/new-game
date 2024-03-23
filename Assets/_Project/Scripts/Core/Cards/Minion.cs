using System;
using _Project.Scripts.Core.Base;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Core.Cards
{
    [Serializable]
    public class Minion : Card
    {
        public float CardActualHealth { get; set; }
    }
}