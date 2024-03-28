using System;
using System.Collections;
using UnityEngine;

namespace ChestSystem
{
    public class Enums : MonoBehaviour
    {
        [Serializable]
        public enum ECurrancy
        {
            None,
            Coin,
            Gem
        }

        public enum States
        {
            Empty,
            Equiped,
            Enqueued
        }
    }
}