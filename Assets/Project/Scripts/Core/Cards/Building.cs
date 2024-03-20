using System;
using Fusion;
using Project.Scripts.Core.Base;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Scripts.Core.Cards
{
    [Serializable]
    public class Building : Card, ISpawnable
    {
        public int CardActualHealth { get; set; }

        public void Spawn()
        {
            NetworkRunner.GetRunnerForScene(SceneManager.GetActiveScene())
                .Spawn(GameObject.CreatePrimitive(PrimitiveType.Quad));
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