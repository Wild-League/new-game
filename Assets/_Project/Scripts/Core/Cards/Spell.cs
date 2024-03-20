using System;
using _Project.Scripts.Core.Base;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Core.Cards
{
    [Serializable]
    public class Spell : Card
    {
        public virtual void Use()
        {
            NetworkRunner.GetRunnerForScene(SceneManager.GetActiveScene())
                .Spawn(GameObject.CreatePrimitive(PrimitiveType.Sphere));
        }
    }
}