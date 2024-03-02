using System;
using Fusion;
using Unity.VisualScripting;
using UnityEngine;

namespace Networking
{
    public class NetworkGameplayHandler : NetworkBehaviour
    {
        public static NetworkGameplayHandler Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public static void UseSpell()
        {
            
        }
    }
}