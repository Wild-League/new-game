using System;
using Fusion;
using JetBrains.Annotations;
using Project.Scripts.Core.Base;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class Minion : CardJSON, ISpawnable
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