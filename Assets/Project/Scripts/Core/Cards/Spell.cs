using System;
using Fusion;
using Project.Scripts.Core.Base;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Scripts.Core.Cards
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