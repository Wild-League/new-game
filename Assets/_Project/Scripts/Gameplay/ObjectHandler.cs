using _Project.Scripts.Core.Base;
using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public class ObjectHandler : InstancedMonoBehaviour<ObjectHandler>
    {
        [field: SerializeField] public SpawnedMinion Minion;
        [field: SerializeField] public SpawnedBuilding Building;
    }
}