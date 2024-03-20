using System;
using Fusion;
using Project.Scripts.Core.Base;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Scripts.Core.Cards
{
    [Serializable]
    public class Minion : Card, ISpawnable
    {
        public int CardActualHealth { get; set; }

        public void Spawn()
        {
            NetworkRunner.GetRunnerForScene(SceneManager.GetActiveScene())
                .Spawn(GameObject.CreatePrimitive(PrimitiveType.Capsule));
        }

        public void OnSpawn()
        {
            throw new System.NotImplementedException();
        }

        public void OnDie()
        {
            throw new System.NotImplementedException();
        }
    }
}