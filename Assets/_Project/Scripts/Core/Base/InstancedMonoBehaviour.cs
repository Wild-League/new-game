﻿using System;
using UnityEngine;

namespace _Project.Scripts.Core.Base
{
    public class InstancedMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance;

        private void Awake()
        {
            Instance = GetComponent<T>();
        }
    }
}