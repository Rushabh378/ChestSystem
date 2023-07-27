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
            none,
            coin,
            gem
        }

        public enum State
        {
            idle,
            running,
            queued,
            opened
        }
    }
}